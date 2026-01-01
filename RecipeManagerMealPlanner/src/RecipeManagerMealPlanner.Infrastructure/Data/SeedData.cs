// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using RecipeManagerMealPlanner.Core.Model.UserAggregate;
using RecipeManagerMealPlanner.Core.Model.UserAggregate.Entities;
using RecipeManagerMealPlanner.Core.Services;
namespace RecipeManagerMealPlanner.Infrastructure;

/// <summary>
/// Provides seed data for the RecipeManagerMealPlanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RecipeManagerMealPlannerContext context, ILogger logger, IPasswordHasher passwordHasher)
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

    private static async Task SeedRecipesAsync(RecipeManagerMealPlannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var recipes = new List<Recipe>
        {
            new Recipe
            {
                RecipeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Classic Spaghetti Carbonara",
                Description = "Traditional Italian pasta dish with eggs, cheese, and pancetta",
                Cuisine = Cuisine.Italian,
                DifficultyLevel = DifficultyLevel.Medium,
                PrepTimeMinutes = 10,
                CookTimeMinutes = 20,
                Servings = 4,
                Instructions = "1. Cook spaghetti according to package directions. 2. Fry pancetta until crispy. 3. Beat eggs with cheese. 4. Combine hot pasta with pancetta, then add egg mixture off heat. 5. Toss quickly and serve immediately.",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Chicken Tikka Masala",
                Description = "Creamy tomato-based curry with marinated chicken",
                Cuisine = Cuisine.Indian,
                DifficultyLevel = DifficultyLevel.Hard,
                PrepTimeMinutes = 30,
                CookTimeMinutes = 40,
                Servings = 6,
                Instructions = "1. Marinate chicken in yogurt and spices. 2. Grill chicken pieces. 3. Prepare tomato-cream sauce with spices. 4. Simmer chicken in sauce. 5. Serve with rice and naan.",
                Rating = 5,
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Recipe
            {
                RecipeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Caesar Salad",
                Description = "Fresh romaine lettuce with parmesan and croutons",
                Cuisine = Cuisine.American,
                DifficultyLevel = DifficultyLevel.Easy,
                PrepTimeMinutes = 15,
                CookTimeMinutes = 0,
                Servings = 2,
                Instructions = "1. Wash and chop romaine lettuce. 2. Make dressing with anchovies, garlic, lemon, and oil. 3. Toss lettuce with dressing. 4. Top with parmesan and croutons.",
                Rating = 4,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Recipes.AddRange(recipes);

        // Add ingredients for first recipe
        var ingredients = new List<Ingredient>
        {
            new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                RecipeId = recipes[0].RecipeId,
                Name = "Spaghetti",
                Quantity = "400",
                Unit = "g",
                CreatedAt = DateTime.UtcNow,
            },
            new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                RecipeId = recipes[0].RecipeId,
                Name = "Pancetta",
                Quantity = "200",
                Unit = "g",
                CreatedAt = DateTime.UtcNow,
            },
            new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                RecipeId = recipes[0].RecipeId,
                Name = "Eggs",
                Quantity = "4",
                Unit = "whole",
                CreatedAt = DateTime.UtcNow,
            },
            new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                RecipeId = recipes[0].RecipeId,
                Name = "Parmesan Cheese",
                Quantity = "100",
                Unit = "g",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Ingredients.AddRange(ingredients);

        // Add sample meal plan
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            UserId = sampleUserId,
            Name = "Friday Dinner",
            MealDate = DateTime.UtcNow.Date.AddDays(1),
            MealType = "Dinner",
            RecipeId = recipes[0].RecipeId,
            IsPrepared = false,
            CreatedAt = DateTime.UtcNow,
        };

        context.MealPlans.Add(mealPlan);

        // Add sample shopping list
        var shoppingList = new ShoppingList
        {
            ShoppingListId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            UserId = sampleUserId,
            Name = "Weekly Groceries",
            Items = "Spaghetti, Pancetta, Eggs, Parmesan Cheese, Chicken, Tomatoes, Cream",
            CreatedDate = DateTime.UtcNow.Date,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        context.ShoppingLists.Add(shoppingList);

        await context.SaveChangesAsync();
    }
}
