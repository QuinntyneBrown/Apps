// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core.Tests;

public class PurchaseTests
{
    [Test]
    public void Purchase_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var giftIdeaId = Guid.NewGuid();
        var purchaseDate = DateTime.UtcNow.AddDays(-1);
        var actualPrice = 89.99m;
        var store = "Best Buy";
        var createdAt = DateTime.UtcNow;

        // Act
        var purchase = new Purchase
        {
            PurchaseId = purchaseId,
            GiftIdeaId = giftIdeaId,
            PurchaseDate = purchaseDate,
            ActualPrice = actualPrice,
            Store = store,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(purchase.GiftIdeaId, Is.EqualTo(giftIdeaId));
            Assert.That(purchase.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(purchase.ActualPrice, Is.EqualTo(actualPrice));
            Assert.That(purchase.Store, Is.EqualTo(store));
            Assert.That(purchase.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Purchase_Store_CanBeNull()
    {
        // Arrange & Act
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = Guid.NewGuid(),
            PurchaseDate = DateTime.UtcNow,
            ActualPrice = 50m,
            Store = null
        };

        // Assert
        Assert.That(purchase.Store, Is.Null);
    }

    [Test]
    public void Purchase_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var purchase = new Purchase();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(purchase.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Purchase_ActualPrice_CanBeZero()
    {
        // Arrange & Act
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = Guid.NewGuid(),
            ActualPrice = 0m
        };

        // Assert
        Assert.That(purchase.ActualPrice, Is.EqualTo(0m));
    }

    [Test]
    public void Purchase_ActualPrice_CanBePositive()
    {
        // Arrange & Act
        var purchase = new Purchase
        {
            ActualPrice = 199.99m
        };

        // Assert
        Assert.That(purchase.ActualPrice, Is.Positive);
    }

    [Test]
    public void Purchase_AllProperties_CanBeModified()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = Guid.NewGuid(),
            ActualPrice = 50m
        };

        var newPurchaseId = Guid.NewGuid();
        var newGiftIdeaId = Guid.NewGuid();
        var newPurchaseDate = DateTime.UtcNow;

        // Act
        purchase.PurchaseId = newPurchaseId;
        purchase.GiftIdeaId = newGiftIdeaId;
        purchase.PurchaseDate = newPurchaseDate;
        purchase.ActualPrice = 75m;
        purchase.Store = "Amazon";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase.PurchaseId, Is.EqualTo(newPurchaseId));
            Assert.That(purchase.GiftIdeaId, Is.EqualTo(newGiftIdeaId));
            Assert.That(purchase.PurchaseDate, Is.EqualTo(newPurchaseDate));
            Assert.That(purchase.ActualPrice, Is.EqualTo(75m));
            Assert.That(purchase.Store, Is.EqualTo("Amazon"));
        });
    }

    [Test]
    public void Purchase_PurchaseDate_CanBeInPast()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-30);

        // Act
        var purchase = new Purchase
        {
            PurchaseDate = pastDate
        };

        // Assert
        Assert.That(purchase.PurchaseDate, Is.LessThan(DateTime.UtcNow));
    }

    [Test]
    public void Purchase_CanHaveDifferentStores()
    {
        // Arrange & Act
        var purchase1 = new Purchase { Store = "Target" };
        var purchase2 = new Purchase { Store = "Walmart" };
        var purchase3 = new Purchase { Store = "Online Store" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase1.Store, Is.EqualTo("Target"));
            Assert.That(purchase2.Store, Is.EqualTo("Walmart"));
            Assert.That(purchase3.Store, Is.EqualTo("Online Store"));
        });
    }

    [Test]
    public void Purchase_ActualPrice_SupportsDecimalPrecision()
    {
        // Arrange & Act
        var purchase = new Purchase
        {
            ActualPrice = 123.456m
        };

        // Assert
        Assert.That(purchase.ActualPrice, Is.EqualTo(123.456m));
    }
}
