// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class GoalTypeTests
{
    [Test]
    public void Savings_HasValue_Zero()
    {
        // Arrange & Act
        var value = (int)GoalType.Savings;

        // Assert
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void DebtPayoff_HasValue_One()
    {
        // Arrange & Act
        var value = (int)GoalType.DebtPayoff;

        // Assert
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void Investment_HasValue_Two()
    {
        // Arrange & Act
        var value = (int)GoalType.Investment;

        // Assert
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void Purchase_HasValue_Three()
    {
        // Arrange & Act
        var value = (int)GoalType.Purchase;

        // Assert
        Assert.That(value, Is.EqualTo(3));
    }

    [Test]
    public void Emergency_HasValue_Four()
    {
        // Arrange & Act
        var value = (int)GoalType.Emergency;

        // Assert
        Assert.That(value, Is.EqualTo(4));
    }

    [Test]
    public void Retirement_HasValue_Five()
    {
        // Arrange & Act
        var value = (int)GoalType.Retirement;

        // Assert
        Assert.That(value, Is.EqualTo(5));
    }

    [Test]
    public void AllEnumValues_CanBeAssigned()
    {
        // Arrange & Act
        var savings = GoalType.Savings;
        var debtPayoff = GoalType.DebtPayoff;
        var investment = GoalType.Investment;
        var purchase = GoalType.Purchase;
        var emergency = GoalType.Emergency;
        var retirement = GoalType.Retirement;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(savings, Is.EqualTo(GoalType.Savings));
            Assert.That(debtPayoff, Is.EqualTo(GoalType.DebtPayoff));
            Assert.That(investment, Is.EqualTo(GoalType.Investment));
            Assert.That(purchase, Is.EqualTo(GoalType.Purchase));
            Assert.That(emergency, Is.EqualTo(GoalType.Emergency));
            Assert.That(retirement, Is.EqualTo(GoalType.Retirement));
        });
    }

    [Test]
    public void EnumValues_AreDistinct()
    {
        // Arrange
        var values = Enum.GetValues<GoalType>();

        // Act
        var distinctValues = values.Distinct().ToList();

        // Assert
        Assert.That(distinctValues.Count, Is.EqualTo(values.Length));
    }
}
