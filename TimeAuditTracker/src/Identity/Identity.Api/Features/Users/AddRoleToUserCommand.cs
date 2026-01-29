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
    private readonly ILogger<AddRoleToUserCommandHandler> _logger;

    public AddRoleToUserCommandHandler(IIdentityDbContext context, ILogger<AddRoleToUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null)
            throw new InvalidOperationException("Role not found");

        user.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roleNames = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} added to user {UserId}", request.RoleId, request.UserId);

        return new UserDto
        {
            UserId = user.UserId,
            TenantId = user.TenantId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = roleNames
        };
    }
}
