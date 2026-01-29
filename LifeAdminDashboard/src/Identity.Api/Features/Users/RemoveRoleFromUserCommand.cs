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
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        user.RemoveRole(request.RoleId);
        await _context.SaveChangesAsync(cancellationToken);

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} removed from user {UserId}", request.RoleId, request.UserId);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles
                .Select(ur => allRoles.FirstOrDefault(r => r.RoleId == ur.RoleId))
                .Where(r => r != null)
                .Select(r => new RoleInfo { RoleId = r!.RoleId, Name = r.Name })
                .ToList()
        };
    }
}
