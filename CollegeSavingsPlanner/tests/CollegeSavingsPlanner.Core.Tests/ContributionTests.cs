// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core.Tests;

public class ContributionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var contribution = new Contribution();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contribution.Amount, Is.EqualTo(0));
            Assert.That(contribution.Contributor, Is.Null);
            Assert.That(contribution.Notes, Is.Null);
            Assert.That(contribution.IsRecurring, Is.False);
            Assert.That(contribution.Plan, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var contributionDate = DateTime.UtcNow.AddDays(-10);

        // Act
        var contribution = new Contribution
        {
            ContributionId = contributionId,
            PlanId = planId,
            Amount = 500m,
            ContributionDate = contributionDate,
            Contributor = "John Doe",
            Notes = "Monthly contribution",
            IsRecurring = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contribution.ContributionId, Is.EqualTo(contributionId));
            Assert.That(contribution.PlanId, Is.EqualTo(planId));
            Assert.That(contribution.Amount, Is.EqualTo(500m));
            Assert.That(contribution.ContributionDate, Is.EqualTo(contributionDate));
            Assert.That(contribution.Contributor, Is.EqualTo("John Doe"));
            Assert.That(contribution.Notes, Is.EqualTo("Monthly contribution"));
            Assert.That(contribution.IsRecurring, Is.True);
        });
    }

    [Test]
    public void ValidateAmount_PositiveAmount_DoesNotThrow()
    {
        // Arrange
        var contribution = new Contribution { Amount = 100m };

        // Act & Assert
        Assert.DoesNotThrow(() => contribution.ValidateAmount());
    }

    [Test]
    public void ValidateAmount_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var contribution = new Contribution { Amount = 0m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.Message, Does.Contain("Contribution amount must be positive"));
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
        });
    }

    [Test]
    public void ValidateAmount_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var contribution = new Contribution { Amount = -50m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.Message, Does.Contain("Contribution amount must be positive"));
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
        });
    }

    [Test]
    public void ValidateAmount_SmallPositiveAmount_DoesNotThrow()
    {
        // Arrange
        var contribution = new Contribution { Amount = 0.01m };

        // Act & Assert
        Assert.DoesNotThrow(() => contribution.ValidateAmount());
    }

    [Test]
    public void IsRecurring_DefaultsToFalse()
    {
        // Arrange & Act
        var contribution = new Contribution();

        // Assert
        Assert.That(contribution.IsRecurring, Is.False);
    }

    [Test]
    public void IsRecurring_CanBeSetToTrue()
    {
        // Arrange
        var contribution = new Contribution();

        // Act
        contribution.IsRecurring = true;

        // Assert
        Assert.That(contribution.IsRecurring, Is.True);
    }

    [Test]
    public void Plan_CanBeAssigned()
    {
        // Arrange
        var plan = new Plan { PlanId = Guid.NewGuid(), Name = "529 Plan" };
        var contribution = new Contribution();

        // Act
        contribution.Plan = plan;

        // Assert
        Assert.That(contribution.Plan, Is.EqualTo(plan));
    }
}
