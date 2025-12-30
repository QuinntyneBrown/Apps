// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class ChoreFrequencyTests
{
    [Test]
    public void ChoreFrequency_Daily_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = ChoreFrequency.Daily;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(0));
    }

    [Test]
    public void ChoreFrequency_Weekly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = ChoreFrequency.Weekly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(1));
    }

    [Test]
    public void ChoreFrequency_BiWeekly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = ChoreFrequency.BiWeekly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(2));
    }

    [Test]
    public void ChoreFrequency_Monthly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = ChoreFrequency.Monthly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(3));
    }

    [Test]
    public void ChoreFrequency_AsNeeded_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = ChoreFrequency.AsNeeded;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(4));
    }

    [Test]
    public void ChoreFrequency_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var daily = ChoreFrequency.Daily;
        var weekly = ChoreFrequency.Weekly;
        var biWeekly = ChoreFrequency.BiWeekly;
        var monthly = ChoreFrequency.Monthly;
        var asNeeded = ChoreFrequency.AsNeeded;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(daily, Is.EqualTo(ChoreFrequency.Daily));
            Assert.That(weekly, Is.EqualTo(ChoreFrequency.Weekly));
            Assert.That(biWeekly, Is.EqualTo(ChoreFrequency.BiWeekly));
            Assert.That(monthly, Is.EqualTo(ChoreFrequency.Monthly));
            Assert.That(asNeeded, Is.EqualTo(ChoreFrequency.AsNeeded));
        });
    }
}
