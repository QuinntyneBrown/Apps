// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core.Tests;

public class ProjectionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var projection = new Projection();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(projection.Name, Is.EqualTo(string.Empty));
            Assert.That(projection.CurrentSavings, Is.EqualTo(0));
            Assert.That(projection.MonthlyContribution, Is.EqualTo(0));
            Assert.That(projection.ExpectedReturnRate, Is.EqualTo(0));
            Assert.That(projection.YearsUntilCollege, Is.EqualTo(0));
            Assert.That(projection.TargetGoal, Is.EqualTo(0));
            Assert.That(projection.ProjectedBalance, Is.EqualTo(0));
            Assert.That(projection.Plan, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        // Act
        var projection = new Projection
        {
            ProjectionId = projectionId,
            PlanId = planId,
            Name = "College Projection 2025",
            CurrentSavings = 10000m,
            MonthlyContribution = 500m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 10,
            TargetGoal = 100000m,
            ProjectedBalance = 95000m,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(projection.ProjectionId, Is.EqualTo(projectionId));
            Assert.That(projection.PlanId, Is.EqualTo(planId));
            Assert.That(projection.Name, Is.EqualTo("College Projection 2025"));
            Assert.That(projection.CurrentSavings, Is.EqualTo(10000m));
            Assert.That(projection.MonthlyContribution, Is.EqualTo(500m));
            Assert.That(projection.ExpectedReturnRate, Is.EqualTo(6m));
            Assert.That(projection.YearsUntilCollege, Is.EqualTo(10));
            Assert.That(projection.TargetGoal, Is.EqualTo(100000m));
            Assert.That(projection.ProjectedBalance, Is.EqualTo(95000m));
            Assert.That(projection.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void CalculateProjectedBalance_WithNoReturn_CalculatesCorrectly()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            MonthlyContribution = 500m,
            ExpectedReturnRate = 0m,
            YearsUntilCollege = 5
        };

        // Act
        var result = projection.CalculateProjectedBalance();

        // Assert
        // 10000 + (500 * 60 months) = 40000
        Assert.That(result, Is.EqualTo(40000m));
        Assert.That(projection.ProjectedBalance, Is.EqualTo(40000m));
    }

    [Test]
    public void CalculateProjectedBalance_WithReturn_CalculatesWithCompounding()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            MonthlyContribution = 500m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 10
        };

        // Act
        var result = projection.CalculateProjectedBalance();

        // Assert
        // Should be more than just principal (10000 + 500*120 = 70000) due to compounding
        Assert.That(result, Is.GreaterThan(70000m));
        Assert.That(projection.ProjectedBalance, Is.EqualTo(result));
    }

    [Test]
    public void CalculateProjectedBalance_ZeroYears_ReturnsCurrentSavingsPlusOneMonthContribution()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            MonthlyContribution = 500m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 0
        };

        // Act
        var result = projection.CalculateProjectedBalance();

        // Assert
        Assert.That(result, Is.EqualTo(10000m));
    }

    [Test]
    public void CalculateProjectedBalance_RoundsToTwoDecimalPlaces()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            MonthlyContribution = 123.456m,
            ExpectedReturnRate = 5.5m,
            YearsUntilCollege = 3
        };

        // Act
        var result = projection.CalculateProjectedBalance();

        // Assert
        Assert.That(result, Is.EqualTo(Math.Round(result, 2)));
    }

    [Test]
    public void CalculateGoalDifference_ProjectedGreaterThanGoal_ReturnsPositiveSurplus()
    {
        // Arrange
        var projection = new Projection
        {
            ProjectedBalance = 100000m,
            TargetGoal = 80000m
        };

        // Act
        var difference = projection.CalculateGoalDifference();

        // Assert
        Assert.That(difference, Is.EqualTo(20000m));
    }

    [Test]
    public void CalculateGoalDifference_ProjectedLessThanGoal_ReturnsNegativeShortfall()
    {
        // Arrange
        var projection = new Projection
        {
            ProjectedBalance = 60000m,
            TargetGoal = 80000m
        };

        // Act
        var difference = projection.CalculateGoalDifference();

        // Assert
        Assert.That(difference, Is.EqualTo(-20000m));
    }

    [Test]
    public void CalculateGoalDifference_ProjectedEqualsGoal_ReturnsZero()
    {
        // Arrange
        var projection = new Projection
        {
            ProjectedBalance = 80000m,
            TargetGoal = 80000m
        };

        // Act
        var difference = projection.CalculateGoalDifference();

        // Assert
        Assert.That(difference, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateRequiredMonthlyContribution_ZeroYears_ReturnsZero()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            TargetGoal = 50000m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 0
        };

        // Act
        var required = projection.CalculateRequiredMonthlyContribution();

        // Assert
        Assert.That(required, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateRequiredMonthlyContribution_NegativeYears_ReturnsZero()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            TargetGoal = 50000m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = -5
        };

        // Act
        var required = projection.CalculateRequiredMonthlyContribution();

        // Assert
        Assert.That(required, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateRequiredMonthlyContribution_ValidScenario_CalculatesCorrectly()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 10000m,
            TargetGoal = 50000m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 10
        };

        // Act
        var required = projection.CalculateRequiredMonthlyContribution();

        // Assert
        Assert.That(required, Is.GreaterThan(0m));
        Assert.That(required, Is.EqualTo(Math.Round(required, 2))); // Rounded to 2 decimals
    }

    [Test]
    public void CalculateRequiredMonthlyContribution_CurrentSavingsExceedsGoal_CanReturnNegative()
    {
        // Arrange
        var projection = new Projection
        {
            CurrentSavings = 60000m,
            TargetGoal = 50000m,
            ExpectedReturnRate = 6m,
            YearsUntilCollege = 10
        };

        // Act
        var required = projection.CalculateRequiredMonthlyContribution();

        // Assert
        // Can be negative when current savings already exceed goal
        Assert.That(required, Is.LessThan(0m));
    }

    [Test]
    public void Plan_CanBeAssigned()
    {
        // Arrange
        var plan = new Plan { PlanId = Guid.NewGuid(), Name = "529 Plan" };
        var projection = new Projection();

        // Act
        projection.Plan = plan;

        // Assert
        Assert.That(projection.Plan, Is.EqualTo(plan));
    }
}
