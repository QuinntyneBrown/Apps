using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Conflicts;

public record ResolveConflictCommand : IRequest<ScheduleConflictDto?>
{
    public Guid ConflictId { get; init; }
}

public class ResolveConflictCommandHandler : IRequestHandler<ResolveConflictCommand, ScheduleConflictDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<ResolveConflictCommandHandler> _logger;

    public ResolveConflictCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<ResolveConflictCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ScheduleConflictDto?> Handle(ResolveConflictCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Resolving schedule conflict {ConflictId}", request.ConflictId);

        var conflict = await _context.ScheduleConflicts
            .FirstOrDefaultAsync(c => c.ConflictId == request.ConflictId, cancellationToken);

        if (conflict == null)
        {
            _logger.LogWarning("Schedule conflict {ConflictId} not found", request.ConflictId);
            return null;
        }

        conflict.Resolve();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Resolved schedule conflict {ConflictId}", request.ConflictId);

        return conflict.ToDto();
    }
}
