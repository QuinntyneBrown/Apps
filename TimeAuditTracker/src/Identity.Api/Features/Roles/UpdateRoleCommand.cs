using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record UpdateRoleCommand : IRequest<RoleDto?>
{
    public Guid RoleId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    private readonly IIdentityDbContext _context;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(IIdentityDbContext context, ILogger<UpdateRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return null;

        if (request.Name != null)
        {
            var existingRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == request.Name && r.RoleId != request.RoleId
                    && r.TenantId == role.TenantId, cancellationToken);
            if (existingRole != null)
                throw new InvalidOperationException("Role with this name already exists");
        }

        role.Update(request.Name, request.Description);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role updated: {RoleId}", role.RoleId);

        return new RoleDto
        {
            RoleId = role.RoleId,
            TenantId = role.TenantId,
            Name = role.Name,
            Description = role.Description
        };
    }
}
