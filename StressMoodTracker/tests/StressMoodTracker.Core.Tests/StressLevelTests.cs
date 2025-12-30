// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core.Tests;

public class StressLevelTests
{
    [Test]
    public void StressLevel_None_HasValue0()
    {
        // Arrange & Act
        var level = StressLevel.None;

        // Assert
        Assert.That((int)level, Is.EqualTo(0));
    }

    [Test]
    public void StressLevel_Low_HasValue1()
    {
        // Arrange & Act
        var level = StressLevel.Low;

        // Assert
        Assert.That((int)level, Is.EqualTo(1));
    }

    [Test]
    public void StressLevel_Moderate_HasValue2()
    {
        // Arrange & Act
        var level = StressLevel.Moderate;

        // Assert
        Assert.That((int)level, Is.EqualTo(2));
    }

    [Test]
    public void StressLevel_High_HasValue3()
    {
        // Arrange & Act
        var level = StressLevel.High;

        // Assert
        Assert.That((int)level, Is.EqualTo(3));
    }

    [Test]
    public void StressLevel_VeryHigh_HasValue4()
    {
        // Arrange & Act
        var level = StressLevel.VeryHigh;

        // Assert
        Assert.That((int)level, Is.EqualTo(4));
    }

    [Test]
    public void StressLevel_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<StressLevel>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(5));
            Assert.That(values, Contains.Item(StressLevel.None));
            Assert.That(values, Contains.Item(StressLevel.Low));
            Assert.That(values, Contains.Item(StressLevel.Moderate));
            Assert.That(values, Contains.Item(StressLevel.High));
            Assert.That(values, Contains.Item(StressLevel.VeryHigh));
        });
    }

    [Test]
    public void StressLevel_CanCompareValues_InAscendingOrder()
    {
        // Arrange & Act & Assert
        Assert.That(StressLevel.None < StressLevel.Low, Is.True);
        Assert.That(StressLevel.Low < StressLevel.Moderate, Is.True);
        Assert.That(StressLevel.Moderate < StressLevel.High, Is.True);
        Assert.That(StressLevel.High < StressLevel.VeryHigh, Is.True);
    }
}
