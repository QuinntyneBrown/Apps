using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUserByIdQuery(Guid UserId, Guid TenantId) : IRequest<UserDto?>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IIdentityDbContext _context;

    public GetUserByIdQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId && u.TenantId == request.TenantId, cancellationToken);

        if (user == null) return null;

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).Select(r => r.Name).ToListAsync(cancellationToken);

        return new UserDto(user.UserId, user.TenantId, user.UserName, user.Email, roles);
    }
}
