using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;

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
            .Include(u => u.UserRoles)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

        return users.Select(u => new UserDto(
            u.UserId,
            u.TenantId,
            u.UserName,
            u.Email,
            u.UserRoles.Select(ur => allRoles.First(r => r.RoleId == ur.RoleId).Name).ToList()));
    }
}
