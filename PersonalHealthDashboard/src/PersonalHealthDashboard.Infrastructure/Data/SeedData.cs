// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalHealthDashboard.Core.Model.UserAggregate;
using PersonalHealthDashboard.Core.Model.UserAggregate.Entities;
using PersonalHealthDashboard.Core.Services;
namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalHealthDashboard database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalHealthDashboardContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Vitals.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedHealthDataAsync(context);
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

    private static async Task SeedHealthDataAsync(PersonalHealthDashboardContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Add sample vitals
        var vitals = new List<Vital>
        {
            new Vital
            {
                VitalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                VitalType = VitalType.BloodPressure,
                Value = 120,
                Unit = "mmHg (Systolic)",
                MeasuredAt = DateTime.UtcNow.AddHours(-2),
                Notes = "Morning measurement",
                Source = "Manual Entry",
                CreatedAt = DateTime.UtcNow,
            },
            new Vital
            {
                VitalId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                VitalType = VitalType.HeartRate,
                Value = 72,
                Unit = "bpm",
                MeasuredAt = DateTime.UtcNow.AddHours(-1),
                Notes = "Resting heart rate",
                Source = "Apple Watch",
                CreatedAt = DateTime.UtcNow,
            },
            new Vital
            {
                VitalId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                VitalType = VitalType.Weight,
                Value = 170.5,
                Unit = "lbs",
                MeasuredAt = DateTime.UtcNow.AddDays(-1),
                Notes = "Weekly weigh-in",
                Source = "Smart Scale",
                CreatedAt = DateTime.UtcNow,
            },
            new Vital
            {
                VitalId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                VitalType = VitalType.Temperature,
                Value = 98.6,
                Unit = "°F",
                MeasuredAt = DateTime.UtcNow.AddHours(-3),
                Source = "Thermometer",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Vitals.AddRange(vitals);

        // Add sample wearable data
        var wearableData = new List<WearableData>
        {
            new WearableData
            {
                WearableDataId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DeviceName = "Apple Watch Series 8",
                DataType = "Steps",
                Value = 8543,
                Unit = "steps",
                RecordedAt = DateTime.UtcNow.AddHours(-1),
                SyncedAt = DateTime.UtcNow,
                Metadata = "{\"goal\": 10000, \"distance_km\": 6.5}",
                CreatedAt = DateTime.UtcNow,
            },
            new WearableData
            {
                WearableDataId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DeviceName = "Apple Watch Series 8",
                DataType = "Calories",
                Value = 2145,
                Unit = "kcal",
                RecordedAt = DateTime.UtcNow.AddHours(-1),
                SyncedAt = DateTime.UtcNow,
                Metadata = "{\"active_calories\": 545, \"resting_calories\": 1600}",
                CreatedAt = DateTime.UtcNow,
            },
            new WearableData
            {
                WearableDataId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DeviceName = "Fitbit Charge 5",
                DataType = "Sleep",
                Value = 7.5,
                Unit = "hours",
                RecordedAt = DateTime.UtcNow.AddHours(-8),
                SyncedAt = DateTime.UtcNow,
                Metadata = "{\"deep_sleep\": 2.1, \"light_sleep\": 4.2, \"rem_sleep\": 1.2}",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.WearableData.AddRange(wearableData);

        // Add sample health trends
        var healthTrends = new List<HealthTrend>
        {
            new HealthTrend
            {
                HealthTrendId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MetricName = "Weight",
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow,
                AverageValue = 172.3,
                MinValue = 169.8,
                MaxValue = 175.2,
                TrendDirection = "Decreasing",
                PercentageChange = -2.1,
                Insights = "Good progress! Weight is trending down. Keep up the healthy habits.",
                CreatedAt = DateTime.UtcNow,
            },
            new HealthTrend
            {
                HealthTrendId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MetricName = "Steps",
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow,
                AverageValue = 9234,
                MinValue = 5432,
                MaxValue = 12543,
                TrendDirection = "Increasing",
                PercentageChange = 12.5,
                Insights = "Activity levels are improving. You're getting closer to your daily goal!",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.HealthTrends.AddRange(healthTrends);

        await context.SaveChangesAsync();
    }
}
