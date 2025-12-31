// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeInventoryManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HomeInventoryManager.Core.Model.UserAggregate;
using HomeInventoryManager.Core.Model.UserAggregate.Entities;
using HomeInventoryManager.Core.Services;
namespace HomeInventoryManager.Infrastructure;

/// <summary>
/// Provides seed data for the HomeInventoryManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeInventoryManagerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Items.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedItemsAsync(context);
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

    private static async Task SeedItemsAsync(HomeInventoryManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var items = new List<Item>
        {
            new Item
            {
                ItemId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Samsung 65\" 4K TV",
                Description = "65-inch 4K Smart TV with HDR",
                Category = Category.Electronics,
                Room = Room.LivingRoom,
                Brand = "Samsung",
                ModelNumber = "UN65RU8000",
                SerialNumber = "12345ABC",
                PurchaseDate = new DateTime(2023, 3, 15),
                PurchasePrice = 899.99m,
                CurrentValue = 650.00m,
                Quantity = 1,
                Notes = "Purchased at Best Buy, 2-year warranty",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new Item
            {
                ItemId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Leather Sofa",
                Description = "Brown leather 3-seater sofa",
                Category = Category.Furniture,
                Room = Room.LivingRoom,
                Brand = "Ashley Furniture",
                ModelNumber = "AF-5040",
                PurchaseDate = new DateTime(2022, 6, 20),
                PurchasePrice = 1499.99m,
                CurrentValue = 1200.00m,
                Quantity = 1,
                Notes = "In excellent condition",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new Item
            {
                ItemId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "KitchenAid Mixer",
                Description = "Stand mixer with multiple attachments",
                Category = Category.Appliances,
                Room = Room.Kitchen,
                Brand = "KitchenAid",
                ModelNumber = "KSM150PSER",
                SerialNumber = "KA789XYZ",
                PurchaseDate = new DateTime(2021, 12, 10),
                PurchasePrice = 379.99m,
                CurrentValue = 320.00m,
                Quantity = 1,
                PhotoUrl = "/photos/mixer.jpg",
                ReceiptUrl = "/receipts/mixer_receipt.pdf",
                Notes = "Empire Red color, includes whisk and dough hook",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new Item
            {
                ItemId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Power Drill Set",
                Description = "Cordless drill with bits and case",
                Category = Category.Tools,
                Room = Room.Garage,
                Brand = "DeWalt",
                ModelNumber = "DCD771C2",
                PurchaseDate = new DateTime(2023, 8, 5),
                PurchasePrice = 129.99m,
                CurrentValue = 110.00m,
                Quantity = 1,
                Notes = "20V MAX battery included",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new Item
            {
                ItemId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Vintage Watch Collection",
                Description = "Collection of 5 vintage watches",
                Category = Category.Jewelry,
                Room = Room.Bedroom,
                PurchaseDate = new DateTime(2020, 5, 1),
                PurchasePrice = 2500.00m,
                CurrentValue = 3200.00m,
                Quantity = 5,
                Notes = "Stored in safety deposit box",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
        };

        context.Items.AddRange(items);

        // Add value estimates for the TV
        var valueEstimates = new List<ValueEstimate>
        {
            new ValueEstimate
            {
                ValueEstimateId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ItemId = items[0].ItemId,
                EstimatedValue = 750.00m,
                EstimationDate = new DateTime(2023, 12, 1),
                Source = "Online Marketplace Research",
                Notes = "Based on similar listings",
                CreatedAt = DateTime.UtcNow,
            },
            new ValueEstimate
            {
                ValueEstimateId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ItemId = items[0].ItemId,
                EstimatedValue = 650.00m,
                EstimationDate = new DateTime(2024, 12, 1),
                Source = "Online Marketplace Research",
                Notes = "Value decreased due to newer models",
                CreatedAt = DateTime.UtcNow,
            },
            new ValueEstimate
            {
                ValueEstimateId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ItemId = items[4].ItemId,
                EstimatedValue = 3200.00m,
                EstimationDate = new DateTime(2024, 11, 15),
                Source = "Professional Appraisal",
                Notes = "Value increased due to rarity",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.ValueEstimates.AddRange(valueEstimates);

        await context.SaveChangesAsync();
    }
}
