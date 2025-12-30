// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class ApplianceTypeTests
{
    [Test]
    public void ApplianceType_Refrigerator_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.Refrigerator;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(0));
    }

    [Test]
    public void ApplianceType_Oven_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.Oven;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(1));
    }

    [Test]
    public void ApplianceType_Dishwasher_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.Dishwasher;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(2));
    }

    [Test]
    public void ApplianceType_WasherDryer_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.WasherDryer;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(3));
    }

    [Test]
    public void ApplianceType_HVAC_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.HVAC;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(4));
    }

    [Test]
    public void ApplianceType_WaterHeater_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.WaterHeater;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(5));
    }

    [Test]
    public void ApplianceType_SmallAppliance_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.SmallAppliance;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(6));
    }

    [Test]
    public void ApplianceType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var applianceType = ApplianceType.Other;

        // Assert
        Assert.That((int)applianceType, Is.EqualTo(7));
    }

    [Test]
    public void ApplianceType_CanBeAssignedToVariable()
    {
        // Arrange & Act
        ApplianceType applianceType = ApplianceType.Refrigerator;

        // Assert
        Assert.That(applianceType, Is.EqualTo(ApplianceType.Refrigerator));
    }

    [Test]
    public void ApplianceType_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<ApplianceType>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(8));
        Assert.That(allValues, Contains.Item(ApplianceType.Refrigerator));
        Assert.That(allValues, Contains.Item(ApplianceType.Oven));
        Assert.That(allValues, Contains.Item(ApplianceType.Dishwasher));
        Assert.That(allValues, Contains.Item(ApplianceType.WasherDryer));
        Assert.That(allValues, Contains.Item(ApplianceType.HVAC));
        Assert.That(allValues, Contains.Item(ApplianceType.WaterHeater));
        Assert.That(allValues, Contains.Item(ApplianceType.SmallAppliance));
        Assert.That(allValues, Contains.Item(ApplianceType.Other));
    }
}
