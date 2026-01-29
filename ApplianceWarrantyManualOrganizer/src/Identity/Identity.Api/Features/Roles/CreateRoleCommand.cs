using Identity.Core;
using Identity.Core.Models.UserAggregate.Entities;
using MediatR;

namespace Identity.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{
    public string Name { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IIdentityDbContext _context;

    public CreateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role(request.TenantId, request.Name);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        return new RoleDto { RoleId = role.RoleId, Name = role.Name };
    }
}
