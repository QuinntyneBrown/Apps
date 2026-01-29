using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record GetRoleByIdQuery : IRequest<RoleDto?>
{
    public Guid RoleId { get; init; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IIdentityDbContext _context;

    public GetRoleByIdQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return null;

        return new RoleDto
        {
            RoleId = role.RoleId,
            TenantId = role.TenantId,
            Name = role.Name,
            Description = role.Description
        };
    }
}
