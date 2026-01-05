using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.Conflicts;

public record ScheduleConflictDto
{
    public Guid ConflictId { get; init; }
    public List<Guid> ConflictingEventIds { get; init; } = new();
    public List<Guid> AffectedMemberIds { get; init; } = new();
    public ConflictSeverity ConflictSeverity { get; init; }
    public bool IsResolved { get; init; }
    public DateTime? ResolvedAt { get; init; }
}

public static class ScheduleConflictExtensions
{
    public static ScheduleConflictDto ToDto(this ScheduleConflict conflict)
    {
        return new ScheduleConflictDto
        {
            ConflictId = conflict.ConflictId,
            ConflictingEventIds = conflict.ConflictingEventIds,
            AffectedMemberIds = conflict.AffectedMemberIds,
            ConflictSeverity = conflict.ConflictSeverity,
            IsResolved = conflict.IsResolved,
            ResolvedAt = conflict.ResolvedAt,
        };
    }
}
