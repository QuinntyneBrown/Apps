// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WineCellarInventory.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WineCellarInventory.Core.Models.UserAggregate;
using WineCellarInventory.Core.Models.UserAggregate.Entities;
using WineCellarInventory.Core.Services;
namespace WineCellarInventory.Infrastructure;

/// <summary>
/// Provides seed data for the WineCellarInventory database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(WineCellarInventoryContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Wines.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedWinesAsync(context);
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

    private static async Task SeedWinesAsync(WineCellarInventoryContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var wines = new List<Wine>
        {
            new Wine
            {
                WineId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Château Margaux",
                WineType = WineType.Red,
                Region = Region.Bordeaux,
                Vintage = 2015,
                Producer = "Château Margaux",
                PurchasePrice = 450.00m,
                BottleCount = 2,
                StorageLocation = "Cellar A - Rack 1",
                Notes = "Premier Grand Cru Classé, excellent investment wine",
                CreatedAt = DateTime.UtcNow,
            },
            new Wine
            {
                WineId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Cloudy Bay Sauvignon Blanc",
                WineType = WineType.White,
                Region = Region.NewZealand,
                Vintage = 2021,
                Producer = "Cloudy Bay Vineyards",
                PurchasePrice = 35.00m,
                BottleCount = 6,
                StorageLocation = "Cellar B - Rack 3",
                Notes = "Crisp and refreshing, perfect for summer",
                CreatedAt = DateTime.UtcNow,
            },
            new Wine
            {
                WineId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Dom Pérignon Vintage",
                WineType = WineType.Sparkling,
                Region = Region.Champagne,
                Vintage = 2010,
                Producer = "Moët & Chandon",
                PurchasePrice = 200.00m,
                BottleCount = 1,
                StorageLocation = "Cellar A - Rack 5",
                Notes = "Special occasion champagne",
                CreatedAt = DateTime.UtcNow,
            },
            new Wine
            {
                WineId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Barolo Riserva",
                WineType = WineType.Red,
                Region = Region.Piedmont,
                Vintage = 2013,
                Producer = "Pio Cesare",
                PurchasePrice = 95.00m,
                BottleCount = 4,
                StorageLocation = "Cellar A - Rack 2",
                Notes = "Bold and complex, needs more aging",
                CreatedAt = DateTime.UtcNow,
            },
            new Wine
            {
                WineId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Caymus Cabernet Sauvignon",
                WineType = WineType.Red,
                Region = Region.Napa,
                Vintage = 2018,
                Producer = "Caymus Vineyards",
                PurchasePrice = 85.00m,
                BottleCount = 3,
                StorageLocation = "Cellar B - Rack 1",
                Notes = "Rich and full-bodied California Cab",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Wines.AddRange(wines);

        // Add sample tasting note for the first wine
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            WineId = wines[0].WineId,
            TastingDate = DateTime.UtcNow.AddDays(-30),
            Rating = 95,
            Appearance = "Deep ruby red with purple hues",
            Aroma = "Complex notes of blackcurrant, cedar, and tobacco",
            Taste = "Full-bodied with layers of dark fruit, silky tannins",
            Finish = "Long, elegant finish with hints of vanilla and spice",
            OverallImpression = "Exceptional wine, showing great aging potential",
            CreatedAt = DateTime.UtcNow,
        };

        context.TastingNotes.Add(tastingNote);

        // Add sample drinking window for the Barolo
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            WineId = wines[3].WineId,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2033, 12, 31),
            Notes = "Optimal drinking window for this Barolo Riserva vintage",
            CreatedAt = DateTime.UtcNow,
        };

        context.DrinkingWindows.Add(drinkingWindow);

        await context.SaveChangesAsync();
    }
}
