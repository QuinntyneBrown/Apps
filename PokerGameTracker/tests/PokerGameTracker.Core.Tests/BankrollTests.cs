// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class BankrollTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesBankroll()
    {
        // Arrange & Act
        var bankroll = new Bankroll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bankroll.BankrollId, Is.EqualTo(Guid.Empty));
            Assert.That(bankroll.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(bankroll.Amount, Is.EqualTo(0m));
            Assert.That(bankroll.RecordedDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(bankroll.Notes, Is.Null);
            Assert.That(bankroll.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recordedDate = DateTime.UtcNow;

        // Act
        var bankroll = new Bankroll
        {
            BankrollId = bankrollId,
            UserId = userId,
            Amount = 5000m,
            RecordedDate = recordedDate,
            Notes = "Starting bankroll"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bankroll.BankrollId, Is.EqualTo(bankrollId));
            Assert.That(bankroll.UserId, Is.EqualTo(userId));
            Assert.That(bankroll.Amount, Is.EqualTo(5000m));
            Assert.That(bankroll.RecordedDate, Is.EqualTo(recordedDate));
            Assert.That(bankroll.Notes, Is.EqualTo("Starting bankroll"));
        });
    }

    [Test]
    public void Amount_AcceptsDecimalValues()
    {
        // Arrange & Act
        var bankroll = new Bankroll { Amount = 2500.75m };

        // Assert
        Assert.That(bankroll.Amount, Is.EqualTo(2500.75m));
    }

    [Test]
    public void Amount_CanBeNegative()
    {
        // Arrange & Act
        var bankroll = new Bankroll { Amount = -100m };

        // Assert
        Assert.That(bankroll.Amount, Is.EqualTo(-100m));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var bankroll = new Bankroll { Notes = null };

        // Assert
        Assert.That(bankroll.Notes, Is.Null);
    }

    [Test]
    public void RecordedDate_CanBeSet()
    {
        // Arrange
        var recordedDate = DateTime.UtcNow.AddDays(-7);

        // Act
        var bankroll = new Bankroll { RecordedDate = recordedDate };

        // Assert
        Assert.That(bankroll.RecordedDate, Is.EqualTo(recordedDate));
    }
}
