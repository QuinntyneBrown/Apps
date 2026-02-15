using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record GetRolesQuery : IRequest<IEnumerable<RoleDto>>;

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
            .AsNoTracking()
            .Select(r => new RoleDto { RoleId = r.RoleId, Name = r.Name })
            .ToListAsync(cancellationToken);
    }
}
