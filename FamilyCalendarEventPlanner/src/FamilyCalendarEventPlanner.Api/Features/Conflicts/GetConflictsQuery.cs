using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Conflicts;

public record GetConflictsQuery : IRequest<IEnumerable<ScheduleConflictDto>>
{
    public Guid? MemberId { get; init; }
    public bool? IsResolved { get; init; }
}

public class GetConflictsQueryHandler : IRequestHandler<GetConflictsQuery, IEnumerable<ScheduleConflictDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetConflictsQueryHandler> _logger;

    public GetConflictsQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetConflictsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ScheduleConflictDto>> Handle(GetConflictsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting conflicts for member {MemberId}, isResolved: {IsResolved}",
            request.MemberId,
            request.IsResolved);

        var query = _context.ScheduleConflicts.AsNoTracking();

        if (request.IsResolved.HasValue)
        {
            query = query.Where(c => c.IsResolved == request.IsResolved.Value);
        }

        var conflicts = await query.ToListAsync(cancellationToken);

        if (request.MemberId.HasValue)
        {
            conflicts = conflicts
                .Where(c => c.AffectedMemberIds.Contains(request.MemberId.Value))
                .ToList();
        }

        return conflicts.Select(c => c.ToDto());
    }
}
