using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record UpdateRoleCommand(Guid RoleId, Guid TenantId, string Name) : IRequest<RoleDto?>;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    private readonly IIdentityDbContext _context;

    public UpdateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.RoleId && r.TenantId == request.TenantId, cancellationToken);

        if (role == null) return null;

        role.UpdateName(request.Name);
        await _context.SaveChangesAsync(cancellationToken);

        return new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
