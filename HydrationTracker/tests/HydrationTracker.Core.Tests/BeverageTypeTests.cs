// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class BeverageTypeTests
{
    [Test]
    public void BeverageType_WaterValue_EqualsZero()
    {
        // Assert
        Assert.That((int)BeverageType.Water, Is.EqualTo(0));
    }

    [Test]
    public void BeverageType_TeaValue_EqualsOne()
    {
        // Assert
        Assert.That((int)BeverageType.Tea, Is.EqualTo(1));
    }

    [Test]
    public void BeverageType_CoffeeValue_EqualsTwo()
    {
        // Assert
        Assert.That((int)BeverageType.Coffee, Is.EqualTo(2));
    }

    [Test]
    public void BeverageType_JuiceValue_EqualsThree()
    {
        // Assert
        Assert.That((int)BeverageType.Juice, Is.EqualTo(3));
    }

    [Test]
    public void BeverageType_SportsValue_EqualsFour()
    {
        // Assert
        Assert.That((int)BeverageType.Sports, Is.EqualTo(4));
    }

    [Test]
    public void BeverageType_OtherValue_EqualsFive()
    {
        // Assert
        Assert.That((int)BeverageType.Other, Is.EqualTo(5));
    }

    [Test]
    public void BeverageType_AllValues_CanBeAssigned()
    {
        // Act & Assert
        BeverageType type;

        Assert.DoesNotThrow(() => type = BeverageType.Water);
        Assert.DoesNotThrow(() => type = BeverageType.Tea);
        Assert.DoesNotThrow(() => type = BeverageType.Coffee);
        Assert.DoesNotThrow(() => type = BeverageType.Juice);
        Assert.DoesNotThrow(() => type = BeverageType.Sports);
        Assert.DoesNotThrow(() => type = BeverageType.Other);
    }

    [Test]
    public void BeverageType_DefaultValue_IsWater()
    {
        // Arrange
        BeverageType type = default;

        // Assert
        Assert.That(type, Is.EqualTo(BeverageType.Water));
    }

    [Test]
    public void BeverageType_CanCompareValues()
    {
        // Arrange
        var water = BeverageType.Water;
        var tea = BeverageType.Tea;
        var coffee = BeverageType.Coffee;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(water, Is.Not.EqualTo(tea));
            Assert.That(tea, Is.Not.EqualTo(coffee));
            Assert.That(water, Is.EqualTo(BeverageType.Water));
        });
    }
}
