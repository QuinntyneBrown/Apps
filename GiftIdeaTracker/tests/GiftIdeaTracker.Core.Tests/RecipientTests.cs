// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core.Tests;

public class RecipientTests
{
    [Test]
    public void Recipient_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var recipientId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Jane Smith";
        var relationship = "Sister";
        var createdAt = DateTime.UtcNow;

        // Act
        var recipient = new Recipient
        {
            RecipientId = recipientId,
            UserId = userId,
            Name = name,
            Relationship = relationship,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipient.RecipientId, Is.EqualTo(recipientId));
            Assert.That(recipient.UserId, Is.EqualTo(userId));
            Assert.That(recipient.Name, Is.EqualTo(name));
            Assert.That(recipient.Relationship, Is.EqualTo(relationship));
            Assert.That(recipient.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Recipient_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var recipient = new Recipient();

        // Assert
        Assert.That(recipient.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Recipient_Relationship_CanBeNull()
    {
        // Arrange & Act
        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "John Doe",
            Relationship = null
        };

        // Assert
        Assert.That(recipient.Relationship, Is.Null);
    }

    [Test]
    public void Recipient_GiftIdeas_DefaultsToEmptyList()
    {
        // Arrange & Act
        var recipient = new Recipient();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipient.GiftIdeas, Is.Not.Null);
            Assert.That(recipient.GiftIdeas, Is.Empty);
        });
    }

    [Test]
    public void Recipient_CanHaveMultipleGiftIdeas()
    {
        // Arrange
        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Alice Johnson"
        };

        var giftIdea1 = new GiftIdea { Name = "Book" };
        var giftIdea2 = new GiftIdea { Name = "Watch" };

        // Act
        recipient.GiftIdeas.Add(giftIdea1);
        recipient.GiftIdeas.Add(giftIdea2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipient.GiftIdeas, Has.Count.EqualTo(2));
            Assert.That(recipient.GiftIdeas, Contains.Item(giftIdea1));
            Assert.That(recipient.GiftIdeas, Contains.Item(giftIdea2));
        });
    }

    [Test]
    public void Recipient_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var recipient = new Recipient();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(recipient.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Recipient_AllProperties_CanBeModified()
    {
        // Arrange
        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newRecipientId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();

        // Act
        recipient.RecipientId = newRecipientId;
        recipient.UserId = newUserId;
        recipient.Name = "Updated Name";
        recipient.Relationship = "Friend";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipient.RecipientId, Is.EqualTo(newRecipientId));
            Assert.That(recipient.UserId, Is.EqualTo(newUserId));
            Assert.That(recipient.Name, Is.EqualTo("Updated Name"));
            Assert.That(recipient.Relationship, Is.EqualTo("Friend"));
        });
    }

    [Test]
    public void Recipient_CanSetRelationship_ToVariousValues()
    {
        // Arrange & Act
        var friend = new Recipient { Relationship = "Friend" };
        var family = new Recipient { Relationship = "Family" };
        var colleague = new Recipient { Relationship = "Colleague" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(friend.Relationship, Is.EqualTo("Friend"));
            Assert.That(family.Relationship, Is.EqualTo("Family"));
            Assert.That(colleague.Relationship, Is.EqualTo("Colleague"));
        });
    }

    [Test]
    public void Recipient_GiftIdeas_CanBeReplaced()
    {
        // Arrange
        var recipient = new Recipient();
        var newGiftIdeas = new List<GiftIdea>
        {
            new GiftIdea { Name = "Gift 1" },
            new GiftIdea { Name = "Gift 2" }
        };

        // Act
        recipient.GiftIdeas = newGiftIdeas;

        // Assert
        Assert.That(recipient.GiftIdeas, Is.EqualTo(newGiftIdeas));
    }
}
