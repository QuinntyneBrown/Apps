// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class GratitudeCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var gratitudeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var text = "I am grateful for...";

        // Act
        var evt = new GratitudeCreatedEvent
        {
            GratitudeId = gratitudeId,
            UserId = userId,
            Text = text
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GratitudeId, Is.EqualTo(gratitudeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Text, Is.EqualTo(text));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new GratitudeCreatedEvent
        {
            GratitudeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void GratitudeId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new GratitudeCreatedEvent { GratitudeId = expectedId };

        // Assert
        Assert.That(evt.GratitudeId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new GratitudeCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Text_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedText = "My partner's support";

        // Act
        var evt = new GratitudeCreatedEvent { Text = expectedText };

        // Assert
        Assert.That(evt.Text, Is.EqualTo(expectedText));
    }
}
