// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using GroceryShoppingListApp.Core.Models.UserAggregate;
using GroceryShoppingListApp.Core.Models.UserAggregate.Entities;
using GroceryShoppingListApp.Core.Services;
namespace GroceryShoppingListApp.Infrastructure;

/// <summary>
/// Provides seed data for the GroceryShoppingListApp database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(GroceryShoppingListAppContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Stores.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGroceryDataAsync(context);
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

    private static async Task SeedGroceryDataAsync(GroceryShoppingListAppContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var stores = new List<Store>
        {
            new Store
            {
                StoreId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Whole Foods Market",
                Address = "123 Main St, Seattle, WA",
                CreatedAt = DateTime.UtcNow,
            },
            new Store
            {
                StoreId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Trader Joe's",
                Address = "456 Oak Ave, Seattle, WA",
                CreatedAt = DateTime.UtcNow,
            },
            new Store
            {
                StoreId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Safeway",
                Address = "789 Pine Blvd, Seattle, WA",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Stores.AddRange(stores);

        var groceryLists = new List<GroceryList>
        {
            new GroceryList
            {
                GroceryListId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Weekly Groceries",
                CreatedDate = DateTime.UtcNow.AddDays(-3),
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new GroceryList
            {
                GroceryListId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Party Supplies",
                CreatedDate = DateTime.UtcNow.AddDays(-7),
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.GroceryLists.AddRange(groceryLists);

        var groceryItems = new List<GroceryItem>
        {
            new GroceryItem
            {
                GroceryItemId = Guid.Parse("11111111-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                GroceryListId = groceryLists[0].GroceryListId,
                Name = "Milk",
                Category = Category.Dairy,
                Quantity = 2,
                IsChecked = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new GroceryItem
            {
                GroceryItemId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                GroceryListId = groceryLists[0].GroceryListId,
                Name = "Bread",
                Category = Category.Bakery,
                Quantity = 1,
                IsChecked = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new GroceryItem
            {
                GroceryItemId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                GroceryListId = groceryLists[0].GroceryListId,
                Name = "Chicken Breast",
                Category = Category.Meat,
                Quantity = 3,
                IsChecked = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new GroceryItem
            {
                GroceryItemId = Guid.Parse("44444444-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                GroceryListId = groceryLists[0].GroceryListId,
                Name = "Apples",
                Category = Category.Produce,
                Quantity = 5,
                IsChecked = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new GroceryItem
            {
                GroceryItemId = Guid.Parse("55555555-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                GroceryListId = groceryLists[1].GroceryListId,
                Name = "Chips",
                Category = Category.Snacks,
                Quantity = 4,
                IsChecked = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.GroceryItems.AddRange(groceryItems);

        var priceHistories = new List<PriceHistory>
        {
            new PriceHistory
            {
                PriceHistoryId = Guid.Parse("11111111-cccc-cccc-cccc-cccccccccccc"),
                GroceryItemId = groceryItems[0].GroceryItemId,
                StoreId = stores[0].StoreId,
                Price = 4.99m,
                Date = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new PriceHistory
            {
                PriceHistoryId = Guid.Parse("22222222-cccc-cccc-cccc-cccccccccccc"),
                GroceryItemId = groceryItems[0].GroceryItemId,
                StoreId = stores[1].StoreId,
                Price = 4.49m,
                Date = DateTime.UtcNow.AddDays(-5),
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new PriceHistory
            {
                PriceHistoryId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                GroceryItemId = groceryItems[1].GroceryItemId,
                StoreId = stores[0].StoreId,
                Price = 3.99m,
                Date = DateTime.UtcNow.AddDays(-8),
                CreatedAt = DateTime.UtcNow.AddDays(-8),
            },
        };

        context.PriceHistories.AddRange(priceHistories);

        await context.SaveChangesAsync();
    }
}
