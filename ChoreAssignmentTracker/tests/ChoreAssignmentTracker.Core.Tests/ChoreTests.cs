// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class ChoreTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var chore = new Chore();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(chore.Name, Is.EqualTo(string.Empty));
            Assert.That(chore.Frequency, Is.EqualTo(ChoreFrequency.Daily));
            Assert.That(chore.Points, Is.EqualTo(0));
            Assert.That(chore.IsActive, Is.True);
            Assert.That(chore.Assignments, Is.Not.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var choreId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var chore = new Chore
        {
            ChoreId = choreId,
            UserId = userId,
            Name = "Wash Dishes",
            Description = "Clean all dishes after dinner",
            Frequency = ChoreFrequency.Daily,
            EstimatedMinutes = 30,
            Points = 10,
            Category = "Kitchen",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(chore.ChoreId, Is.EqualTo(choreId));
            Assert.That(chore.UserId, Is.EqualTo(userId));
            Assert.That(chore.Name, Is.EqualTo("Wash Dishes"));
            Assert.That(chore.Description, Is.EqualTo("Clean all dishes after dinner"));
            Assert.That(chore.Frequency, Is.EqualTo(ChoreFrequency.Daily));
            Assert.That(chore.EstimatedMinutes, Is.EqualTo(30));
            Assert.That(chore.Points, Is.EqualTo(10));
            Assert.That(chore.Category, Is.EqualTo("Kitchen"));
            Assert.That(chore.IsActive, Is.True);
        });
    }

    [Test]
    public void CalculateNextDueDate_Daily_AddsOneDay()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.Daily };
        var fromDate = new DateTime(2024, 1, 1);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 2)));
    }

    [Test]
    public void CalculateNextDueDate_Weekly_AddsSevenDays()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.Weekly };
        var fromDate = new DateTime(2024, 1, 1);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 8)));
    }

    [Test]
    public void CalculateNextDueDate_BiWeekly_AddsFourteenDays()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.BiWeekly };
        var fromDate = new DateTime(2024, 1, 1);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 15)));
    }

    [Test]
    public void CalculateNextDueDate_Monthly_AddsOneMonth()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.Monthly };
        var fromDate = new DateTime(2024, 1, 15);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        Assert.That(result, Is.EqualTo(new DateTime(2024, 2, 15)));
    }

    [Test]
    public void CalculateNextDueDate_AsNeeded_ReturnsSameDate()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.AsNeeded };
        var fromDate = new DateTime(2024, 1, 1);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        Assert.That(result, Is.EqualTo(fromDate));
    }

    [Test]
    public void CalculateNextDueDate_MonthlyEndOfMonth_HandlesCorrectly()
    {
        // Arrange
        var chore = new Chore { Frequency = ChoreFrequency.Monthly };
        var fromDate = new DateTime(2024, 1, 31);

        // Act
        var result = chore.CalculateNextDueDate(fromDate);

        // Assert
        // February doesn't have 31 days, so it should be Feb 29 (2024 is a leap year)
        Assert.That(result, Is.EqualTo(new DateTime(2024, 2, 29)));
    }
}
