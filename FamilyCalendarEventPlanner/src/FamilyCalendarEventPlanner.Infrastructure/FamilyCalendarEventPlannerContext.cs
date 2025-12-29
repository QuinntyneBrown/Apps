using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class FamilyCalendarEventPlannerContext : DbContext, IFamilyCalendarEventPlannerContext
{
    public DbSet<CalendarEvent> CalendarEvents => Set<CalendarEvent>();
    public DbSet<EventAttendee> EventAttendees => Set<EventAttendee>();
    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();
    public DbSet<AvailabilityBlock> AvailabilityBlocks => Set<AvailabilityBlock>();
    public DbSet<ScheduleConflict> ScheduleConflicts => Set<ScheduleConflict>();
    public DbSet<EventReminder> EventReminders => Set<EventReminder>();

    public FamilyCalendarEventPlannerContext(DbContextOptions<FamilyCalendarEventPlannerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CalendarEventConfiguration());
        modelBuilder.ApplyConfiguration(new EventAttendeeConfiguration());
        modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
        modelBuilder.ApplyConfiguration(new AvailabilityBlockConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConflictConfiguration());
        modelBuilder.ApplyConfiguration(new EventReminderConfiguration());
    }
}
