// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core.Tests;

public class HabitFrequencyTests
{
    [Test]
    public void HabitFrequency_Daily_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = HabitFrequency.Daily;

        // Assert
        Assert.That(frequency, Is.EqualTo(HabitFrequency.Daily));
        Assert.That((int)frequency, Is.EqualTo(0));
    }

    [Test]
    public void HabitFrequency_Weekly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = HabitFrequency.Weekly;

        // Assert
        Assert.That(frequency, Is.EqualTo(HabitFrequency.Weekly));
        Assert.That((int)frequency, Is.EqualTo(1));
    }

    [Test]
    public void HabitFrequency_Custom_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = HabitFrequency.Custom;

        // Assert
        Assert.That(frequency, Is.EqualTo(HabitFrequency.Custom));
        Assert.That((int)frequency, Is.EqualTo(2));
    }

    [Test]
    public void HabitFrequency_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var frequencies = new[]
        {
            HabitFrequency.Daily,
            HabitFrequency.Weekly,
            HabitFrequency.Custom
        };

        // Assert
        Assert.That(frequencies, Has.Length.EqualTo(3));
    }

    [Test]
    public void HabitFrequency_CanBeCompared()
    {
        // Arrange
        var frequency1 = HabitFrequency.Daily;
        var frequency2 = HabitFrequency.Daily;
        var frequency3 = HabitFrequency.Weekly;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(frequency1, Is.EqualTo(frequency2));
            Assert.That(frequency1, Is.Not.EqualTo(frequency3));
        });
    }
}
