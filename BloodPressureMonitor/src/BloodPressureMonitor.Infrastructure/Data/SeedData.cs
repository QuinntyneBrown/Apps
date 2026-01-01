// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BloodPressureMonitor.Core.Model.UserAggregate;
using BloodPressureMonitor.Core.Model.UserAggregate.Entities;
using BloodPressureMonitor.Core.Services;
namespace BloodPressureMonitor.Infrastructure;

/// <summary>
/// Provides seed data for the BloodPressureMonitor database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(BloodPressureMonitorContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Readings.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedReadingsAsync(context);
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

    private static async Task SeedReadingsAsync(BloodPressureMonitorContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var readings = new List<Reading>
        {
            new Reading
            {
                ReadingId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Systolic = 118,
                Diastolic = 76,
                Pulse = 72,
                Category = BloodPressureCategory.Normal,
                MeasuredAt = DateTime.UtcNow.AddDays(-7),
                Position = "Sitting",
                Arm = "Left",
                Notes = "Morning reading, before breakfast",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Reading
            {
                ReadingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Systolic = 122,
                Diastolic = 78,
                Pulse = 75,
                Category = BloodPressureCategory.Elevated,
                MeasuredAt = DateTime.UtcNow.AddDays(-6),
                Position = "Sitting",
                Arm = "Left",
                Notes = "Evening reading, after dinner",
                CreatedAt = DateTime.UtcNow.AddDays(-6),
            },
            new Reading
            {
                ReadingId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Systolic = 132,
                Diastolic = 82,
                Pulse = 78,
                Category = BloodPressureCategory.HypertensionStage1,
                MeasuredAt = DateTime.UtcNow.AddDays(-5),
                Position = "Sitting",
                Arm = "Left",
                Notes = "After stressful work day",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new Reading
            {
                ReadingId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Systolic = 116,
                Diastolic = 74,
                Pulse = 68,
                Category = BloodPressureCategory.Normal,
                MeasuredAt = DateTime.UtcNow.AddDays(-4),
                Position = "Sitting",
                Arm = "Left",
                Notes = "Morning reading after exercise",
                CreatedAt = DateTime.UtcNow.AddDays(-4),
            },
            new Reading
            {
                ReadingId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Systolic = 120,
                Diastolic = 79,
                Pulse = 73,
                Category = BloodPressureCategory.Elevated,
                MeasuredAt = DateTime.UtcNow.AddDays(-3),
                Position = "Sitting",
                Arm = "Left",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
        };

        context.Readings.AddRange(readings);

        // Add sample trends
        var trends = new List<Trend>
        {
            new Trend
            {
                TrendId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddDays(-1),
                AverageSystolic = 121.5m,
                AverageDiastolic = 77.8m,
                HighestSystolic = 135,
                HighestDiastolic = 86,
                LowestSystolic = 112,
                LowestDiastolic = 70,
                ReadingCount = 28,
                TrendDirection = "Stable",
                Insights = "Blood pressure is generally in the normal to elevated range. Consider lifestyle modifications to prevent progression.",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
            new Trend
            {
                TrendId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow,
                AverageSystolic = 121.6m,
                AverageDiastolic = 77.8m,
                HighestSystolic = 132,
                HighestDiastolic = 82,
                LowestSystolic = 116,
                LowestDiastolic = 74,
                ReadingCount = 5,
                TrendDirection = "Improving",
                Insights = "Recent readings show slight improvement. Continue current lifestyle habits.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Trends.AddRange(trends);

        await context.SaveChangesAsync();
    }
}
