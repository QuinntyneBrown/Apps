using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Roles;

public record GetRoleByIdQuery : IRequest<RoleDto?>
{
    public Guid RoleId { get; init; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetRoleByIdQueryHandler> _logger;

    public GetRoleByIdQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetRoleByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting role {RoleId}", request.RoleId);

        var role = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);

        return role?.ToDto();
    }
}
