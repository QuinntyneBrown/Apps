using Microsoft.EntityFrameworkCore;
using FamilyCalendarEventPlanner.Core.Models.EventAggregate;
using FamilyCalendarEventPlanner.Core.Models.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Models.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Models.HouseholdAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate.Entities;

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
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
