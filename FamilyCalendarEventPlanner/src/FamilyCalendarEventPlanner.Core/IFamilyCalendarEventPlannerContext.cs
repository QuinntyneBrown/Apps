using Microsoft.EntityFrameworkCore;

namespace FamilyCalendarEventPlanner.Core;

public interface IFamilyCalendarEventPlannerContext
{
    DbSet<CalendarEvent> CalendarEvents { get; }
    DbSet<EventAttendee> EventAttendees { get; }
    DbSet<FamilyMember> FamilyMembers { get; }
    DbSet<AvailabilityBlock> AvailabilityBlocks { get; }
    DbSet<ScheduleConflict> ScheduleConflicts { get; }
    DbSet<EventReminder> EventReminders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
