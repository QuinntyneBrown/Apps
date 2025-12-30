// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core.Tests;

public class SleepQualityTests
{
    [Test]
    public void SleepQuality_VeryPoor_HasValue0()
    {
        // Arrange & Act
        var quality = SleepQuality.VeryPoor;

        // Assert
        Assert.That((int)quality, Is.EqualTo(0));
    }

    [Test]
    public void SleepQuality_Poor_HasValue1()
    {
        // Arrange & Act
        var quality = SleepQuality.Poor;

        // Assert
        Assert.That((int)quality, Is.EqualTo(1));
    }

    [Test]
    public void SleepQuality_Fair_HasValue2()
    {
        // Arrange & Act
        var quality = SleepQuality.Fair;

        // Assert
        Assert.That((int)quality, Is.EqualTo(2));
    }

    [Test]
    public void SleepQuality_Good_HasValue3()
    {
        // Arrange & Act
        var quality = SleepQuality.Good;

        // Assert
        Assert.That((int)quality, Is.EqualTo(3));
    }

    [Test]
    public void SleepQuality_Excellent_HasValue4()
    {
        // Arrange & Act
        var quality = SleepQuality.Excellent;

        // Assert
        Assert.That((int)quality, Is.EqualTo(4));
    }

    [Test]
    public void SleepQuality_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<SleepQuality>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(5));
            Assert.That(values, Contains.Item(SleepQuality.VeryPoor));
            Assert.That(values, Contains.Item(SleepQuality.Poor));
            Assert.That(values, Contains.Item(SleepQuality.Fair));
            Assert.That(values, Contains.Item(SleepQuality.Good));
            Assert.That(values, Contains.Item(SleepQuality.Excellent));
        });
    }

    [Test]
    public void SleepQuality_CanCompareValues_InAscendingOrder()
    {
        // Arrange & Act & Assert
        Assert.That(SleepQuality.VeryPoor < SleepQuality.Poor, Is.True);
        Assert.That(SleepQuality.Poor < SleepQuality.Fair, Is.True);
        Assert.That(SleepQuality.Fair < SleepQuality.Good, Is.True);
        Assert.That(SleepQuality.Good < SleepQuality.Excellent, Is.True);
    }
}
