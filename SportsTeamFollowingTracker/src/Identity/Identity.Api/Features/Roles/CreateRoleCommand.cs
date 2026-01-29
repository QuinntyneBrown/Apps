using Identity.Core;
using Identity.Core.Models.UserAggregate.Entities;
using MediatR;

namespace Identity.Api.Features.Roles;

public record CreateRoleCommand(Guid TenantId, string Name) : IRequest<RoleDto>;

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

        return new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
