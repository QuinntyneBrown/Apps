// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MealPrepPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MealPrepPlanner.Core.Models.UserAggregate;
using MealPrepPlanner.Core.Models.UserAggregate.Entities;
using MealPrepPlanner.Core.Services;
namespace MealPrepPlanner.Infrastructure;

/// <summary>
/// Provides seed data for the MealPrepPlanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MealPrepPlannerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.MealPlans.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMealPlansAsync(context);
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

    private static async Task SeedMealPlansAsync(MealPrepPlannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create meal plans
        var mealPlans = new List<MealPlan>
        {
            new MealPlan
            {
                MealPlanId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Weekly Healthy Eating",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date.AddDays(7),
                DailyCalorieTarget = 2000,
                DietaryPreferences = "Balanced, High Protein",
                IsActive = true,
                Notes = "Focus on lean proteins and vegetables",
                CreatedAt = DateTime.UtcNow,
            },
            new MealPlan
            {
                MealPlanId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Vegetarian Week",
                StartDate = DateTime.UtcNow.Date.AddDays(7),
                EndDate = DateTime.UtcNow.Date.AddDays(14),
                DailyCalorieTarget = 1800,
                DietaryPreferences = "Vegetarian, High Fiber",
                IsActive = true,
                Notes = "Plant-based protein sources",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.MealPlans.AddRange(mealPlans);

        // Create recipes
        var recipes = new List<Recipe>
        {
            new Recipe
            {
                RecipeId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MealPlanId = mealPlans[0].MealPlanId,
                Name = "Grilled Chicken Breast with Vegetables",
                Description = "Simple and healthy grilled chicken with roasted vegetables",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 25,
                Servings = 4,
                Ingredients = "[{\"item\":\"Chicken breast\",\"quantity\":\"4 pieces\"},{\"item\":\"Broccoli\",\"quantity\":\"2 cups\"},{\"item\":\"Olive oil\",\"quantity\":\"2 tbsp\"}]",
                Instructions = "1. Season chicken with salt and pepper\n2. Grill chicken for 6-7 minutes per side\n3. Roast vegetables at 400°F for 20 minutes",
                MealType = "Dinner",
                Tags = "High Protein, Low Carb",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MealPlanId = mealPlans[0].MealPlanId,
                Name = "Overnight Oats",
                Description = "Quick breakfast prep for busy mornings",
                PrepTimeMinutes = 5,
                CookTimeMinutes = 0,
                Servings = 1,
                Ingredients = "[{\"item\":\"Rolled oats\",\"quantity\":\"1/2 cup\"},{\"item\":\"Milk\",\"quantity\":\"1/2 cup\"},{\"item\":\"Berries\",\"quantity\":\"1/4 cup\"}]",
                Instructions = "1. Mix oats and milk in jar\n2. Add berries and sweetener if desired\n3. Refrigerate overnight",
                MealType = "Breakfast",
                Tags = "Quick, Vegetarian",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MealPlanId = mealPlans[1].MealPlanId,
                Name = "Lentil Curry",
                Description = "Hearty vegetarian curry packed with protein",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 30,
                Servings = 6,
                Ingredients = "[{\"item\":\"Red lentils\",\"quantity\":\"2 cups\"},{\"item\":\"Coconut milk\",\"quantity\":\"1 can\"},{\"item\":\"Curry powder\",\"quantity\":\"2 tbsp\"}]",
                Instructions = "1. Sauté onions and garlic\n2. Add lentils and spices\n3. Simmer with coconut milk for 30 minutes",
                MealType = "Dinner",
                Tags = "Vegetarian, High Fiber",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Recipes.AddRange(recipes);

        // Create nutrition data
        var nutritions = new List<Nutrition>
        {
            new Nutrition
            {
                NutritionId = Guid.Parse("11111111-1111-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[0].RecipeId,
                Calories = 280,
                Protein = 42,
                Carbohydrates = 12,
                Fat = 8,
                Fiber = 4,
                Sugar = 3,
                Sodium = 420,
                CreatedAt = DateTime.UtcNow,
            },
            new Nutrition
            {
                NutritionId = Guid.Parse("22222222-2222-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipeId = recipes[1].RecipeId,
                Calories = 320,
                Protein = 12,
                Carbohydrates = 52,
                Fat = 8,
                Fiber = 8,
                Sugar = 12,
                Sodium = 85,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Nutritions.AddRange(nutritions);

        // Create grocery list
        var groceryLists = new List<GroceryList>
        {
            new GroceryList
            {
                GroceryListId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MealPlanId = mealPlans[0].MealPlanId,
                Name = "Weekly Shopping - Healthy Eating",
                Items = "[{\"item\":\"Chicken breast\",\"quantity\":\"2 lbs\",\"checked\":false},{\"item\":\"Broccoli\",\"quantity\":\"1 lb\",\"checked\":false},{\"item\":\"Rolled oats\",\"quantity\":\"1 container\",\"checked\":false}]",
                ShoppingDate = DateTime.UtcNow.Date.AddDays(1),
                IsCompleted = false,
                EstimatedCost = 45.50m,
                Notes = "Check for sales on chicken",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.GroceryLists.AddRange(groceryLists);

        await context.SaveChangesAsync();
    }
}
