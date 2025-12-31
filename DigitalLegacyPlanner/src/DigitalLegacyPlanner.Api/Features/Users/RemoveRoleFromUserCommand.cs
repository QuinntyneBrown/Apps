using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalLegacyPlanner.Api.Features.Users;

public record RemoveRoleFromUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}

public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, UserDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;
    private readonly ILogger<RemoveRoleFromUserCommandHandler> _logger;

    public RemoveRoleFromUserCommandHandler(IDigitalLegacyPlannerContext context, ILogger<RemoveRoleFromUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removing role {RoleId} from user {UserId}", request.RoleId, request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        user.RemoveRole(request.RoleId);
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }
}
