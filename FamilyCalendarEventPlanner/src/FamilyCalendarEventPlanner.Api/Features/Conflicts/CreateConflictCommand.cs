using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Conflicts;

public record CreateConflictCommand : IRequest<ScheduleConflictDto>
{
    public List<Guid> ConflictingEventIds { get; init; } = new();
    public List<Guid> AffectedMemberIds { get; init; } = new();
    public ConflictSeverity ConflictSeverity { get; init; }
}

public class CreateConflictCommandHandler : IRequestHandler<CreateConflictCommand, ScheduleConflictDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<CreateConflictCommandHandler> _logger;

    public CreateConflictCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ITenantContext tenantContext,
        ILogger<CreateConflictCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<ScheduleConflictDto> Handle(CreateConflictCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating schedule conflict with {EventCount} events and {MemberCount} affected members",
            request.ConflictingEventIds.Count,
            request.AffectedMemberIds.Count);

        var conflict = new ScheduleConflict(
            _tenantContext.TenantId,
            request.ConflictingEventIds,
            request.AffectedMemberIds,
            request.ConflictSeverity);

        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created schedule conflict {ConflictId}", conflict.ConflictId);

        return conflict.ToDto();
    }
}
