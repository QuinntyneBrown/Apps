// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Infrastructure;

/// <summary>
/// Provides seed data for the SportsTeamFollowingTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SportsTeamFollowingTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Teams.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDataAsync(context);
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

    private static async Task SeedDataAsync(SportsTeamFollowingTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var teams = new List<Team>
        {
            new Team
            {
                TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "New York Giants",
                Sport = Sport.Football,
                League = "NFL",
                City = "New York",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Team
            {
                TeamId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Los Angeles Lakers",
                Sport = Sport.Basketball,
                League = "NBA",
                City = "Los Angeles",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Teams.AddRange(teams);

        var games = new List<Game>
        {
            new Game
            {
                GameId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                TeamId = teams[0].TeamId,
                GameDate = DateTime.UtcNow.AddDays(-7),
                Opponent = "Dallas Cowboys",
                TeamScore = 24,
                OpponentScore = 21,
                IsWin = true,
                Notes = "Great defensive game!",
                CreatedAt = DateTime.UtcNow,
            },
            new Game
            {
                GameId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                TeamId = teams[1].TeamId,
                GameDate = DateTime.UtcNow.AddDays(-3),
                Opponent = "Boston Celtics",
                TeamScore = 108,
                OpponentScore = 112,
                IsWin = false,
                Notes = "Close game, overtime thriller",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Games.AddRange(games);

        var seasons = new List<Season>
        {
            new Season
            {
                SeasonId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                TeamId = teams[0].TeamId,
                SeasonName = "2024-2025 Regular Season",
                Year = 2024,
                Wins = 8,
                Losses = 5,
                Notes = "Strong start to the season",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Seasons.AddRange(seasons);

        var statistics = new List<Statistic>
        {
            new Statistic
            {
                StatisticId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                TeamId = teams[0].TeamId,
                StatName = "Passing Yards Per Game",
                Value = 245.5m,
                RecordedDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Statistics.AddRange(statistics);

        await context.SaveChangesAsync();
    }
}
