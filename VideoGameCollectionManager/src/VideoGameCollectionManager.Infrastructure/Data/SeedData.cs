// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using VideoGameCollectionManager.Core.Model.UserAggregate;
using VideoGameCollectionManager.Core.Model.UserAggregate.Entities;
using VideoGameCollectionManager.Core.Services;
namespace VideoGameCollectionManager.Infrastructure;

/// <summary>
/// Provides seed data for the VideoGameCollectionManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(VideoGameCollectionManagerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Games.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGamesAsync(context);
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

    private static async Task SeedGamesAsync(VideoGameCollectionManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var game1 = new Game
        {
            GameId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Title = "The Legend of Zelda: Breath of the Wild",
            Platform = Platform.NintendoSwitch,
            Genre = Genre.Adventure,
            Status = CompletionStatus.Completed,
            Publisher = "Nintendo",
            Developer = "Nintendo EPD",
            ReleaseDate = new DateTime(2017, 3, 3),
            PurchaseDate = new DateTime(2017, 3, 10),
            PurchasePrice = 59.99m,
            Rating = 10,
            Notes = "One of the best games ever made",
            CreatedAt = DateTime.UtcNow,
        };

        var game2 = new Game
        {
            GameId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            Title = "Elden Ring",
            Platform = Platform.PlayStation5,
            Genre = Genre.RPG,
            Status = CompletionStatus.InProgress,
            Publisher = "Bandai Namco",
            Developer = "FromSoftware",
            ReleaseDate = new DateTime(2022, 2, 25),
            PurchaseDate = new DateTime(2022, 3, 1),
            PurchasePrice = 59.99m,
            Rating = 9,
            Notes = "Challenging but rewarding",
            CreatedAt = DateTime.UtcNow,
        };

        var game3 = new Game
        {
            GameId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            UserId = sampleUserId,
            Title = "Halo Infinite",
            Platform = Platform.XboxSeriesX,
            Genre = Genre.Shooter,
            Status = CompletionStatus.NotStarted,
            Publisher = "Xbox Game Studios",
            Developer = "343 Industries",
            ReleaseDate = new DateTime(2021, 12, 8),
            PurchaseDate = new DateTime(2024, 11, 15),
            PurchasePrice = 39.99m,
            Notes = "On sale, planning to start soon",
            CreatedAt = DateTime.UtcNow,
        };

        context.Games.AddRange(game1, game2, game3);

        // Add play sessions for game 1
        var playSession1 = new PlaySession
        {
            PlaySessionId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            GameId = game1.GameId,
            StartTime = new DateTime(2024, 11, 1, 19, 0, 0),
            EndTime = new DateTime(2024, 11, 1, 22, 30, 0),
            DurationMinutes = 210,
            Notes = "Explored the Hebra region",
            CreatedAt = DateTime.UtcNow,
        };

        var playSession2 = new PlaySession
        {
            PlaySessionId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            GameId = game1.GameId,
            StartTime = new DateTime(2024, 11, 5, 18, 0, 0),
            EndTime = new DateTime(2024, 11, 5, 20, 45, 0),
            DurationMinutes = 165,
            Notes = "Completed Divine Beast Vah Ruta",
            CreatedAt = DateTime.UtcNow,
        };

        var playSession3 = new PlaySession
        {
            PlaySessionId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            GameId = game2.GameId,
            StartTime = new DateTime(2024, 12, 1, 20, 0, 0),
            EndTime = new DateTime(2024, 12, 1, 23, 0, 0),
            DurationMinutes = 180,
            Notes = "Defeated Margit the Fell Omen",
            CreatedAt = DateTime.UtcNow,
        };

        context.PlaySessions.AddRange(playSession1, playSession2, playSession3);

        // Add wishlist items
        var wishlist1 = new Wishlist
        {
            WishlistId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
            UserId = sampleUserId,
            Title = "Baldur's Gate 3",
            Platform = Platform.PC,
            Genre = Genre.RPG,
            Priority = 1,
            Notes = "Waiting for sale",
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        var wishlist2 = new Wishlist
        {
            WishlistId = Guid.Parse("bbbbbbbb-2222-2222-2222-222222222222"),
            UserId = sampleUserId,
            Title = "Spider-Man 2",
            Platform = Platform.PlayStation5,
            Genre = Genre.Action,
            Priority = 2,
            Notes = "Birthday present idea",
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        var wishlist3 = new Wishlist
        {
            WishlistId = Guid.Parse("cccccccc-3333-3333-3333-333333333333"),
            UserId = sampleUserId,
            Title = "Starfield",
            Platform = Platform.XboxSeriesX,
            Genre = Genre.RPG,
            Priority = 3,
            Notes = "Looks interesting",
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        context.Wishlists.AddRange(wishlist1, wishlist2, wishlist3);

        await context.SaveChangesAsync();
    }
}
