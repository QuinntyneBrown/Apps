// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core.Tests;

public class MoodLevelTests
{
    [Test]
    public void MoodLevel_VeryLow_HasValue0()
    {
        // Arrange & Act
        var level = MoodLevel.VeryLow;

        // Assert
        Assert.That((int)level, Is.EqualTo(0));
    }

    [Test]
    public void MoodLevel_Low_HasValue1()
    {
        // Arrange & Act
        var level = MoodLevel.Low;

        // Assert
        Assert.That((int)level, Is.EqualTo(1));
    }

    [Test]
    public void MoodLevel_Neutral_HasValue2()
    {
        // Arrange & Act
        var level = MoodLevel.Neutral;

        // Assert
        Assert.That((int)level, Is.EqualTo(2));
    }

    [Test]
    public void MoodLevel_Good_HasValue3()
    {
        // Arrange & Act
        var level = MoodLevel.Good;

        // Assert
        Assert.That((int)level, Is.EqualTo(3));
    }

    [Test]
    public void MoodLevel_Excellent_HasValue4()
    {
        // Arrange & Act
        var level = MoodLevel.Excellent;

        // Assert
        Assert.That((int)level, Is.EqualTo(4));
    }

    [Test]
    public void MoodLevel_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<MoodLevel>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(5));
            Assert.That(values, Contains.Item(MoodLevel.VeryLow));
            Assert.That(values, Contains.Item(MoodLevel.Low));
            Assert.That(values, Contains.Item(MoodLevel.Neutral));
            Assert.That(values, Contains.Item(MoodLevel.Good));
            Assert.That(values, Contains.Item(MoodLevel.Excellent));
        });
    }

    [Test]
    public void MoodLevel_CanCompareValues_InAscendingOrder()
    {
        // Arrange & Act & Assert
        Assert.That(MoodLevel.VeryLow < MoodLevel.Low, Is.True);
        Assert.That(MoodLevel.Low < MoodLevel.Neutral, Is.True);
        Assert.That(MoodLevel.Neutral < MoodLevel.Good, Is.True);
        Assert.That(MoodLevel.Good < MoodLevel.Excellent, Is.True);
    }
}
