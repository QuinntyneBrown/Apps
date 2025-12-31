using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalNetWorthDashboard.Api.Features.Roles;

public record GetRoleByIdQuery : IRequest<RoleDto?>
{
    public Guid RoleId { get; init; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IPersonalNetWorthDashboardContext _context;
    private readonly ILogger<GetRoleByIdQueryHandler> _logger;

    public GetRoleByIdQueryHandler(IPersonalNetWorthDashboardContext context, ILogger<GetRoleByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting role {RoleId}", request.RoleId);
        var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        return role?.ToDto();
    }
}
