// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class PriceHistoryTests
{
    [Test]
    public void PriceHistory_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var priceHistoryId = Guid.NewGuid();
        var groceryItemId = Guid.NewGuid();
        var storeId = Guid.NewGuid();
        var price = 3.99m;
        var date = DateTime.UtcNow.AddDays(-5);
        var createdAt = DateTime.UtcNow;

        // Act
        var priceHistory = new PriceHistory
        {
            PriceHistoryId = priceHistoryId,
            GroceryItemId = groceryItemId,
            StoreId = storeId,
            Price = price,
            Date = date,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priceHistory.PriceHistoryId, Is.EqualTo(priceHistoryId));
            Assert.That(priceHistory.GroceryItemId, Is.EqualTo(groceryItemId));
            Assert.That(priceHistory.StoreId, Is.EqualTo(storeId));
            Assert.That(priceHistory.Price, Is.EqualTo(price));
            Assert.That(priceHistory.Date, Is.EqualTo(date));
            Assert.That(priceHistory.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void PriceHistory_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var priceHistory = new PriceHistory();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(priceHistory.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void PriceHistory_Price_CanBeZero()
    {
        // Arrange & Act
        var priceHistory = new PriceHistory
        {
            Price = 0m
        };

        // Assert
        Assert.That(priceHistory.Price, Is.EqualTo(0m));
    }

    [Test]
    public void PriceHistory_Price_CanBePositive()
    {
        // Arrange & Act
        var priceHistory = new PriceHistory
        {
            Price = 12.99m
        };

        // Assert
        Assert.That(priceHistory.Price, Is.Positive);
    }

    [Test]
    public void PriceHistory_Price_SupportsDecimalPrecision()
    {
        // Arrange & Act
        var priceHistory = new PriceHistory
        {
            Price = 5.456m
        };

        // Assert
        Assert.That(priceHistory.Price, Is.EqualTo(5.456m));
    }

    [Test]
    public void PriceHistory_Date_CanBeInPast()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddMonths(-6);

        // Act
        var priceHistory = new PriceHistory
        {
            Date = pastDate
        };

        // Assert
        Assert.That(priceHistory.Date, Is.LessThan(DateTime.UtcNow));
    }

    [Test]
    public void PriceHistory_AllProperties_CanBeModified()
    {
        // Arrange
        var priceHistory = new PriceHistory
        {
            PriceHistoryId = Guid.NewGuid(),
            Price = 5m
        };

        var newPriceHistoryId = Guid.NewGuid();
        var newGroceryItemId = Guid.NewGuid();
        var newStoreId = Guid.NewGuid();
        var newDate = DateTime.UtcNow;

        // Act
        priceHistory.PriceHistoryId = newPriceHistoryId;
        priceHistory.GroceryItemId = newGroceryItemId;
        priceHistory.StoreId = newStoreId;
        priceHistory.Price = 7.99m;
        priceHistory.Date = newDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priceHistory.PriceHistoryId, Is.EqualTo(newPriceHistoryId));
            Assert.That(priceHistory.GroceryItemId, Is.EqualTo(newGroceryItemId));
            Assert.That(priceHistory.StoreId, Is.EqualTo(newStoreId));
            Assert.That(priceHistory.Price, Is.EqualTo(7.99m));
            Assert.That(priceHistory.Date, Is.EqualTo(newDate));
        });
    }

    [Test]
    public void PriceHistory_CanTrackMultiplePricesForSameItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var storeId = Guid.NewGuid();

        // Act
        var price1 = new PriceHistory
        {
            GroceryItemId = itemId,
            StoreId = storeId,
            Price = 3.99m,
            Date = DateTime.UtcNow.AddDays(-30)
        };

        var price2 = new PriceHistory
        {
            GroceryItemId = itemId,
            StoreId = storeId,
            Price = 4.49m,
            Date = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(price1.GroceryItemId, Is.EqualTo(itemId));
            Assert.That(price2.GroceryItemId, Is.EqualTo(itemId));
            Assert.That(price1.Price, Is.Not.EqualTo(price2.Price));
        });
    }
}
