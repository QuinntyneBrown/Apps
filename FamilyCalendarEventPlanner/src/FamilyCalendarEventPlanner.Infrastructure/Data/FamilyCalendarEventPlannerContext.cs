using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Models.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Models.EventAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.HouseholdAggregate;
using FamilyCalendarEventPlanner.Core.Models.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate.Entities;
using FamilyCalendarEventPlanner.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FamilyCalendarEventPlanner.Infrastructure.Data;

public class FamilyCalendarEventPlannerContext : DbContext, IFamilyCalendarEventPlannerContext
{
    private readonly ITenantContext? _tenantContext;

    public FamilyCalendarEventPlannerContext(DbContextOptions<FamilyCalendarEventPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<CalendarEvent>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<EventAttendee>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<FamilyMember>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<AvailabilityBlock>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ScheduleConflict>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<EventReminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Household>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<UserRole>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyCalendarEventPlannerContext).Assembly);
    }
}
