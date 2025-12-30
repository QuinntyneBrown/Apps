// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the WarrantyReturnPeriodTrackerContext.
/// </summary>
[TestFixture]
public class WarrantyReturnPeriodTrackerContextTests
{
    private WarrantyReturnPeriodTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WarrantyReturnPeriodTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WarrantyReturnPeriodTrackerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Purchases can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Purchases_CanAddAndRetrieve()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "MacBook Pro",
            Category = ProductCategory.Electronics,
            StoreName = "Apple Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 2499.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Purchases.FindAsync(purchase.PurchaseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ProductName, Is.EqualTo("MacBook Pro"));
        Assert.That(retrieved.Category, Is.EqualTo(ProductCategory.Electronics));
        Assert.That(retrieved.Price, Is.EqualTo(2499.99m));
    }

    /// <summary>
    /// Tests that Receipts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Receipts_CanAddAndRetrieve()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Headphones",
            Category = ProductCategory.Electronics,
            StoreName = "Best Buy",
            PurchaseDate = DateTime.UtcNow,
            Price = 399.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            ReceiptNumber = "REC-12345",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.PDF,
            ReceiptDate = DateTime.UtcNow,
            StoreName = "Best Buy",
            TotalAmount = 399.99m,
            PaymentMethod = PaymentMethod.CreditCard,
            Status = ReceiptStatus.Active,
            IsVerified = true,
            UploadedAt = DateTime.UtcNow,
        };

        // Act
        _context.Purchases.Add(purchase);
        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Receipts.FindAsync(receipt.ReceiptId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ReceiptNumber, Is.EqualTo("REC-12345"));
        Assert.That(retrieved.IsVerified, Is.True);
    }

    /// <summary>
    /// Tests that ReturnWindows can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ReturnWindows_CanAddAndRetrieve()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "TV",
            Category = ProductCategory.HomeAppliances,
            StoreName = "Costco",
            PurchaseDate = DateTime.UtcNow,
            Price = 1299.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            DurationDays = 90,
            Status = ReturnWindowStatus.Open,
            PolicyDetails = "90-day return policy",
        };

        // Act
        _context.Purchases.Add(purchase);
        _context.ReturnWindows.Add(returnWindow);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ReturnWindows.FindAsync(returnWindow.ReturnWindowId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.DurationDays, Is.EqualTo(90));
        Assert.That(retrieved.Status, Is.EqualTo(ReturnWindowStatus.Open));
    }

    /// <summary>
    /// Tests that Warranties can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Warranties_CanAddAndRetrieve()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Laptop",
            Category = ProductCategory.Electronics,
            StoreName = "Apple Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 2499.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            WarrantyType = WarrantyType.Manufacturer,
            Provider = "Apple Inc.",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(12),
            DurationMonths = 12,
            Status = WarrantyStatus.Active,
        };

        // Act
        _context.Purchases.Add(purchase);
        _context.Warranties.Add(warranty);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Warranties.FindAsync(warranty.WarrantyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.WarrantyType, Is.EqualTo(WarrantyType.Manufacturer));
        Assert.That(retrieved.DurationMonths, Is.EqualTo(12));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Phone",
            Category = ProductCategory.Electronics,
            StoreName = "Apple Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 999.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            ReceiptNumber = "REC-99999",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.PDF,
            ReceiptDate = DateTime.UtcNow,
            StoreName = "Apple Store",
            TotalAmount = 999.99m,
            PaymentMethod = PaymentMethod.CreditCard,
            Status = ReceiptStatus.Active,
            UploadedAt = DateTime.UtcNow,
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            WarrantyType = WarrantyType.Manufacturer,
            Provider = "Apple Inc.",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(12),
            DurationMonths = 12,
            Status = WarrantyStatus.Active,
        };

        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(14),
            DurationDays = 14,
            Status = ReturnWindowStatus.Open,
        };

        _context.Purchases.Add(purchase);
        _context.Receipts.Add(receipt);
        _context.Warranties.Add(warranty);
        _context.ReturnWindows.Add(returnWindow);
        await _context.SaveChangesAsync();

        // Act
        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();

        var retrievedReceipt = await _context.Receipts.FindAsync(receipt.ReceiptId);
        var retrievedWarranty = await _context.Warranties.FindAsync(warranty.WarrantyId);
        var retrievedReturnWindow = await _context.ReturnWindows.FindAsync(returnWindow.ReturnWindowId);

        // Assert
        Assert.That(retrievedReceipt, Is.Null);
        Assert.That(retrievedWarranty, Is.Null);
        Assert.That(retrievedReturnWindow, Is.Null);
    }
}
