// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core.Tests;

public class GiftIdeaTests
{
    [Test]
    public void GiftIdea_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var giftIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var name = "Bluetooth Headphones";
        var occasion = Occasion.Birthday;
        var estimatedPrice = 99.99m;
        var createdAt = DateTime.UtcNow;

        // Act
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = giftIdeaId,
            UserId = userId,
            RecipientId = recipientId,
            Name = name,
            Occasion = occasion,
            EstimatedPrice = estimatedPrice,
            IsPurchased = false,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(giftIdea.GiftIdeaId, Is.EqualTo(giftIdeaId));
            Assert.That(giftIdea.UserId, Is.EqualTo(userId));
            Assert.That(giftIdea.RecipientId, Is.EqualTo(recipientId));
            Assert.That(giftIdea.Name, Is.EqualTo(name));
            Assert.That(giftIdea.Occasion, Is.EqualTo(occasion));
            Assert.That(giftIdea.EstimatedPrice, Is.EqualTo(estimatedPrice));
            Assert.That(giftIdea.IsPurchased, Is.False);
            Assert.That(giftIdea.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void GiftIdea_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var giftIdea = new GiftIdea();

        // Assert
        Assert.That(giftIdea.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GiftIdea_DefaultIsPurchased_IsFalse()
    {
        // Arrange & Act
        var giftIdea = new GiftIdea();

        // Assert
        Assert.That(giftIdea.IsPurchased, Is.False);
    }

    [Test]
    public void GiftIdea_RecipientId_CanBeNull()
    {
        // Arrange & Act
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipientId = null,
            Name = "Gift without specific recipient"
        };

        // Assert
        Assert.That(giftIdea.RecipientId, Is.Null);
    }

    [Test]
    public void GiftIdea_EstimatedPrice_CanBeNull()
    {
        // Arrange & Act
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Gift with unknown price",
            EstimatedPrice = null
        };

        // Assert
        Assert.That(giftIdea.EstimatedPrice, Is.Null);
    }

    [Test]
    public void GiftIdea_IsPurchased_CanBeSetToTrue()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Smart Watch"
        };

        // Act
        giftIdea.IsPurchased = true;

        // Assert
        Assert.That(giftIdea.IsPurchased, Is.True);
    }

    [Test]
    public void GiftIdea_Recipient_CanBeSet()
    {
        // Arrange
        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            Name = "John Doe"
        };

        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Gift Card"
        };

        // Act
        giftIdea.Recipient = recipient;

        // Assert
        Assert.That(giftIdea.Recipient, Is.EqualTo(recipient));
    }

    [Test]
    public void GiftIdea_CanHaveMultipleOccasions()
    {
        // Arrange & Act
        var birthdayGift = new GiftIdea { Occasion = Occasion.Birthday };
        var christmasGift = new GiftIdea { Occasion = Occasion.Christmas };
        var weddingGift = new GiftIdea { Occasion = Occasion.Wedding };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(birthdayGift.Occasion, Is.EqualTo(Occasion.Birthday));
            Assert.That(christmasGift.Occasion, Is.EqualTo(Occasion.Christmas));
            Assert.That(weddingGift.Occasion, Is.EqualTo(Occasion.Wedding));
        });
    }

    [Test]
    public void GiftIdea_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var giftIdea = new GiftIdea();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(giftIdea.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void GiftIdea_EstimatedPrice_CanBeZero()
    {
        // Arrange & Act
        var giftIdea = new GiftIdea
        {
            EstimatedPrice = 0m
        };

        // Assert
        Assert.That(giftIdea.EstimatedPrice, Is.EqualTo(0m));
    }

    [Test]
    public void GiftIdea_AllProperties_CanBeModified()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newGiftIdeaId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newRecipientId = Guid.NewGuid();

        // Act
        giftIdea.GiftIdeaId = newGiftIdeaId;
        giftIdea.UserId = newUserId;
        giftIdea.RecipientId = newRecipientId;
        giftIdea.Name = "Updated Name";
        giftIdea.Occasion = Occasion.Anniversary;
        giftIdea.EstimatedPrice = 150m;
        giftIdea.IsPurchased = true;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(giftIdea.GiftIdeaId, Is.EqualTo(newGiftIdeaId));
            Assert.That(giftIdea.UserId, Is.EqualTo(newUserId));
            Assert.That(giftIdea.RecipientId, Is.EqualTo(newRecipientId));
            Assert.That(giftIdea.Name, Is.EqualTo("Updated Name"));
            Assert.That(giftIdea.Occasion, Is.EqualTo(Occasion.Anniversary));
            Assert.That(giftIdea.EstimatedPrice, Is.EqualTo(150m));
            Assert.That(giftIdea.IsPurchased, Is.True);
        });
    }
}
