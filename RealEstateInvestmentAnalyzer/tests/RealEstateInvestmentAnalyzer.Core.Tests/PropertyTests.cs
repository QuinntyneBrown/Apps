// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class PropertyTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesProperty()
    {
        // Arrange & Act
        var property = new Property();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(property.PropertyId, Is.EqualTo(Guid.Empty));
            Assert.That(property.Address, Is.EqualTo(string.Empty));
            Assert.That(property.PropertyType, Is.EqualTo(PropertyType.SingleFamily));
            Assert.That(property.PurchasePrice, Is.EqualTo(0m));
            Assert.That(property.PurchaseDate, Is.EqualTo(default(DateTime)));
            Assert.That(property.CurrentValue, Is.EqualTo(0m));
            Assert.That(property.SquareFeet, Is.EqualTo(0));
            Assert.That(property.Bedrooms, Is.EqualTo(0));
            Assert.That(property.Bathrooms, Is.EqualTo(0));
            Assert.That(property.Notes, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesProperty()
    {
        // Arrange
        var propertyId = Guid.NewGuid();
        var address = "123 Main St, City, State 12345";
        var purchasePrice = 250000m;
        var purchaseDate = new DateTime(2023, 1, 15);
        var currentValue = 280000m;

        // Act
        var property = new Property
        {
            PropertyId = propertyId,
            Address = address,
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = purchasePrice,
            PurchaseDate = purchaseDate,
            CurrentValue = currentValue,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(property.PropertyId, Is.EqualTo(propertyId));
            Assert.That(property.Address, Is.EqualTo(address));
            Assert.That(property.PropertyType, Is.EqualTo(PropertyType.SingleFamily));
            Assert.That(property.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(property.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(property.CurrentValue, Is.EqualTo(currentValue));
            Assert.That(property.SquareFeet, Is.EqualTo(2000));
            Assert.That(property.Bedrooms, Is.EqualTo(3));
            Assert.That(property.Bathrooms, Is.EqualTo(2));
        });
    }

    [Test]
    public void CalculateEquity_PositiveEquity_ReturnsCorrectValue()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 280000m
        };

        // Act
        var equity = property.CalculateEquity();

        // Assert
        Assert.That(equity, Is.EqualTo(30000m));
    }

    [Test]
    public void CalculateEquity_NegativeEquity_ReturnsNegativeValue()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 280000m,
            CurrentValue = 250000m
        };

        // Act
        var equity = property.CalculateEquity();

        // Assert
        Assert.That(equity, Is.EqualTo(-30000m));
    }

    [Test]
    public void CalculateEquity_NoChange_ReturnsZero()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 250000m
        };

        // Act
        var equity = property.CalculateEquity();

        // Assert
        Assert.That(equity, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateROI_PositiveROI_ReturnsCorrectPercentage()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 280000m
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(12m));
    }

    [Test]
    public void CalculateROI_NegativeROI_ReturnsNegativePercentage()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 280000m,
            CurrentValue = 250000m
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(-10.714285714285714285714285714m).Within(0.01m));
    }

    [Test]
    public void CalculateROI_ZeroPurchasePrice_ReturnsZero()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 0m,
            CurrentValue = 280000m
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateROI_NoChange_ReturnsZero()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 250000m
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(0m));
    }

    [Test]
    public void Property_SingleFamily_SetsCorrectly()
    {
        // Arrange & Act
        var property = new Property
        {
            Address = "456 Oak Ave",
            PropertyType = PropertyType.SingleFamily,
            Bedrooms = 4,
            Bathrooms = 3
        };

        // Assert
        Assert.That(property.PropertyType, Is.EqualTo(PropertyType.SingleFamily));
    }

    [Test]
    public void Property_MultiFamily_SetsCorrectly()
    {
        // Arrange & Act
        var property = new Property
        {
            Address = "789 Elm St",
            PropertyType = PropertyType.MultiFamily,
            Bedrooms = 8,
            Bathrooms = 6
        };

        // Assert
        Assert.That(property.PropertyType, Is.EqualTo(PropertyType.MultiFamily));
    }

    [Test]
    public void Property_Commercial_SetsCorrectly()
    {
        // Arrange & Act
        var property = new Property
        {
            Address = "100 Business Blvd",
            PropertyType = PropertyType.Commercial,
            SquareFeet = 5000
        };

        // Assert
        Assert.That(property.PropertyType, Is.EqualTo(PropertyType.Commercial));
    }

    [Test]
    public void Property_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Prime location near downtown";

        // Act
        var property = new Property
        {
            Address = "123 Main St",
            Notes = notes
        };

        // Assert
        Assert.That(property.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Property_LargeSquareFeet_StoresCorrectly()
    {
        // Arrange & Act
        var property = new Property
        {
            Address = "999 Large Ave",
            SquareFeet = 10000
        };

        // Assert
        Assert.That(property.SquareFeet, Is.EqualTo(10000));
    }

    [Test]
    public void Property_HighValue_StoresCorrectly()
    {
        // Arrange & Act
        var property = new Property
        {
            Address = "Luxury Lane",
            PurchasePrice = 2500000m,
            CurrentValue = 3000000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(property.PurchasePrice, Is.EqualTo(2500000m));
            Assert.That(property.CurrentValue, Is.EqualTo(3000000m));
        });
    }

    [Test]
    public void CalculateROI_DoubleValue_Returns100Percent()
    {
        // Arrange
        var property = new Property
        {
            PurchasePrice = 100000m,
            CurrentValue = 200000m
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(100m));
    }
}
