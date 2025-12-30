// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class ContributionTests
{
    [Test]
    public void Constructor_CreatesContribution_WithDefaultValues()
    {
        // Arrange & Act
        var contribution = new Contribution();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contribution.ContributionId, Is.EqualTo(Guid.Empty));
            Assert.That(contribution.GoalId, Is.EqualTo(Guid.Empty));
            Assert.That(contribution.Amount, Is.EqualTo(0m));
            Assert.That(contribution.ContributionDate, Is.EqualTo(default(DateTime)));
            Assert.That(contribution.Notes, Is.Null);
            Assert.That(contribution.Goal, Is.Null);
        });
    }

    [Test]
    public void ContributionId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var contribution = new Contribution();
        var expectedId = Guid.NewGuid();

        // Act
        contribution.ContributionId = expectedId;

        // Assert
        Assert.That(contribution.ContributionId, Is.EqualTo(expectedId));
    }

    [Test]
    public void GoalId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var contribution = new Contribution();
        var expectedGoalId = Guid.NewGuid();

        // Act
        contribution.GoalId = expectedGoalId;

        // Assert
        Assert.That(contribution.GoalId, Is.EqualTo(expectedGoalId));
    }

    [Test]
    public void Amount_CanBeSet_AndRetrieved()
    {
        // Arrange
        var contribution = new Contribution();
        var expectedAmount = 250.50m;

        // Act
        contribution.Amount = expectedAmount;

        // Assert
        Assert.That(contribution.Amount, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void ContributionDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var contribution = new Contribution();
        var expectedDate = new DateTime(2024, 6, 15);

        // Act
        contribution.ContributionDate = expectedDate;

        // Assert
        Assert.That(contribution.ContributionDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Notes_CanBeSet_AndRetrieved()
    {
        // Arrange
        var contribution = new Contribution();
        var expectedNotes = "Monthly savings contribution";

        // Act
        contribution.Notes = expectedNotes;

        // Assert
        Assert.That(contribution.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange
        var contribution = new Contribution();

        // Act
        contribution.Notes = null;

        // Assert
        Assert.That(contribution.Notes, Is.Null);
    }

    [Test]
    public void Contribution_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var amount = 500m;
        var contributionDate = new DateTime(2024, 7, 1);
        var notes = "Bonus contribution";

        // Act
        var contribution = new Contribution
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount,
            ContributionDate = contributionDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contribution.ContributionId, Is.EqualTo(contributionId));
            Assert.That(contribution.GoalId, Is.EqualTo(goalId));
            Assert.That(contribution.Amount, Is.EqualTo(amount));
            Assert.That(contribution.ContributionDate, Is.EqualTo(contributionDate));
            Assert.That(contribution.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Contribution_WithZeroAmount_CanBeCreated()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            Amount = 0m,
            ContributionDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(contribution.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Contribution_WithLargeAmount_CanBeCreated()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            Amount = 10000m,
            ContributionDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(contribution.Amount, Is.EqualTo(10000m));
    }
}
