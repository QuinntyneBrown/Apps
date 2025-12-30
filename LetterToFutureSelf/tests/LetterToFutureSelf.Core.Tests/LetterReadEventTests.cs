// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core.Tests;

public class LetterReadEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var letterId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var readDate = DateTime.UtcNow;

        // Act
        var evt = new LetterReadEvent
        {
            LetterId = letterId,
            UserId = userId,
            ReadDate = readDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LetterId, Is.EqualTo(letterId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ReadDate, Is.EqualTo(readDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new LetterReadEvent
        {
            LetterId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void LetterId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new LetterReadEvent { LetterId = expectedId };

        // Assert
        Assert.That(evt.LetterId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new LetterReadEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void ReadDate_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedReadDate = DateTime.UtcNow.AddDays(-1);

        // Act
        var evt = new LetterReadEvent { ReadDate = expectedReadDate };

        // Assert
        Assert.That(evt.ReadDate, Is.EqualTo(expectedReadDate));
    }
}
