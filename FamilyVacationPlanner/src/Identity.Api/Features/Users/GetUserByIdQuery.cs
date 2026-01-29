using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
}

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
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roles = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = roles
        };
    }
}
