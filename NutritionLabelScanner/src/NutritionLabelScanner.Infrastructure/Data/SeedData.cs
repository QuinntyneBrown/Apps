// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NutritionLabelScanner.Core.Models.UserAggregate;
using NutritionLabelScanner.Core.Models.UserAggregate.Entities;
using NutritionLabelScanner.Core.Services;
namespace NutritionLabelScanner.Infrastructure;

/// <summary>
/// Provides seed data for the NutritionLabelScanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(NutritionLabelScannerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Products.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedProductDataAsync(context);
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

    private static async Task SeedProductDataAsync(NutritionLabelScannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var product1 = new Product
        {
            ProductId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "Organic Whole Milk",
            Brand = "Happy Farms",
            Barcode = "012345678901",
            Category = "Dairy",
            ServingSize = "1 cup (240ml)",
            ScannedAt = DateTime.UtcNow,
            Notes = "Local organic dairy",
            CreatedAt = DateTime.UtcNow,
        };

        var product2 = new Product
        {
            ProductId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            Name = "Granola Bars",
            Brand = "Nature's Best",
            Barcode = "098765432109",
            Category = "Snacks",
            ServingSize = "1 bar (40g)",
            ScannedAt = DateTime.UtcNow,
            Notes = "Honey oat flavor",
            CreatedAt = DateTime.UtcNow,
        };

        context.Products.AddRange(product1, product2);

        var nutritionInfo1 = new NutritionInfo
        {
            NutritionInfoId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            ProductId = product1.ProductId,
            Calories = 150,
            TotalFat = 8m,
            SaturatedFat = 5m,
            TransFat = 0m,
            Cholesterol = 35m,
            Sodium = 120m,
            TotalCarbohydrates = 12m,
            DietaryFiber = 0m,
            TotalSugars = 12m,
            Protein = 8m,
            CreatedAt = DateTime.UtcNow,
        };

        var nutritionInfo2 = new NutritionInfo
        {
            NutritionInfoId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            ProductId = product2.ProductId,
            Calories = 180,
            TotalFat = 6m,
            SaturatedFat = 1m,
            TransFat = 0m,
            Cholesterol = 0m,
            Sodium = 95m,
            TotalCarbohydrates = 28m,
            DietaryFiber = 3m,
            TotalSugars = 12m,
            Protein = 4m,
            CreatedAt = DateTime.UtcNow,
        };

        context.NutritionInfos.AddRange(nutritionInfo1, nutritionInfo2);

        var comparison = new Comparison
        {
            ComparisonId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            UserId = sampleUserId,
            Name = "Breakfast Options Comparison",
            ProductIds = $"[\"{product1.ProductId}\",\"{product2.ProductId}\"]",
            Results = "Both products are healthy options with moderate calories",
            WinnerProductId = product1.ProductId,
            CreatedAt = DateTime.UtcNow,
        };

        context.Comparisons.Add(comparison);

        await context.SaveChangesAsync();
    }
}
