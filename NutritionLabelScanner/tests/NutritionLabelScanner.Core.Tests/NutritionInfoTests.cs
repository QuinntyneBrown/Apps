// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class NutritionInfoTests
{
    [Test]
    public void Constructor_CreatesNutritionInfo_WithDefaultValues()
    {
        // Arrange & Act
        var nutritionInfo = new NutritionInfo();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nutritionInfo.NutritionInfoId, Is.EqualTo(Guid.Empty));
            Assert.That(nutritionInfo.ProductId, Is.EqualTo(Guid.Empty));
            Assert.That(nutritionInfo.Calories, Is.EqualTo(0));
            Assert.That(nutritionInfo.TotalFat, Is.EqualTo(0));
            Assert.That(nutritionInfo.SaturatedFat, Is.Null);
            Assert.That(nutritionInfo.TransFat, Is.Null);
            Assert.That(nutritionInfo.Cholesterol, Is.Null);
            Assert.That(nutritionInfo.Sodium, Is.EqualTo(0));
            Assert.That(nutritionInfo.TotalCarbohydrates, Is.EqualTo(0));
            Assert.That(nutritionInfo.DietaryFiber, Is.Null);
            Assert.That(nutritionInfo.TotalSugars, Is.Null);
            Assert.That(nutritionInfo.Protein, Is.EqualTo(0));
            Assert.That(nutritionInfo.AdditionalNutrients, Is.Null);
            Assert.That(nutritionInfo.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(nutritionInfo.Product, Is.Null);
        });
    }

    [Test]
    public void Calories_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();

        // Act
        nutritionInfo.Calories = 200;

        // Assert
        Assert.That(nutritionInfo.Calories, Is.EqualTo(200));
    }

    [Test]
    public void TotalFat_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();

        // Act
        nutritionInfo.TotalFat = 12.5m;

        // Assert
        Assert.That(nutritionInfo.TotalFat, Is.EqualTo(12.5m));
    }

    [Test]
    public void Sodium_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();

        // Act
        nutritionInfo.Sodium = 450m;

        // Assert
        Assert.That(nutritionInfo.Sodium, Is.EqualTo(450m));
    }

    [Test]
    public void TotalCarbohydrates_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();

        // Act
        nutritionInfo.TotalCarbohydrates = 30m;

        // Assert
        Assert.That(nutritionInfo.TotalCarbohydrates, Is.EqualTo(30m));
    }

    [Test]
    public void Protein_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();

        // Act
        nutritionInfo.Protein = 15m;

        // Assert
        Assert.That(nutritionInfo.Protein, Is.EqualTo(15m));
    }

    [Test]
    public void IsHighSodium_ReturnsFalse_WhenSodiumIs600OrLess()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            Sodium = 600m
        };

        // Act
        var result = nutritionInfo.IsHighSodium();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighSodium_ReturnsTrue_WhenSodiumIsGreaterThan600()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            Sodium = 601m
        };

        // Act
        var result = nutritionInfo.IsHighSodium();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighSodium_ReturnsTrue_WhenSodiumIsWellAboveThreshold()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            Sodium = 1200m
        };

        // Act
        var result = nutritionInfo.IsHighSodium();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighSugar_ReturnsFalse_WhenTotalSugarsIsNull()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            TotalSugars = null
        };

        // Act
        var result = nutritionInfo.IsHighSugar();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighSugar_ReturnsFalse_WhenTotalSugarsIs15OrLess()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            TotalSugars = 15m
        };

        // Act
        var result = nutritionInfo.IsHighSugar();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighSugar_ReturnsTrue_WhenTotalSugarsIsGreaterThan15()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            TotalSugars = 16m
        };

        // Act
        var result = nutritionInfo.IsHighSugar();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighSugar_ReturnsTrue_WhenTotalSugarsIsWellAboveThreshold()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo
        {
            TotalSugars = 40m
        };

        // Act
        var result = nutritionInfo.IsHighSugar();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Product_CanBeSet_AndRetrieved()
    {
        // Arrange
        var nutritionInfo = new NutritionInfo();
        var product = new Product { ProductId = Guid.NewGuid(), Name = "Test Product" };

        // Act
        nutritionInfo.Product = product;

        // Assert
        Assert.That(nutritionInfo.Product, Is.EqualTo(product));
    }

    [Test]
    public void NutritionInfo_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var nutritionInfoId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        // Act
        var nutritionInfo = new NutritionInfo
        {
            NutritionInfoId = nutritionInfoId,
            ProductId = productId,
            Calories = 250,
            TotalFat = 10m,
            SaturatedFat = 3m,
            TransFat = 0m,
            Cholesterol = 20m,
            Sodium = 700m,
            TotalCarbohydrates = 35m,
            DietaryFiber = 5m,
            TotalSugars = 18m,
            Protein = 12m,
            AdditionalNutrients = "{\"VitaminD\": \"2mcg\"}"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nutritionInfo.NutritionInfoId, Is.EqualTo(nutritionInfoId));
            Assert.That(nutritionInfo.ProductId, Is.EqualTo(productId));
            Assert.That(nutritionInfo.Calories, Is.EqualTo(250));
            Assert.That(nutritionInfo.TotalFat, Is.EqualTo(10m));
            Assert.That(nutritionInfo.SaturatedFat, Is.EqualTo(3m));
            Assert.That(nutritionInfo.Sodium, Is.EqualTo(700m));
            Assert.That(nutritionInfo.TotalSugars, Is.EqualTo(18m));
            Assert.That(nutritionInfo.Protein, Is.EqualTo(12m));
            Assert.That(nutritionInfo.IsHighSodium(), Is.True);
            Assert.That(nutritionInfo.IsHighSugar(), Is.True);
        });
    }
}
