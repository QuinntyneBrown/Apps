// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class ProductTests
{
    [Test]
    public void Constructor_CreatesProduct_WithDefaultValues()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(product.ProductId, Is.EqualTo(Guid.Empty));
            Assert.That(product.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(product.Name, Is.EqualTo(string.Empty));
            Assert.That(product.Brand, Is.Null);
            Assert.That(product.Barcode, Is.Null);
            Assert.That(product.Category, Is.Null);
            Assert.That(product.ServingSize, Is.Null);
            Assert.That(product.ScannedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(product.Notes, Is.Null);
            Assert.That(product.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(product.NutritionInfo, Is.Null);
        });
    }

    [Test]
    public void ProductId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedId = Guid.NewGuid();

        // Act
        product.ProductId = expectedId;

        // Assert
        Assert.That(product.ProductId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedUserId = Guid.NewGuid();

        // Act
        product.UserId = expectedUserId;

        // Assert
        Assert.That(product.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedName = "Organic Granola Bar";

        // Act
        product.Name = expectedName;

        // Assert
        Assert.That(product.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Brand_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedBrand = "Nature Valley";

        // Act
        product.Brand = expectedBrand;

        // Assert
        Assert.That(product.Brand, Is.EqualTo(expectedBrand));
    }

    [Test]
    public void Barcode_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedBarcode = "123456789012";

        // Act
        product.Barcode = expectedBarcode;

        // Assert
        Assert.That(product.Barcode, Is.EqualTo(expectedBarcode));
    }

    [Test]
    public void Category_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedCategory = "Snacks";

        // Act
        product.Category = expectedCategory;

        // Assert
        Assert.That(product.Category, Is.EqualTo(expectedCategory));
    }

    [Test]
    public void ServingSize_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedServingSize = "1 bar (40g)";

        // Act
        product.ServingSize = expectedServingSize;

        // Assert
        Assert.That(product.ServingSize, Is.EqualTo(expectedServingSize));
    }

    [Test]
    public void ScannedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedDate = new DateTime(2024, 6, 15, 10, 30, 0);

        // Act
        product.ScannedAt = expectedDate;

        // Assert
        Assert.That(product.ScannedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Notes_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var expectedNotes = "Found at local grocery store";

        // Act
        product.Notes = expectedNotes;

        // Assert
        Assert.That(product.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void NutritionInfo_CanBeSet_AndRetrieved()
    {
        // Arrange
        var product = new Product();
        var nutritionInfo = new NutritionInfo { NutritionInfoId = Guid.NewGuid() };

        // Act
        product.NutritionInfo = nutritionInfo;

        // Assert
        Assert.That(product.NutritionInfo, Is.EqualTo(nutritionInfo));
    }

    [Test]
    public void HasNutritionInfo_ReturnsFalse_WhenNutritionInfoIsNull()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            NutritionInfo = null
        };

        // Act
        var result = product.HasNutritionInfo();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void HasNutritionInfo_ReturnsTrue_WhenNutritionInfoIsSet()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            NutritionInfo = new NutritionInfo { NutritionInfoId = Guid.NewGuid() }
        };

        // Act
        var result = product.HasNutritionInfo();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Product_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var scannedAt = DateTime.UtcNow;

        // Act
        var product = new Product
        {
            ProductId = productId,
            UserId = userId,
            Name = "Test Product",
            Brand = "Test Brand",
            Barcode = "123456789",
            Category = "Beverages",
            ServingSize = "250ml",
            ScannedAt = scannedAt,
            Notes = "Test notes"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(product.ProductId, Is.EqualTo(productId));
            Assert.That(product.UserId, Is.EqualTo(userId));
            Assert.That(product.Name, Is.EqualTo("Test Product"));
            Assert.That(product.Brand, Is.EqualTo("Test Brand"));
            Assert.That(product.Barcode, Is.EqualTo("123456789"));
            Assert.That(product.Category, Is.EqualTo("Beverages"));
            Assert.That(product.ServingSize, Is.EqualTo("250ml"));
            Assert.That(product.ScannedAt, Is.EqualTo(scannedAt));
            Assert.That(product.Notes, Is.EqualTo("Test notes"));
            Assert.That(product.HasNutritionInfo(), Is.False);
        });
    }
}
