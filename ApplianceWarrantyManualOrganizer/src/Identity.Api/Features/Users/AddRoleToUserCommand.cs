using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record AddRoleToUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, UserDto?>
{
    private readonly IIdentityDbContext _context;

    public AddRoleToUserCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) throw new InvalidOperationException("Role not found");

        user.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).ToDictionaryAsync(r => r.RoleId, cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles
                .Where(ur => roles.ContainsKey(ur.RoleId))
                .Select(ur => new RoleInfo
                {
                    RoleId = ur.RoleId,
                    Name = roles[ur.RoleId].Name
                }).ToList()
        };
    }
}
