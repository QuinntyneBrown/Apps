// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.ConflictAggregate;

public class ScheduleConflict
{
    public Guid ConflictId { get; private set; }
    public List<Guid> ConflictingEventIds { get; private set; }
    public List<Guid> AffectedMemberIds { get; private set; }
    public ConflictSeverity ConflictSeverity { get; private set; }
    public bool IsResolved { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    private ScheduleConflict()
    {
        ConflictingEventIds = new List<Guid>();
        AffectedMemberIds = new List<Guid>();
    }

    public ScheduleConflict(
        IEnumerable<Guid> conflictingEventIds,
        IEnumerable<Guid> affectedMemberIds,
        ConflictSeverity severity)
    {
        var eventIds = conflictingEventIds.ToList();
        var memberIds = affectedMemberIds.ToList();

        if (eventIds.Count < 2)
        {
            throw new ArgumentException("At least two events are required to create a conflict.", nameof(conflictingEventIds));
        }

        if (memberIds.Count == 0)
        {
            throw new ArgumentException("At least one affected member is required.", nameof(affectedMemberIds));
        }

        ConflictId = Guid.NewGuid();
        ConflictingEventIds = eventIds;
        AffectedMemberIds = memberIds;
        ConflictSeverity = severity;
        IsResolved = false;
        ResolvedAt = null;
    }

    public void Resolve()
    {
        if (IsResolved)
        {
            throw new InvalidOperationException("Conflict is already resolved.");
        }

        IsResolved = true;
        ResolvedAt = DateTime.UtcNow;
    }

    public void UpdateSeverity(ConflictSeverity newSeverity)
    {
        if (IsResolved)
        {
            throw new InvalidOperationException("Cannot update severity of a resolved conflict.");
        }

        ConflictSeverity = newSeverity;
    }
}
