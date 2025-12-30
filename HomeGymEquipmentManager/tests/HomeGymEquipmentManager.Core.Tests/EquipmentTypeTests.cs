// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class EquipmentTypeTests
{
    [Test]
    public void EquipmentType_Cardio_HasCorrectValue()
    {
        // Arrange & Act
        var equipmentType = EquipmentType.Cardio;

        // Assert
        Assert.That((int)equipmentType, Is.EqualTo(0));
    }

    [Test]
    public void EquipmentType_Strength_HasCorrectValue()
    {
        // Arrange & Act
        var equipmentType = EquipmentType.Strength;

        // Assert
        Assert.That((int)equipmentType, Is.EqualTo(1));
    }

    [Test]
    public void EquipmentType_Flexibility_HasCorrectValue()
    {
        // Arrange & Act
        var equipmentType = EquipmentType.Flexibility;

        // Assert
        Assert.That((int)equipmentType, Is.EqualTo(2));
    }

    [Test]
    public void EquipmentType_Accessory_HasCorrectValue()
    {
        // Arrange & Act
        var equipmentType = EquipmentType.Accessory;

        // Assert
        Assert.That((int)equipmentType, Is.EqualTo(3));
    }

    [Test]
    public void EquipmentType_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var cardio = EquipmentType.Cardio;
        var strength = EquipmentType.Strength;
        var flexibility = EquipmentType.Flexibility;
        var accessory = EquipmentType.Accessory;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cardio, Is.EqualTo(EquipmentType.Cardio));
            Assert.That(strength, Is.EqualTo(EquipmentType.Strength));
            Assert.That(flexibility, Is.EqualTo(EquipmentType.Flexibility));
            Assert.That(accessory, Is.EqualTo(EquipmentType.Accessory));
        });
    }

    [Test]
    public void EquipmentType_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var cardioName = EquipmentType.Cardio.ToString();
        var strengthName = EquipmentType.Strength.ToString();
        var flexibilityName = EquipmentType.Flexibility.ToString();
        var accessoryName = EquipmentType.Accessory.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cardioName, Is.EqualTo("Cardio"));
            Assert.That(strengthName, Is.EqualTo("Strength"));
            Assert.That(flexibilityName, Is.EqualTo("Flexibility"));
            Assert.That(accessoryName, Is.EqualTo("Accessory"));
        });
    }

    [Test]
    public void EquipmentType_CanBeCompared()
    {
        // Arrange
        var type1 = EquipmentType.Cardio;
        var type2 = EquipmentType.Cardio;
        var type3 = EquipmentType.Strength;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(type1, Is.EqualTo(type2));
            Assert.That(type1, Is.Not.EqualTo(type3));
        });
    }

    [Test]
    public void EquipmentType_CanBeUsedInSwitch()
    {
        // Arrange
        var equipmentType = EquipmentType.Strength;
        string result;

        // Act
        result = equipmentType switch
        {
            EquipmentType.Cardio => "Cardio",
            EquipmentType.Strength => "Strength",
            EquipmentType.Flexibility => "Flexibility",
            EquipmentType.Accessory => "Accessory",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Strength"));
    }

    [Test]
    public void EquipmentType_EnumParse_WorksCorrectly()
    {
        // Arrange
        var typeName = "Flexibility";

        // Act
        var parsed = Enum.Parse<EquipmentType>(typeName);

        // Assert
        Assert.That(parsed, Is.EqualTo(EquipmentType.Flexibility));
    }
}
