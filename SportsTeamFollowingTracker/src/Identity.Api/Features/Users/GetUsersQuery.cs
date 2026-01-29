using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUsersQuery(Guid TenantId) : IRequest<IEnumerable<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IIdentityDbContext _context;

    public GetUsersQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Where(u => u.TenantId == request.TenantId)
            .Include(u => u.UserRoles)
            .ToListAsync(cancellationToken);

        var roleIds = users.SelectMany(u => u.UserRoles).Select(ur => ur.RoleId).Distinct().ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).ToDictionaryAsync(r => r.RoleId, r => r.Name, cancellationToken);

        return users.Select(u => new UserDto(
            u.UserId,
            u.TenantId,
            u.UserName,
            u.Email,
            u.UserRoles.Select(ur => roles.GetValueOrDefault(ur.RoleId, "Unknown"))));
    }
}
