// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class UtilityTypeTests
{
    [Test]
    public void UtilityType_Electricity_HasCorrectValue()
    {
        // Arrange & Act
        var utilityType = UtilityType.Electricity;

        // Assert
        Assert.That((int)utilityType, Is.EqualTo(0));
    }

    [Test]
    public void UtilityType_Gas_HasCorrectValue()
    {
        // Arrange & Act
        var utilityType = UtilityType.Gas;

        // Assert
        Assert.That((int)utilityType, Is.EqualTo(1));
    }

    [Test]
    public void UtilityType_Water_HasCorrectValue()
    {
        // Arrange & Act
        var utilityType = UtilityType.Water;

        // Assert
        Assert.That((int)utilityType, Is.EqualTo(2));
    }

    [Test]
    public void UtilityType_Internet_HasCorrectValue()
    {
        // Arrange & Act
        var utilityType = UtilityType.Internet;

        // Assert
        Assert.That((int)utilityType, Is.EqualTo(3));
    }

    [Test]
    public void UtilityType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var utilityType = UtilityType.Other;

        // Assert
        Assert.That((int)utilityType, Is.EqualTo(4));
    }

    [Test]
    public void UtilityType_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var electricity = UtilityType.Electricity;
        var gas = UtilityType.Gas;
        var water = UtilityType.Water;
        var internet = UtilityType.Internet;
        var other = UtilityType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(electricity, Is.EqualTo(UtilityType.Electricity));
            Assert.That(gas, Is.EqualTo(UtilityType.Gas));
            Assert.That(water, Is.EqualTo(UtilityType.Water));
            Assert.That(internet, Is.EqualTo(UtilityType.Internet));
            Assert.That(other, Is.EqualTo(UtilityType.Other));
        });
    }

    [Test]
    public void UtilityType_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var electricityName = UtilityType.Electricity.ToString();
        var gasName = UtilityType.Gas.ToString();
        var waterName = UtilityType.Water.ToString();
        var internetName = UtilityType.Internet.ToString();
        var otherName = UtilityType.Other.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(electricityName, Is.EqualTo("Electricity"));
            Assert.That(gasName, Is.EqualTo("Gas"));
            Assert.That(waterName, Is.EqualTo("Water"));
            Assert.That(internetName, Is.EqualTo("Internet"));
            Assert.That(otherName, Is.EqualTo("Other"));
        });
    }

    [Test]
    public void UtilityType_CanBeCompared()
    {
        // Arrange
        var type1 = UtilityType.Electricity;
        var type2 = UtilityType.Electricity;
        var type3 = UtilityType.Gas;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(type1, Is.EqualTo(type2));
            Assert.That(type1, Is.Not.EqualTo(type3));
        });
    }

    [Test]
    public void UtilityType_CanBeUsedInSwitch()
    {
        // Arrange
        var utilityType = UtilityType.Water;
        string result;

        // Act
        result = utilityType switch
        {
            UtilityType.Electricity => "Electric",
            UtilityType.Gas => "Gas",
            UtilityType.Water => "Water",
            UtilityType.Internet => "Internet",
            UtilityType.Other => "Other",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Water"));
    }

    [Test]
    public void UtilityType_EnumParse_WorksCorrectly()
    {
        // Arrange
        var typeName = "Electricity";

        // Act
        var parsed = Enum.Parse<UtilityType>(typeName);

        // Assert
        Assert.That(parsed, Is.EqualTo(UtilityType.Electricity));
    }
}
