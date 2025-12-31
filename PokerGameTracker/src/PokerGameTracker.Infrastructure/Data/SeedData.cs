// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokerGameTracker.Core;

using PokerGameTracker.Core.Model.UserAggregate;
using PokerGameTracker.Core.Model.UserAggregate.Entities;
using PokerGameTracker.Core.Services;
namespace PokerGameTracker.Infrastructure;

/// <summary>
/// Provides seed data for the PokerGameTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PokerGameTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Sessions.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPokerDataAsync(context);
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

    private static async Task SeedPokerDataAsync(PokerGameTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Bankrolls
        var bankrolls = new List<Bankroll>
        {
            new Bankroll
            {
                BankrollId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Amount = 1000.00m,
                RecordedDate = new DateTime(2024, 1, 1),
                Notes = "Initial bankroll",
                CreatedAt = DateTime.UtcNow,
            },
            new Bankroll
            {
                BankrollId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Amount = 1250.00m,
                RecordedDate = new DateTime(2024, 2, 1),
                Notes = "After first month",
                CreatedAt = DateTime.UtcNow,
            },
            new Bankroll
            {
                BankrollId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Amount = 1450.00m,
                RecordedDate = new DateTime(2024, 3, 1),
                Notes = "Steady growth",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Bankrolls.AddRange(bankrolls);

        // Seed Sessions
        var sessions = new List<Session>
        {
            new Session
            {
                SessionId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                GameType = GameType.TexasHoldem,
                StartTime = new DateTime(2024, 6, 15, 19, 0, 0),
                EndTime = new DateTime(2024, 6, 15, 23, 30, 0),
                BuyIn = 200.00m,
                CashOut = 350.00m,
                Location = "Local Casino",
                Notes = "Great session, played solid poker",
                CreatedAt = DateTime.UtcNow,
            },
            new Session
            {
                SessionId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                GameType = GameType.CashGame,
                StartTime = new DateTime(2024, 6, 20, 20, 0, 0),
                EndTime = new DateTime(2024, 6, 21, 1, 0, 0),
                BuyIn = 300.00m,
                CashOut = 250.00m,
                Location = "Home Game",
                Notes = "Lost on a bad beat",
                CreatedAt = DateTime.UtcNow,
            },
            new Session
            {
                SessionId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                GameType = GameType.Tournament,
                StartTime = new DateTime(2024, 7, 1, 15, 0, 0),
                EndTime = new DateTime(2024, 7, 1, 22, 0, 0),
                BuyIn = 100.00m,
                CashOut = 500.00m,
                Location = "Online Tournament",
                Notes = "Finished in 3rd place!",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Sessions.AddRange(sessions);

        // Seed Hands
        var hands = new List<Hand>
        {
            new Hand
            {
                HandId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SessionId = sessions[0].SessionId,
                StartingCards = "As Kd",
                PotSize = 85.00m,
                WasWon = true,
                Notes = "Flopped top pair, held up",
                CreatedAt = DateTime.UtcNow,
            },
            new Hand
            {
                HandId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                SessionId = sessions[0].SessionId,
                StartingCards = "Qh Qc",
                PotSize = 120.00m,
                WasWon = true,
                Notes = "Set on the flop, good action",
                CreatedAt = DateTime.UtcNow,
            },
            new Hand
            {
                HandId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                SessionId = sessions[1].SessionId,
                StartingCards = "Ah Ad",
                PotSize = 200.00m,
                WasWon = false,
                Notes = "Bad beat - opponent hit flush on river",
                CreatedAt = DateTime.UtcNow,
            },
            new Hand
            {
                HandId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                SessionId = sessions[2].SessionId,
                StartingCards = "Jh Jd",
                PotSize = 150.00m,
                WasWon = true,
                Notes = "All-in pre-flop, held against AK",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Hands.AddRange(hands);

        await context.SaveChangesAsync();
    }
}
