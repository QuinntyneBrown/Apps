using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record GetRoleByIdQuery(Guid RoleId) : IRequest<RoleDto?>;

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
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);

        return role == null ? null : new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
