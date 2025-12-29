using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(FamilyCalendarEventPlannerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.EnsureCreatedAsync();

            if (context.FamilyMembers.Any())
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
                return;
            }

            logger.LogInformation("Seeding database...");

            var familyId = Guid.NewGuid();

            var members = new List<FamilyMember>
            {
                new FamilyMember(familyId, "John Smith", "john@example.com", "#4285F4", MemberRole.Admin),
                new FamilyMember(familyId, "Jane Smith", "jane@example.com", "#EA4335", MemberRole.Admin),
                new FamilyMember(familyId, "Tommy Smith", "tommy@example.com", "#34A853", MemberRole.Member),
                new FamilyMember(familyId, "Sarah Smith", "sarah@example.com", "#FBBC05", MemberRole.Member),
            };

            context.FamilyMembers.AddRange(members);
            await context.SaveChangesAsync();

            var events = new List<CalendarEvent>
            {
                new CalendarEvent(
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
                    familyId,
                    members[1].MemberId,
                    "Parent-Teacher Conference",
                    DateTime.UtcNow.Date.AddDays(7).AddHours(15),
                    DateTime.UtcNow.Date.AddDays(7).AddHours(16),
                    EventType.School,
                    "Discuss progress reports",
                    "Lincoln Elementary School"),
                new CalendarEvent(
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
}
