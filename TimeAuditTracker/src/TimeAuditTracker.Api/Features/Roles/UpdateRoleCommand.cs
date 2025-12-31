using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.Roles;

public record UpdateRoleCommand : IRequest<RoleDto?>
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(ITimeAuditTrackerContext context, ILogger<UpdateRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating role {RoleId}", request.RoleId);
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return null;
        if (request.Name != role.Name && await _context.Roles.AnyAsync(r => r.Name == request.Name && r.RoleId != request.RoleId, cancellationToken))
            throw new InvalidOperationException($"Role with name '{request.Name}' already exists.");
        role.UpdateName(request.Name);
        await _context.SaveChangesAsync(cancellationToken);
        return role.ToDto();
    }
}
