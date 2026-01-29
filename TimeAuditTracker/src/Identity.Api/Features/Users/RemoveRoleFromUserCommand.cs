using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record RemoveRoleFromUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}

public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, UserDto?>
{
    private readonly IIdentityDbContext _context;
    private readonly ILogger<RemoveRoleFromUserCommandHandler> _logger;

    public RemoveRoleFromUserCommandHandler(IIdentityDbContext context, ILogger<RemoveRoleFromUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        user.RemoveRole(request.RoleId);
        await _context.SaveChangesAsync(cancellationToken);

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roleNames = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} removed from user {UserId}", request.RoleId, request.UserId);

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
