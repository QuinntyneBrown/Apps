// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WarrantyReturnPeriodTracker.Infrastructure;

/// <summary>
/// Provides seed data for the WarrantyReturnPeriodTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(WarrantyReturnPeriodTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Purchases.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPurchasesAsync(context);
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

    private static async Task SeedPurchasesAsync(WarrantyReturnPeriodTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var purchase1 = new Purchase
        {
            PurchaseId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            ProductName = "MacBook Pro 16-inch",
            Category = ProductCategory.Electronics,
            StoreName = "Apple Store",
            PurchaseDate = new DateTime(2024, 6, 15),
            Price = 2499.99m,
            Status = PurchaseStatus.Active,
            ModelNumber = "MK1E3LL/A",
            Notes = "M3 Max chip, 36GB RAM, 1TB SSD",
            CreatedAt = DateTime.UtcNow,
        };

        var purchase2 = new Purchase
        {
            PurchaseId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            ProductName = "Sony WH-1000XM5 Headphones",
            Category = ProductCategory.Electronics,
            StoreName = "Best Buy",
            PurchaseDate = new DateTime(2024, 11, 20),
            Price = 399.99m,
            Status = PurchaseStatus.Active,
            ModelNumber = "WH1000XM5/B",
            Notes = "Black color, excellent noise cancellation",
            CreatedAt = DateTime.UtcNow,
        };

        var purchase3 = new Purchase
        {
            PurchaseId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            UserId = sampleUserId,
            ProductName = "Samsung 65-inch QLED TV",
            Category = ProductCategory.HomeAppliances,
            StoreName = "Costco",
            PurchaseDate = new DateTime(2024, 10, 1),
            Price = 1299.99m,
            Status = PurchaseStatus.Active,
            ModelNumber = "QN65Q80C",
            Notes = "4K, 120Hz, includes wall mount",
            CreatedAt = DateTime.UtcNow,
        };

        context.Purchases.AddRange(purchase1, purchase2, purchase3);

        // Add warranties for purchases
        var warranty1 = new Warranty
        {
            WarrantyId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            PurchaseId = purchase1.PurchaseId,
            WarrantyType = WarrantyType.Manufacturer,
            Provider = "Apple Inc.",
            StartDate = new DateTime(2024, 6, 15),
            EndDate = new DateTime(2025, 6, 15),
            DurationMonths = 12,
            Status = WarrantyStatus.Active,
            CoverageDetails = "Hardware defects and manufacturing issues",
            Terms = "Does not cover accidental damage or liquid damage",
            RegistrationNumber = "APPL-WAR-2024-12345",
        };

        var warranty2 = new Warranty
        {
            WarrantyId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            PurchaseId = purchase1.PurchaseId,
            WarrantyType = WarrantyType.Extended,
            Provider = "AppleCare+",
            StartDate = new DateTime(2024, 6, 15),
            EndDate = new DateTime(2027, 6, 15),
            DurationMonths = 36,
            Status = WarrantyStatus.Active,
            CoverageDetails = "Covers accidental damage, battery service, and priority support",
            RegistrationNumber = "APPL-CARE-2024-67890",
        };

        var warranty3 = new Warranty
        {
            WarrantyId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            PurchaseId = purchase2.PurchaseId,
            WarrantyType = WarrantyType.Manufacturer,
            Provider = "Sony Electronics",
            StartDate = new DateTime(2024, 11, 20),
            EndDate = new DateTime(2025, 11, 20),
            DurationMonths = 12,
            Status = WarrantyStatus.Active,
            CoverageDetails = "Covers defects in materials and workmanship",
        };

        var warranty4 = new Warranty
        {
            WarrantyId = Guid.Parse("44444444-cccc-cccc-cccc-cccccccccccc"),
            PurchaseId = purchase3.PurchaseId,
            WarrantyType = WarrantyType.Manufacturer,
            Provider = "Samsung",
            StartDate = new DateTime(2024, 10, 1),
            EndDate = new DateTime(2025, 10, 1),
            DurationMonths = 12,
            Status = WarrantyStatus.Active,
            CoverageDetails = "Covers parts and labor for defects",
        };

        context.Warranties.AddRange(warranty1, warranty2, warranty3, warranty4);

        // Add return windows
        var returnWindow1 = new ReturnWindow
        {
            ReturnWindowId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            PurchaseId = purchase1.PurchaseId,
            StartDate = new DateTime(2024, 6, 15),
            EndDate = new DateTime(2024, 7, 29),
            DurationDays = 14,
            Status = ReturnWindowStatus.Expired,
            PolicyDetails = "Apple 14-day return policy",
            Conditions = "Product must be in original packaging with all accessories",
        };

        var returnWindow2 = new ReturnWindow
        {
            ReturnWindowId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            PurchaseId = purchase2.PurchaseId,
            StartDate = new DateTime(2024, 11, 20),
            EndDate = new DateTime(2025, 1, 14),
            DurationDays = 15,
            Status = ReturnWindowStatus.Open,
            PolicyDetails = "Best Buy extended holiday return policy",
            Conditions = "Must have receipt and be in original condition",
            RestockingFeePercent = 15,
        };

        var returnWindow3 = new ReturnWindow
        {
            ReturnWindowId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            PurchaseId = purchase3.PurchaseId,
            StartDate = new DateTime(2024, 10, 1),
            EndDate = new DateTime(2024, 12, 31),
            DurationDays = 90,
            Status = ReturnWindowStatus.Open,
            PolicyDetails = "Costco 90-day return policy on electronics",
            Conditions = "Full refund with receipt and membership verification",
            Notes = "No restocking fee at Costco",
        };

        context.ReturnWindows.AddRange(returnWindow1, returnWindow2, returnWindow3);

        // Add receipts
        var receipt1 = new Receipt
        {
            ReceiptId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
            PurchaseId = purchase1.PurchaseId,
            ReceiptNumber = "APPL-2024061512345",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.PDF,
            StorageLocation = "/receipts/apple_macbook_20240615.pdf",
            ReceiptDate = new DateTime(2024, 6, 15),
            StoreName = "Apple Store",
            TotalAmount = 2499.99m,
            PaymentMethod = PaymentMethod.CreditCard,
            Status = ReceiptStatus.Active,
            IsVerified = true,
            UploadedAt = DateTime.UtcNow,
        };

        var receipt2 = new Receipt
        {
            ReceiptId = Guid.Parse("bbbbbbbb-2222-2222-2222-222222222222"),
            PurchaseId = purchase2.PurchaseId,
            ReceiptNumber = "BB-202411201234",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.Image,
            StorageLocation = "/receipts/bestbuy_headphones_20241120.jpg",
            ReceiptDate = new DateTime(2024, 11, 20),
            StoreName = "Best Buy",
            TotalAmount = 399.99m,
            PaymentMethod = PaymentMethod.DebitCard,
            Status = ReceiptStatus.Active,
            IsVerified = true,
            UploadedAt = DateTime.UtcNow,
        };

        var receipt3 = new Receipt
        {
            ReceiptId = Guid.Parse("cccccccc-3333-3333-3333-333333333333"),
            PurchaseId = purchase3.PurchaseId,
            ReceiptNumber = "COST-20241001-987654",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.PDF,
            StorageLocation = "/receipts/costco_tv_20241001.pdf",
            ReceiptDate = new DateTime(2024, 10, 1),
            StoreName = "Costco",
            TotalAmount = 1299.99m,
            PaymentMethod = PaymentMethod.CreditCard,
            Status = ReceiptStatus.Active,
            IsVerified = true,
            Notes = "Includes extended warranty purchase",
            UploadedAt = DateTime.UtcNow,
        };

        context.Receipts.AddRange(receipt1, receipt2, receipt3);

        await context.SaveChangesAsync();
    }
}
