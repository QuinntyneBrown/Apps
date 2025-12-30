// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class ReflectionCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var reflectionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var text = "Reflecting on our journey...";
        var topic = "Communication";

        // Act
        var evt = new ReflectionCreatedEvent
        {
            ReflectionId = reflectionId,
            UserId = userId,
            Text = text,
            Topic = topic
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReflectionId, Is.EqualTo(reflectionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Text, Is.EqualTo(text));
            Assert.That(evt.Topic, Is.EqualTo(topic));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new ReflectionCreatedEvent
        {
            ReflectionId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ReflectionId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new ReflectionCreatedEvent { ReflectionId = expectedId };

        // Assert
        Assert.That(evt.ReflectionId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new ReflectionCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Text_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedText = "Our relationship has grown";

        // Act
        var evt = new ReflectionCreatedEvent { Text = expectedText };

        // Assert
        Assert.That(evt.Text, Is.EqualTo(expectedText));
    }

    [Test]
    public void Topic_CanBeNull()
    {
        // Arrange & Act
        var evt = new ReflectionCreatedEvent { Topic = null };

        // Assert
        Assert.That(evt.Topic, Is.Null);
    }

    [Test]
    public void Topic_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedTopic = "Trust";

        // Act
        var evt = new ReflectionCreatedEvent { Topic = expectedTopic };

        // Assert
        Assert.That(evt.Topic, Is.EqualTo(expectedTopic));
    }
}
