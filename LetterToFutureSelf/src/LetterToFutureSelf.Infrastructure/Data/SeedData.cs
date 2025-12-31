// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using LetterToFutureSelf.Core.Model.UserAggregate;
using LetterToFutureSelf.Core.Model.UserAggregate.Entities;
using LetterToFutureSelf.Core.Services;
namespace LetterToFutureSelf.Infrastructure;

/// <summary>
/// Provides seed data for the LetterToFutureSelf database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(LetterToFutureSelfContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Letters.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedLettersAsync(context);
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

    private static async Task SeedLettersAsync(LetterToFutureSelfContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var letters = new List<Letter>
        {
            new Letter
            {
                LetterId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Subject = "New Year's Resolutions 2024",
                Content = "Dear Future Me,\n\nAs I start this new year, I want to reflect on my goals...",
                WrittenDate = new DateTime(2024, 1, 1),
                ScheduledDeliveryDate = new DateTime(2025, 1, 1),
                DeliveryStatus = DeliveryStatus.Pending,
                HasBeenRead = false,
                CreatedAt = new DateTime(2024, 1, 1),
            },
            new Letter
            {
                LetterId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Subject = "Career Reflection",
                Content = "Dear Future Me,\n\nI hope you've achieved the career milestones we discussed...",
                WrittenDate = new DateTime(2024, 6, 15),
                ScheduledDeliveryDate = new DateTime(2025, 6, 15),
                DeliveryStatus = DeliveryStatus.Pending,
                HasBeenRead = false,
                CreatedAt = new DateTime(2024, 6, 15),
            },
            new Letter
            {
                LetterId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Subject = "Birthday Wisdom",
                Content = "Dear Future Me,\n\nOn this birthday, I want to capture what I've learned this year...",
                WrittenDate = new DateTime(2023, 3, 20),
                ScheduledDeliveryDate = new DateTime(2024, 3, 20),
                DeliveryStatus = DeliveryStatus.Delivered,
                ActualDeliveryDate = new DateTime(2024, 3, 20),
                HasBeenRead = true,
                ReadDate = new DateTime(2024, 3, 21),
                CreatedAt = new DateTime(2023, 3, 20),
            },
        };

        context.Letters.AddRange(letters);

        // Add delivery schedules for the first letter
        var deliverySchedules = new List<DeliverySchedule>
        {
            new DeliverySchedule
            {
                DeliveryScheduleId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LetterId = letters[0].LetterId,
                ScheduledDateTime = new DateTime(2025, 1, 1, 9, 0, 0),
                DeliveryMethod = "Email",
                RecipientContact = "user@example.com",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1),
            },
            new DeliverySchedule
            {
                DeliveryScheduleId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LetterId = letters[0].LetterId,
                ScheduledDateTime = new DateTime(2025, 1, 1, 12, 0, 0),
                DeliveryMethod = "In-App",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1),
            },
        };

        context.DeliverySchedules.AddRange(deliverySchedules);

        await context.SaveChangesAsync();
    }
}
