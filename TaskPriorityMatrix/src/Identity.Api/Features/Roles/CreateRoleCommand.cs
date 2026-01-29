using Identity.Core;
using Identity.Core.Models.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IIdentityDbContext _context;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(IIdentityDbContext context, ILogger<CreateRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == request.Name && r.TenantId == request.TenantId, cancellationToken);

        if (existingRole != null)
            throw new InvalidOperationException("Role with this name already exists");

        var role = new Role(request.TenantId, request.Name, request.Description);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role created: {RoleId}", role.RoleId);

        return new RoleDto
        {
            RoleId = role.RoleId,
            TenantId = role.TenantId,
            Name = role.Name,
            Description = role.Description
        };
    }
}
