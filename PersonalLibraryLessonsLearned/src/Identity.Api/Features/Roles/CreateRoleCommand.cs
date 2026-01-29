using Identity.Core;
using Identity.Core.Models.UserAggregate.Entities;
using MediatR;

namespace Identity.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
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
        var role = new Role(request.TenantId, request.Name);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role created: {RoleId}", role.RoleId);

        return new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
