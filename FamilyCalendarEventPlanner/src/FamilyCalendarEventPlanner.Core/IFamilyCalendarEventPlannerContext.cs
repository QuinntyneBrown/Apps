using Microsoft.EntityFrameworkCore;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;

namespace FamilyCalendarEventPlanner.Core;

public interface IFamilyCalendarEventPlannerContext
{
    DbSet<CalendarEvent> CalendarEvents { get; }
    DbSet<EventAttendee> EventAttendees { get; }
    DbSet<FamilyMember> FamilyMembers { get; }
    DbSet<AvailabilityBlock> AvailabilityBlocks { get; }
    DbSet<ScheduleConflict> ScheduleConflicts { get; }
    DbSet<EventReminder> EventReminders { get; }
    DbSet<Household> Households { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
