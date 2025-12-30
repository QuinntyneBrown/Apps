// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// Provides seed data for the GolfScoreTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(GolfScoreTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Courses.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedCoursesAndRoundsAsync(context);
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

    private static async Task SeedCoursesAndRoundsAsync(GolfScoreTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var courses = new List<Course>
        {
            new Course
            {
                CourseId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Pebble Beach Golf Links",
                Location = "Pebble Beach, CA",
                NumberOfHoles = 18,
                TotalPar = 72,
                CourseRating = 74.5m,
                SlopeRating = 145,
                Notes = "Famous oceanside course",
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Augusta National Golf Club",
                Location = "Augusta, GA",
                NumberOfHoles = 18,
                TotalPar = 72,
                CourseRating = 76.2m,
                SlopeRating = 137,
                Notes = "Home of The Masters",
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Pine Valley Golf Club",
                Location = "Pine Valley, NJ",
                NumberOfHoles = 18,
                TotalPar = 70,
                CourseRating = 75.3m,
                SlopeRating = 153,
                Notes = "Consistently rated #1 in the world",
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "Oakmont Country Club",
                Location = "Oakmont, PA",
                NumberOfHoles = 18,
                TotalPar = 71,
                CourseRating = 77.5m,
                SlopeRating = 151,
                Notes = "Known for its challenging bunkers",
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                Name = "St Andrews Old Course",
                Location = "St Andrews, Scotland",
                NumberOfHoles = 18,
                TotalPar = 72,
                CourseRating = 73.8m,
                SlopeRating = 135,
                Notes = "The Home of Golf",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Courses.AddRange(courses);

        // Add sample rounds
        var round1 = new Round
        {
            RoundId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            CourseId = courses[0].CourseId,
            PlayedDate = DateTime.UtcNow.AddDays(-7),
            TotalScore = 82,
            TotalPar = 72,
            Weather = "Sunny, light breeze",
            Notes = "Great day on the course",
            CreatedAt = DateTime.UtcNow.AddDays(-7),
        };

        var round2 = new Round
        {
            RoundId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            CourseId = courses[1].CourseId,
            PlayedDate = DateTime.UtcNow.AddDays(-14),
            TotalScore = 88,
            TotalPar = 72,
            Weather = "Overcast",
            Notes = "Struggled with putting",
            CreatedAt = DateTime.UtcNow.AddDays(-14),
        };

        context.Rounds.AddRange(round1, round2);

        // Add sample hole scores for round1
        var holeScores = new List<HoleScore>
        {
            new HoleScore
            {
                HoleScoreId = Guid.Parse("11111111-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoundId = round1.RoundId,
                HoleNumber = 1,
                Par = 4,
                Score = 5,
                Putts = 2,
                FairwayHit = true,
                GreenInRegulation = false,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new HoleScore
            {
                HoleScoreId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoundId = round1.RoundId,
                HoleNumber = 2,
                Par = 3,
                Score = 3,
                Putts = 1,
                FairwayHit = false,
                GreenInRegulation = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new HoleScore
            {
                HoleScoreId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoundId = round1.RoundId,
                HoleNumber = 3,
                Par = 5,
                Score = 6,
                Putts = 3,
                FairwayHit = true,
                GreenInRegulation = false,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.HoleScores.AddRange(holeScores);

        // Add sample handicap
        var handicap = new Handicap
        {
            HandicapId = Guid.Parse("11111111-cccc-cccc-cccc-cccccccccccc"),
            UserId = sampleUserId,
            HandicapIndex = 12.5m,
            CalculatedDate = DateTime.UtcNow.AddDays(-1),
            RoundsUsed = 20,
            Notes = "Calculated using last 20 rounds",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
        };

        context.Handicaps.Add(handicap);

        await context.SaveChangesAsync();
    }
}
