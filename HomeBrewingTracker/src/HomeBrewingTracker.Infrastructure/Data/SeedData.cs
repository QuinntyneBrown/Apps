// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HomeBrewingTracker.Core.Model.UserAggregate;
using HomeBrewingTracker.Core.Model.UserAggregate.Entities;
using HomeBrewingTracker.Core.Services;
namespace HomeBrewingTracker.Infrastructure;

/// <summary>
/// Provides seed data for the HomeBrewingTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeBrewingTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Recipes.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedBrewingDataAsync(context);
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

    private static async Task SeedBrewingDataAsync(HomeBrewingTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var recipes = new List<Recipe>
        {
            new Recipe
            {
                RecipeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "West Coast IPA",
                BeerStyle = BeerStyle.IPA,
                Description = "A classic hoppy West Coast style IPA",
                OriginalGravity = 1.065m,
                FinalGravity = 1.012m,
                ABV = 7.0m,
                IBU = 65,
                BatchSize = 5.0m,
                Ingredients = "Pale malt, Cascade hops, Centennial hops, American Ale yeast",
                Instructions = "Mash at 152F for 60 minutes. Boil for 60 minutes with hop additions at 60, 30, and 5 minutes.",
                Notes = "Dry hop with 2oz Cascade for 5 days",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
            },
            new Recipe
            {
                RecipeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Irish Stout",
                BeerStyle = BeerStyle.Stout,
                Description = "A smooth and roasty Irish dry stout",
                OriginalGravity = 1.048m,
                FinalGravity = 1.011m,
                ABV = 4.8m,
                IBU = 35,
                BatchSize = 5.0m,
                Ingredients = "Pale malt, Roasted barley, Flaked barley, East Kent Goldings hops, Irish Ale yeast",
                Instructions = "Mash at 154F for 60 minutes. Boil for 60 minutes.",
                Notes = "Condition with nitrogen for authentic pour",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow.AddMonths(-4),
            },
            new Recipe
            {
                RecipeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Belgian Tripel",
                BeerStyle = BeerStyle.Belgian,
                Description = "A complex and strong Belgian ale",
                OriginalGravity = 1.080m,
                FinalGravity = 1.010m,
                ABV = 9.2m,
                IBU = 30,
                BatchSize = 5.0m,
                Ingredients = "Pilsner malt, Belgian candy sugar, Saaz hops, Belgian Abbey yeast",
                Instructions = "Mash at 148F for 90 minutes. Boil for 90 minutes.",
                Notes = "Ferment at 68-72F for best flavor development",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
            },
        };

        context.Recipes.AddRange(recipes);

        var batches = new List<Batch>
        {
            new Batch
            {
                BatchId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[0].RecipeId,
                BatchNumber = "IPA-001",
                Status = BatchStatus.Completed,
                BrewDate = DateTime.UtcNow.AddDays(-60),
                BottlingDate = DateTime.UtcNow.AddDays(-30),
                ActualOriginalGravity = 1.066m,
                ActualFinalGravity = 1.013m,
                ActualABV = 7.0m,
                Notes = "Great hop aroma, slightly higher OG than expected",
                CreatedAt = DateTime.UtcNow.AddDays(-60),
            },
            new Batch
            {
                BatchId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[1].RecipeId,
                BatchNumber = "STOUT-001",
                Status = BatchStatus.Fermenting,
                BrewDate = DateTime.UtcNow.AddDays(-14),
                ActualOriginalGravity = 1.047m,
                Notes = "Beautiful dark color, fermentation is active",
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new Batch
            {
                BatchId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[2].RecipeId,
                BatchNumber = "TRIPEL-001",
                Status = BatchStatus.Bottled,
                BrewDate = DateTime.UtcNow.AddDays(-45),
                BottlingDate = DateTime.UtcNow.AddDays(-14),
                ActualOriginalGravity = 1.082m,
                ActualFinalGravity = 1.011m,
                ActualABV = 9.4m,
                Notes = "Strong fermentation, needs 3-4 weeks conditioning",
                CreatedAt = DateTime.UtcNow.AddDays(-45),
            },
        };

        context.Batches.AddRange(batches);

        var tastingNotes = new List<TastingNote>
        {
            new TastingNote
            {
                TastingNoteId = Guid.Parse("11111111-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                BatchId = batches[0].BatchId,
                TastingDate = DateTime.UtcNow.AddDays(-10),
                Rating = 5,
                Appearance = "Clear golden color with thick white head",
                Aroma = "Citrus and pine with subtle malt sweetness",
                Flavor = "Hoppy bitterness balanced with malt backbone",
                Mouthfeel = "Medium body with high carbonation",
                OverallImpression = "Excellent West Coast IPA, one of my best batches!",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new TastingNote
            {
                TastingNoteId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                BatchId = batches[0].BatchId,
                TastingDate = DateTime.UtcNow.AddDays(-5),
                Rating = 5,
                Appearance = "Clear golden, persistent head",
                Aroma = "Still very hoppy, citrus dominant",
                Flavor = "Great hop character, clean finish",
                Mouthfeel = "Perfect carbonation level",
                OverallImpression = "Getting better with age, perfect carbonation now",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
        };

        context.TastingNotes.AddRange(tastingNotes);

        await context.SaveChangesAsync();
    }
}
