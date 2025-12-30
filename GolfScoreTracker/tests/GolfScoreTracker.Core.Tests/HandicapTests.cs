// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core.Tests;

public class HandicapTests
{
    [Test]
    public void Handicap_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var handicapId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var handicapIndex = 15.5m;
        var calculatedDate = DateTime.UtcNow;
        var roundsUsed = 10;
        var notes = "Based on last 10 rounds";
        var createdAt = DateTime.UtcNow;

        // Act
        var handicap = new Handicap
        {
            HandicapId = handicapId,
            UserId = userId,
            HandicapIndex = handicapIndex,
            CalculatedDate = calculatedDate,
            RoundsUsed = roundsUsed,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(handicap.HandicapId, Is.EqualTo(handicapId));
            Assert.That(handicap.UserId, Is.EqualTo(userId));
            Assert.That(handicap.HandicapIndex, Is.EqualTo(handicapIndex));
            Assert.That(handicap.CalculatedDate, Is.EqualTo(calculatedDate));
            Assert.That(handicap.RoundsUsed, Is.EqualTo(roundsUsed));
            Assert.That(handicap.Notes, Is.EqualTo(notes));
            Assert.That(handicap.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Handicap_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var handicap = new Handicap();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(handicap.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Handicap_CalculatedDate_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var handicap = new Handicap();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(handicap.CalculatedDate, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Handicap_Notes_CanBeNull()
    {
        // Arrange & Act
        var handicap = new Handicap
        {
            HandicapId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Notes = null
        };

        // Assert
        Assert.That(handicap.Notes, Is.Null);
    }

    [Test]
    public void IsCurrentlyValid_ReturnsTrue_WhenCalculatedWithinLastMonth()
    {
        // Arrange
        var handicap = new Handicap
        {
            CalculatedDate = DateTime.UtcNow.AddDays(-15)
        };

        // Act
        var result = handicap.IsCurrentlyValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsCurrentlyValid_ReturnsFalse_WhenCalculatedMoreThanMonthAgo()
    {
        // Arrange
        var handicap = new Handicap
        {
            CalculatedDate = DateTime.UtcNow.AddMonths(-2)
        };

        // Act
        var result = handicap.IsCurrentlyValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsCurrentlyValid_ReturnsTrue_WhenCalculatedExactlyOneMonthAgo()
    {
        // Arrange
        var handicap = new Handicap
        {
            CalculatedDate = DateTime.UtcNow.AddMonths(-1)
        };

        // Act
        var result = handicap.IsCurrentlyValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsCurrentlyValid_ReturnsTrue_WhenCalculatedToday()
    {
        // Arrange
        var handicap = new Handicap
        {
            CalculatedDate = DateTime.UtcNow
        };

        // Act
        var result = handicap.IsCurrentlyValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Handicap_HandicapIndex_CanBeZero()
    {
        // Arrange & Act
        var handicap = new Handicap
        {
            HandicapIndex = 0m
        };

        // Assert
        Assert.That(handicap.HandicapIndex, Is.EqualTo(0m));
    }

    [Test]
    public void Handicap_HandicapIndex_CanBeNegative()
    {
        // Arrange & Act
        var handicap = new Handicap
        {
            HandicapIndex = -2.5m
        };

        // Assert
        Assert.That(handicap.HandicapIndex, Is.EqualTo(-2.5m));
    }

    [Test]
    public void Handicap_RoundsUsed_CanBeZero()
    {
        // Arrange & Act
        var handicap = new Handicap
        {
            RoundsUsed = 0
        };

        // Assert
        Assert.That(handicap.RoundsUsed, Is.EqualTo(0));
    }

    [Test]
    public void Handicap_AllProperties_CanBeModified()
    {
        // Arrange
        var handicap = new Handicap
        {
            HandicapId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HandicapIndex = 10m
        };

        var newHandicapId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();

        // Act
        handicap.HandicapId = newHandicapId;
        handicap.UserId = newUserId;
        handicap.HandicapIndex = 12.5m;
        handicap.RoundsUsed = 8;
        handicap.Notes = "Updated notes";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(handicap.HandicapId, Is.EqualTo(newHandicapId));
            Assert.That(handicap.UserId, Is.EqualTo(newUserId));
            Assert.That(handicap.HandicapIndex, Is.EqualTo(12.5m));
            Assert.That(handicap.RoundsUsed, Is.EqualTo(8));
            Assert.That(handicap.Notes, Is.EqualTo("Updated notes"));
        });
    }
}
