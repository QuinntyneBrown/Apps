using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record GetRoleByIdQuery(Guid RoleId, Guid TenantId) : IRequest<RoleDto?>;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IIdentityDbContext _context;

    public GetRoleByIdQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.RoleId && r.TenantId == request.TenantId, cancellationToken);

        return role == null ? null : new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
