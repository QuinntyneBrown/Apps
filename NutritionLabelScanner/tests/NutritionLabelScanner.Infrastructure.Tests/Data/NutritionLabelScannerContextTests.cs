// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the NutritionLabelScannerContext.
/// </summary>
[TestFixture]
public class NutritionLabelScannerContextTests
{
    private NutritionLabelScannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NutritionLabelScannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new NutritionLabelScannerContext(options);
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
    /// Tests that Products can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Products_CanAddAndRetrieve()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            Brand = "Test Brand",
            Barcode = "123456789",
            Category = "Snacks",
            ServingSize = "1 serving",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Products.FindAsync(product.ProductId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Product"));
        Assert.That(retrieved.Brand, Is.EqualTo("Test Brand"));
    }

    /// <summary>
    /// Tests that NutritionInfos can be added and retrieved.
    /// </summary>
    [Test]
    public async Task NutritionInfos_CanAddAndRetrieve()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var nutritionInfo = new NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = product.ProductId,
            Calories = 200,
            TotalFat = 10m,
            Sodium = 300m,
            TotalCarbohydrates = 25m,
            Protein = 5m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Products.Add(product);
        _context.NutritionInfos.Add(nutritionInfo);
        await _context.SaveChangesAsync();

        var retrieved = await _context.NutritionInfos.FindAsync(nutritionInfo.NutritionInfoId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Calories, Is.EqualTo(200));
        Assert.That(retrieved.TotalFat, Is.EqualTo(10m));
    }

    /// <summary>
    /// Tests that Comparisons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Comparisons_CanAddAndRetrieve()
    {
        // Arrange
        var comparison = new Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Comparison",
            ProductIds = "[\"id1\",\"id2\"]",
            Results = "Product 1 is healthier",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Comparisons.Add(comparison);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Comparisons.FindAsync(comparison.ComparisonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Comparison"));
        Assert.That(retrieved.Results, Is.EqualTo("Product 1 is healthier"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var nutritionInfo = new NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = product.ProductId,
            Calories = 200,
            TotalFat = 10m,
            Sodium = 300m,
            TotalCarbohydrates = 25m,
            Protein = 5m,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Products.Add(product);
        _context.NutritionInfos.Add(nutritionInfo);
        await _context.SaveChangesAsync();

        // Act
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        var retrievedNutritionInfo = await _context.NutritionInfos.FindAsync(nutritionInfo.NutritionInfoId);

        // Assert
        Assert.That(retrievedNutritionInfo, Is.Null);
    }
}
