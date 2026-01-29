using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record GetRolesQuery(Guid TenantId) : IRequest<IEnumerable<RoleDto>>;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IIdentityDbContext _context;

    public GetRolesQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .Where(r => r.TenantId == request.TenantId)
            .Select(r => new RoleDto(r.RoleId, r.TenantId, r.Name))
            .ToListAsync(cancellationToken);
    }
}
