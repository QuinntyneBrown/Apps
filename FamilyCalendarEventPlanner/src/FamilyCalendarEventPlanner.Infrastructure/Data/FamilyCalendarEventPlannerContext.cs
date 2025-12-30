using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;
using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyCalendarEventPlanner.Infrastructure.Data;

public class FamilyCalendarEventPlannerContext : DbContext, IFamilyCalendarEventPlannerContext
{
    public FamilyCalendarEventPlannerContext(DbContextOptions<FamilyCalendarEventPlannerContext> options)
        : base(options)
    {
    }

    public DbSet<CalendarEvent> CalendarEvents { get; set; } = null!;

    public DbSet<EventAttendee> EventAttendees { get; set; } = null!;

    public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;

    public DbSet<AvailabilityBlock> AvailabilityBlocks { get; set; } = null!;

    public DbSet<ScheduleConflict> ScheduleConflicts { get; set; } = null!;

    public DbSet<EventReminder> EventReminders { get; set; } = null!;

    public DbSet<Household> Households { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    public DbSet<UserRole> UserRoles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyCalendarEventPlannerContext).Assembly);
    }
}
