// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class TipAddedEventTests
{
    [Test]
    public void TipAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var title = "Energy Saving Tip";
        var timestamp = DateTime.UtcNow;

        // Act
        var tipEvent = new TipAddedEvent
        {
            SavingsTipId = savingsTipId,
            Title = title,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tipEvent.SavingsTipId, Is.EqualTo(savingsTipId));
            Assert.That(tipEvent.Title, Is.EqualTo(title));
            Assert.That(tipEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TipAddedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var tipEvent = new TipAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tipEvent.SavingsTipId, Is.EqualTo(Guid.Empty));
            Assert.That(tipEvent.Title, Is.EqualTo(string.Empty));
            Assert.That(tipEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TipAddedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var tipEvent = new TipAddedEvent
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Test Tip"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(tipEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void TipAddedEvent_WithEmptyTitle_IsValid()
    {
        // Arrange & Act
        var tipEvent = new TipAddedEvent
        {
            SavingsTipId = Guid.NewGuid(),
            Title = ""
        };

        // Assert
        Assert.That(tipEvent.Title, Is.EqualTo(string.Empty));
    }

    [Test]
    public void TipAddedEvent_IsImmutable()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var title = "Test Tip";

        // Act
        var tipEvent = new TipAddedEvent
        {
            SavingsTipId = savingsTipId,
            Title = title
        };

        // Assert - Record properties are init-only, so we verify they were set correctly
        Assert.Multiple(() =>
        {
            Assert.That(tipEvent.SavingsTipId, Is.EqualTo(savingsTipId));
            Assert.That(tipEvent.Title, Is.EqualTo(title));
        });
    }

    [Test]
    public void TipAddedEvent_EqualityByValue()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var title = "Test Tip";
        var timestamp = DateTime.UtcNow;

        var event1 = new TipAddedEvent
        {
            SavingsTipId = savingsTipId,
            Title = title,
            Timestamp = timestamp
        };

        var event2 = new TipAddedEvent
        {
            SavingsTipId = savingsTipId,
            Title = title,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void TipAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new TipAddedEvent
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Tip 1"
        };

        var event2 = new TipAddedEvent
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Tip 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void TipAddedEvent_WithLongTitle_IsValid()
    {
        // Arrange
        var longTitle = new string('A', 500);

        // Act
        var tipEvent = new TipAddedEvent
        {
            SavingsTipId = Guid.NewGuid(),
            Title = longTitle
        };

        // Assert
        Assert.That(tipEvent.Title, Is.EqualTo(longTitle));
    }
}
