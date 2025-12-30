// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core.Tests;

public class MedicationTypeTests
{
    [Test]
    public void MedicationType_TabletValue_IsZero()
    {
        // Act
        var value = MedicationType.Tablet;

        // Assert
        Assert.That((int)value, Is.EqualTo(0));
    }

    [Test]
    public void MedicationType_CapsuleValue_IsOne()
    {
        // Act
        var value = MedicationType.Capsule;

        // Assert
        Assert.That((int)value, Is.EqualTo(1));
    }

    [Test]
    public void MedicationType_LiquidValue_IsTwo()
    {
        // Act
        var value = MedicationType.Liquid;

        // Assert
        Assert.That((int)value, Is.EqualTo(2));
    }

    [Test]
    public void MedicationType_InjectionValue_IsThree()
    {
        // Act
        var value = MedicationType.Injection;

        // Assert
        Assert.That((int)value, Is.EqualTo(3));
    }

    [Test]
    public void MedicationType_TopicalValue_IsFour()
    {
        // Act
        var value = MedicationType.Topical;

        // Assert
        Assert.That((int)value, Is.EqualTo(4));
    }

    [Test]
    public void MedicationType_InhalerValue_IsFive()
    {
        // Act
        var value = MedicationType.Inhaler;

        // Assert
        Assert.That((int)value, Is.EqualTo(5));
    }

    [Test]
    public void MedicationType_PatchValue_IsSix()
    {
        // Act
        var value = MedicationType.Patch;

        // Assert
        Assert.That((int)value, Is.EqualTo(6));
    }

    [Test]
    public void MedicationType_OtherValue_IsSeven()
    {
        // Act
        var value = MedicationType.Other;

        // Assert
        Assert.That((int)value, Is.EqualTo(7));
    }

    [Test]
    public void MedicationType_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var tablet = MedicationType.Tablet;
        var capsule = MedicationType.Capsule;
        var liquid = MedicationType.Liquid;
        var injection = MedicationType.Injection;
        var topical = MedicationType.Topical;
        var inhaler = MedicationType.Inhaler;
        var patch = MedicationType.Patch;
        var other = MedicationType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tablet, Is.EqualTo(MedicationType.Tablet));
            Assert.That(capsule, Is.EqualTo(MedicationType.Capsule));
            Assert.That(liquid, Is.EqualTo(MedicationType.Liquid));
            Assert.That(injection, Is.EqualTo(MedicationType.Injection));
            Assert.That(topical, Is.EqualTo(MedicationType.Topical));
            Assert.That(inhaler, Is.EqualTo(MedicationType.Inhaler));
            Assert.That(patch, Is.EqualTo(MedicationType.Patch));
            Assert.That(other, Is.EqualTo(MedicationType.Other));
        });
    }

    [Test]
    public void MedicationType_CanBeCompared()
    {
        // Arrange
        var type1 = MedicationType.Tablet;
        var type2 = MedicationType.Tablet;
        var type3 = MedicationType.Capsule;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(type1, Is.EqualTo(type2));
            Assert.That(type1, Is.Not.EqualTo(type3));
        });
    }

    [Test]
    public void MedicationType_CanBeCastToInt()
    {
        // Arrange
        var medicationType = MedicationType.Injection;

        // Act
        int intValue = (int)medicationType;

        // Assert
        Assert.That(intValue, Is.EqualTo(3));
    }

    [Test]
    public void MedicationType_CanBeCastFromInt()
    {
        // Arrange
        int intValue = 5;

        // Act
        var medicationType = (MedicationType)intValue;

        // Assert
        Assert.That(medicationType, Is.EqualTo(MedicationType.Inhaler));
    }
}
