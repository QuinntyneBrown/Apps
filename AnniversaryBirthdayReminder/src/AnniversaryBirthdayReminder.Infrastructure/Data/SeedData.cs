// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using AnniversaryBirthdayReminder.Core.Model.UserAggregate;
using AnniversaryBirthdayReminder.Core.Model.UserAggregate.Entities;
using AnniversaryBirthdayReminder.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// Provides seed data for the AnniversaryBirthdayReminder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="passwordHasher">The password hasher.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(AnniversaryBirthdayReminderContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.EnsureCreatedAsync();

            await SeedRolesAndAdminUserAsync(context, logger, passwordHasher);

            if (!await context.ImportantDates.IgnoreQueryFilters().AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedImportantDatesAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedRolesAndAdminUserAsync(
        AnniversaryBirthdayReminderContext context,
        ILogger logger,
        IPasswordHasher passwordHasher)
    {
        var defaultTenantId = Constants.DefaultTenantId;

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

        var adminUserName = "admin";
        var adminUser = context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.UserName == adminUserName);
        if (adminUser == null)
        {
            var (hashedPassword, salt) = passwordHasher.HashPassword("Admin123!");
            adminUser = new User(
                tenantId: defaultTenantId,
                userName: adminUserName,
                email: "admin@anniversarybirthday.local",
                hashedPassword: hashedPassword,
                salt: salt);

            adminUser.AddRole(adminRole);
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

            logger.LogInformation("Created admin user with Admin role.");
        }
    }

    private static async Task SeedImportantDatesAsync(AnniversaryBirthdayReminderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var importantDates = new List<ImportantDate>
        {
            new ImportantDate
            {
                ImportantDateId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                PersonName = "John Smith",
                DateType = DateType.Birthday,
                DateValue = new DateTime(1985, 6, 15),
                RecurrencePattern = RecurrencePattern.Annual,
                Relationship = "Friend",
                Notes = "Likes technology gadgets",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new ImportantDate
            {
                ImportantDateId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                PersonName = "Jane Doe",
                DateType = DateType.Birthday,
                DateValue = new DateTime(1990, 3, 22),
                RecurrencePattern = RecurrencePattern.Annual,
                Relationship = "Family",
                Notes = "Loves books and gardening",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new ImportantDate
            {
                ImportantDateId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                PersonName = "Wedding Anniversary",
                DateType = DateType.Anniversary,
                DateValue = new DateTime(2015, 9, 10),
                RecurrencePattern = RecurrencePattern.Annual,
                Relationship = "Spouse",
                Notes = "10th anniversary coming up!",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new ImportantDate
            {
                ImportantDateId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                PersonName = "Mom",
                DateType = DateType.Birthday,
                DateValue = new DateTime(1960, 12, 5),
                RecurrencePattern = RecurrencePattern.Annual,
                Relationship = "Family",
                Notes = "Prefers experiences over gifts",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new ImportantDate
            {
                ImportantDateId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                PersonName = "Company Anniversary",
                DateType = DateType.Custom,
                DateValue = new DateTime(2020, 1, 15),
                RecurrencePattern = RecurrencePattern.Annual,
                Relationship = "Work",
                Notes = "Celebrate work anniversary",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.ImportantDates.AddRange(importantDates);

        // Add sample reminders for the first important date
        var reminder = new Reminder
        {
            ReminderId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            ImportantDateId = importantDates[0].ImportantDateId,
            ScheduledTime = importantDates[0].DateValue.AddDays(-7).AddYears(DateTime.UtcNow.Year - importantDates[0].DateValue.Year),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        context.Reminders.Add(reminder);

        // Add sample gift for the first important date
        var gift = new Gift
        {
            GiftId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            ImportantDateId = importantDates[0].ImportantDateId,
            Description = "Wireless Bluetooth Headphones",
            EstimatedPrice = 79.99m,
            PurchaseUrl = "https://example.com/headphones",
            Status = GiftStatus.Idea,
        };

        context.Gifts.Add(gift);

        // Add sample celebration for the third important date (anniversary)
        var celebration = new Celebration
        {
            CelebrationId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            ImportantDateId = importantDates[2].ImportantDateId,
            CelebrationDate = new DateTime(2024, 9, 10),
            Notes = "Had a wonderful dinner at our favorite restaurant",
            Photos = new List<string> { "photo1.jpg", "photo2.jpg" },
            Attendees = new List<string> { "Spouse" },
            Rating = 5,
            Status = CelebrationStatus.Completed,
        };

        context.Celebrations.Add(celebration);

        await context.SaveChangesAsync();
    }
}
