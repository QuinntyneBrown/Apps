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
        var users = await _context.Users.ToListAsync(cancellationToken);
        var userRoles = await _context.UserRoles.ToListAsync(cancellationToken);
        var roles = await _context.Roles.ToListAsync(cancellationToken);

        return users.Select(u => new UserDto
        {
            UserId = u.UserId,
            UserName = u.UserName,
            Email = u.Email,
            Roles = userRoles
                .Where(ur => ur.UserId == u.UserId)
                .Join(roles, ur => ur.RoleId, r => r.RoleId, (ur, r) => r.Name)
                .ToList()
        });
    }
}
