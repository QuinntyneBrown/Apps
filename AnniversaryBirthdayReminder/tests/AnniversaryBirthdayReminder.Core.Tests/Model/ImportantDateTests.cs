// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core.Tests;

/// <summary>
/// Unit tests for the ImportantDate entity.
/// </summary>
[TestFixture]
public class ImportantDateTests
{
    /// <summary>
    /// Tests that GetNextOccurrence returns the current year date when it's in the future.
    /// </summary>
    [Test]
    public void GetNextOccurrence_WhenDateIsInFutureThisYear_ReturnsThisYearDate()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var futureDate = today.AddMonths(2);
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = new DateTime(1990, futureDate.Month, futureDate.Day),
            RecurrencePattern = RecurrencePattern.Annual,
        };

        // Act
        var nextOccurrence = importantDate.GetNextOccurrence();

        // Assert
        Assert.That(nextOccurrence.Year, Is.EqualTo(today.Year));
        Assert.That(nextOccurrence.Month, Is.EqualTo(futureDate.Month));
        Assert.That(nextOccurrence.Day, Is.EqualTo(futureDate.Day));
    }

    /// <summary>
    /// Tests that GetNextOccurrence returns next year date when this year's date has passed.
    /// </summary>
    [Test]
    public void GetNextOccurrence_WhenDateHasPassedThisYear_ReturnsNextYearDate()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var pastDate = today.AddMonths(-2);
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = new DateTime(1990, pastDate.Month, pastDate.Day),
            RecurrencePattern = RecurrencePattern.Annual,
        };

        // Act
        var nextOccurrence = importantDate.GetNextOccurrence();

        // Assert
        Assert.That(nextOccurrence.Year, Is.EqualTo(today.Year + 1));
        Assert.That(nextOccurrence.Month, Is.EqualTo(pastDate.Month));
        Assert.That(nextOccurrence.Day, Is.EqualTo(pastDate.Day));
    }

    /// <summary>
    /// Tests that HasPendingGifts returns true when there are gifts with Idea status.
    /// </summary>
    [Test]
    public void HasPendingGifts_WhenGiftIdeasExist_ReturnsTrue()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            Gifts = new List<Gift>
            {
                new Gift
                {
                    GiftId = Guid.NewGuid(),
                    Description = "Test Gift",
                    EstimatedPrice = 50m,
                    Status = GiftStatus.Idea,
                },
            },
        };

        // Act
        var hasPendingGifts = importantDate.HasPendingGifts();

        // Assert
        Assert.That(hasPendingGifts, Is.True);
    }

    /// <summary>
    /// Tests that HasPendingGifts returns false when there are no pending gifts.
    /// </summary>
    [Test]
    public void HasPendingGifts_WhenNoGiftIdeas_ReturnsFalse()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            Gifts = new List<Gift>
            {
                new Gift
                {
                    GiftId = Guid.NewGuid(),
                    Description = "Purchased Gift",
                    EstimatedPrice = 50m,
                    Status = GiftStatus.Purchased,
                },
            },
        };

        // Act
        var hasPendingGifts = importantDate.HasPendingGifts();

        // Assert
        Assert.That(hasPendingGifts, Is.False);
    }

    /// <summary>
    /// Tests that ToggleActive properly toggles the IsActive property.
    /// </summary>
    [Test]
    public void ToggleActive_WhenActive_BecomesInactive()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
        };

        // Act
        importantDate.ToggleActive();

        // Assert
        Assert.That(importantDate.IsActive, Is.False);
    }

    /// <summary>
    /// Tests that ToggleActive properly toggles the IsActive property back to active.
    /// </summary>
    [Test]
    public void ToggleActive_WhenInactive_BecomesActive()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = false,
        };

        // Act
        importantDate.ToggleActive();

        // Assert
        Assert.That(importantDate.IsActive, Is.True);
    }
}
