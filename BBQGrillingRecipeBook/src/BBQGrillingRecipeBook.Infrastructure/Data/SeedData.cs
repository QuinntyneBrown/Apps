// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BBQGrillingRecipeBook.Infrastructure;

/// <summary>
/// Provides seed data for the BBQGrillingRecipeBook database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(BBQGrillingRecipeBookContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Recipes.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedRecipesAsync(context);
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

    private static async Task SeedRecipesAsync(BBQGrillingRecipeBookContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var recipes = new List<Recipe>
        {
            new Recipe
            {
                RecipeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Classic BBQ Ribs",
                Description = "Tender, fall-off-the-bone pork ribs with a sweet and tangy BBQ sauce",
                MeatType = MeatType.Pork,
                CookingMethod = CookingMethod.SlowAndLow,
                PrepTimeMinutes = 30,
                CookTimeMinutes = 240,
                Ingredients = "Baby back ribs, BBQ sauce, dry rub (paprika, brown sugar, garlic powder, onion powder, salt, pepper)",
                Instructions = "1. Apply dry rub to ribs. 2. Smoke at 225°F for 3 hours. 3. Wrap in foil for 1 hour. 4. Unwrap, apply sauce, cook 30 minutes.",
                TargetTemperature = 225,
                Servings = 4,
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Grilled Chicken Breast",
                Description = "Juicy grilled chicken breast with herbs",
                MeatType = MeatType.Chicken,
                CookingMethod = CookingMethod.DirectGrilling,
                PrepTimeMinutes = 15,
                CookTimeMinutes = 20,
                Ingredients = "Chicken breasts, olive oil, garlic, rosemary, thyme, lemon, salt, pepper",
                Instructions = "1. Marinate chicken for 30 min. 2. Grill over medium-high heat 6-8 minutes per side. 3. Rest 5 minutes before serving.",
                TargetTemperature = 375,
                Servings = 4,
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Smoked Brisket",
                Description = "Texas-style smoked beef brisket",
                MeatType = MeatType.Beef,
                CookingMethod = CookingMethod.Smoking,
                PrepTimeMinutes = 45,
                CookTimeMinutes = 720,
                Ingredients = "Whole packer brisket (12-14 lbs), kosher salt, black pepper, oak or hickory wood",
                Instructions = "1. Trim brisket. 2. Season with salt and pepper. 3. Smoke at 250°F until internal temp reaches 203°F (about 12 hours). 4. Rest 1 hour before slicing.",
                TargetTemperature = 250,
                Servings = 12,
                Notes = "Low and slow is the key. Don't rush it!",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Recipes.AddRange(recipes);

        // Add sample techniques
        var techniques = new List<Technique>
        {
            new Technique
            {
                TechniqueId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "The 3-2-1 Method for Ribs",
                Description = "A popular method for smoking perfect ribs every time",
                Category = "Smoking",
                DifficultyLevel = 2,
                Instructions = "1. Smoke unwrapped for 3 hours. 2. Wrap in foil and smoke for 2 hours. 3. Unwrap, apply sauce, smoke for 1 hour.",
                Tips = "Use apple or cherry wood for a sweeter smoke flavor. Maintain consistent temperature.",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Technique
            {
                TechniqueId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Reverse Searing",
                Description = "Technique for perfect steaks with a great crust",
                Category = "Grilling",
                DifficultyLevel = 3,
                Instructions = "1. Cook steak low and slow until 10-15°F below target temp. 2. Rest 10 minutes. 3. Sear over high heat 1-2 minutes per side.",
                Tips = "Works best with thick-cut steaks (1.5 inches or more). Use a meat thermometer for precision.",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Techniques.AddRange(techniques);

        // Add sample cook sessions
        var cookSessions = new List<CookSession>
        {
            new CookSession
            {
                CookSessionId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[0].RecipeId,
                CookDate = new DateTime(2024, 7, 4),
                ActualCookTimeMinutes = 250,
                TemperatureUsed = 225,
                Rating = 5,
                Notes = "Perfect for July 4th! Everyone loved them.",
                WasSuccessful = true,
                CreatedAt = DateTime.UtcNow,
            },
            new CookSession
            {
                CookSessionId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[2].RecipeId,
                CookDate = new DateTime(2024, 9, 15),
                ActualCookTimeMinutes = 750,
                TemperatureUsed = 250,
                Rating = 4,
                Notes = "Took longer than expected but turned out great",
                Modifications = "Added a bit more pepper to the rub",
                WasSuccessful = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.CookSessions.AddRange(cookSessions);

        await context.SaveChangesAsync();
    }
}
