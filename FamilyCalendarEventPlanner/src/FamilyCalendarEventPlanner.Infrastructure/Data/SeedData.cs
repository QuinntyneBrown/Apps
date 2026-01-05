using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.EventAggregate;
using FamilyCalendarEventPlanner.Core.Models.EventAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate.Entities;
using FamilyCalendarEventPlanner.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(FamilyCalendarEventPlannerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.EnsureCreatedAsync();

            await SeedRolesAndAdminUserAsync(context, logger, passwordHasher);

            if (context.FamilyMembers.IgnoreQueryFilters().Any())
            {
                logger.LogInformation("Database already contains family data. Skipping family seed.");
                return;
            }

            logger.LogInformation("Seeding database...");

            var tenantId = Constants.DefaultTenantId;

            var familyId = Guid.NewGuid();

            var members = new List<FamilyMember>
            {
                new FamilyMember(tenantId, familyId, "John Smith", "john@example.com", "#4285F4", MemberRole.Admin),
                new FamilyMember(tenantId, familyId, "Jane Smith", "jane@example.com", "#EA4335", MemberRole.Admin),
                new FamilyMember(tenantId, familyId, "Tommy Smith", "tommy@example.com", "#34A853", MemberRole.Member),
                new FamilyMember(tenantId, familyId, "Sarah Smith", "sarah@example.com", "#FBBC05", MemberRole.Member),
            };

            context.FamilyMembers.AddRange(members);
            await context.SaveChangesAsync();

            var events = new List<CalendarEvent>
            {
                new CalendarEvent(
                    tenantId,
                    familyId,
                    members[0].MemberId,
                    "Weekly Family Dinner",
                    DateTime.UtcNow.Date.AddDays(1).AddHours(18),
                    DateTime.UtcNow.Date.AddDays(1).AddHours(20),
                    EventType.FamilyDinner,
                    "Pizza night!",
                    "Home",
                    RecurrencePattern.Weekly(1, null, new List<DayOfWeek> { DayOfWeek.Sunday })),
                new CalendarEvent(
                    tenantId,
                    familyId,
                    members[0].MemberId,
                    "Tommy's Soccer Practice",
                    DateTime.UtcNow.Date.AddDays(2).AddHours(16),
                    DateTime.UtcNow.Date.AddDays(2).AddHours(18),
                    EventType.Sports,
                    "Bring cleats and shin guards",
                    "Community Sports Field",
                    RecurrencePattern.Weekly(1, null, new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday })),
                new CalendarEvent(
                    tenantId,
                    familyId,
                    members[1].MemberId,
                    "Parent-Teacher Conference",
                    DateTime.UtcNow.Date.AddDays(7).AddHours(15),
                    DateTime.UtcNow.Date.AddDays(7).AddHours(16),
                    EventType.School,
                    "Discuss progress reports",
                    "Lincoln Elementary School"),
                new CalendarEvent(
                    tenantId,
                    familyId,
                    members[0].MemberId,
                    "Family Vacation",
                    DateTime.UtcNow.Date.AddDays(30),
                    DateTime.UtcNow.Date.AddDays(37),
                    EventType.Vacation,
                    "Beach trip to Florida",
                    "Miami Beach, FL"),
            };

            context.CalendarEvents.AddRange(events);
            await context.SaveChangesAsync();

            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedRolesAndAdminUserAsync(
        FamilyCalendarEventPlannerContext context,
        ILogger logger,
        IPasswordHasher passwordHasher)
    {
        // Use a default tenant ID for seed data
        var defaultTenantId = Constants.DefaultTenantId;

        // Seed roles if they don't exist
        var adminRoleName = "Admin";
        var userRoleName = "User";

        var adminRole = context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.Name == adminRoleName);
        if (adminRole == null)
        {
            adminRole = new Role(defaultTenantId, adminRoleName);
            context.Roles.Add(adminRole);
            logger.LogInformation("Created Admin role.");
        }

        var userRole = context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.Name == userRoleName);
        if (userRole == null)
        {
            userRole = new Role(defaultTenantId, userRoleName);
            context.Roles.Add(userRole);
            logger.LogInformation("Created User role.");
        }

        await context.SaveChangesAsync();

        // Seed admin user if it doesn't exist
        var adminUserName = "admin";
        var adminUser = context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.UserName == adminUserName);
        if (adminUser == null)
        {
            var (hashedPassword, salt) = passwordHasher.HashPassword("Admin123!");
            adminUser = new User(
                tenantId: defaultTenantId,
                userName: adminUserName,
                email: "admin@familycalendar.local",
                hashedPassword: hashedPassword,
                salt: salt);

            adminUser.AddRole(adminRole);
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

            logger.LogInformation("Created admin user with Admin role.");
        }
    }
}
