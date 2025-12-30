// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class ProductScannedEventTests
{
    [Test]
    public void ProductScannedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Organic Granola";
        var barcode = "123456789012";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ProductScannedEvent
        {
            ProductId = productId,
            UserId = userId,
            Name = name,
            Barcode = barcode,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProductId, Is.EqualTo(productId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Barcode, Is.EqualTo(barcode));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ProductScannedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ProductScannedEvent
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ProductScannedEvent_BarcodeCanBeNull()
    {
        // Arrange & Act
        var evt = new ProductScannedEvent
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            Barcode = null
        };

        // Assert
        Assert.That(evt.Barcode, Is.Null);
    }

    [Test]
    public void ProductScannedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ProductScannedEvent
        {
            ProductId = productId,
            UserId = userId,
            Name = "Test",
            Barcode = "123",
            Timestamp = timestamp
        };

        var evt2 = new ProductScannedEvent
        {
            ProductId = productId,
            UserId = userId,
            Name = "Test",
            Barcode = "123",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ProductScannedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ProductScannedEvent
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Product 1",
            Barcode = "111"
        };

        var evt2 = new ProductScannedEvent
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Product 2",
            Barcode = "222"
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
