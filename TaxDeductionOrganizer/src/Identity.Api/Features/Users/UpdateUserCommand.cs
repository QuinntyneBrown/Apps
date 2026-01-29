using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record UpdateUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IIdentityDbContext _context;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IIdentityDbContext context, ILogger<UpdateUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        if (request.Email != null)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.UserId != request.UserId
                    && u.TenantId == user.TenantId, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException("Email already in use");
        }

        if (request.UserName != null)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.UserId != request.UserId
                    && u.TenantId == user.TenantId, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException("Username already in use");
        }

        user.UpdateProfile(request.UserName, request.Email);
        await _context.SaveChangesAsync(cancellationToken);

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roleNames = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("User updated: {UserId}", user.UserId);

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
