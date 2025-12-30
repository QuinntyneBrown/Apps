// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class MaintenanceTypeTests
{
    [Test]
    public void MaintenanceType_Preventive_HasCorrectValue()
    {
        // Arrange & Act
        var type = MaintenanceType.Preventive;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void MaintenanceType_Corrective_HasCorrectValue()
    {
        // Arrange & Act
        var type = MaintenanceType.Corrective;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void MaintenanceType_Seasonal_HasCorrectValue()
    {
        // Arrange & Act
        var type = MaintenanceType.Seasonal;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void MaintenanceType_Emergency_HasCorrectValue()
    {
        // Arrange & Act
        var type = MaintenanceType.Emergency;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void MaintenanceType_Inspection_HasCorrectValue()
    {
        // Arrange & Act
        var type = MaintenanceType.Inspection;

        // Assert
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void MaintenanceType_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var preventive = MaintenanceType.Preventive;
        var corrective = MaintenanceType.Corrective;
        var seasonal = MaintenanceType.Seasonal;
        var emergency = MaintenanceType.Emergency;
        var inspection = MaintenanceType.Inspection;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(preventive, Is.EqualTo(MaintenanceType.Preventive));
            Assert.That(corrective, Is.EqualTo(MaintenanceType.Corrective));
            Assert.That(seasonal, Is.EqualTo(MaintenanceType.Seasonal));
            Assert.That(emergency, Is.EqualTo(MaintenanceType.Emergency));
            Assert.That(inspection, Is.EqualTo(MaintenanceType.Inspection));
        });
    }

    [Test]
    public void MaintenanceType_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var preventiveName = MaintenanceType.Preventive.ToString();
        var correctiveName = MaintenanceType.Corrective.ToString();
        var seasonalName = MaintenanceType.Seasonal.ToString();
        var emergencyName = MaintenanceType.Emergency.ToString();
        var inspectionName = MaintenanceType.Inspection.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(preventiveName, Is.EqualTo("Preventive"));
            Assert.That(correctiveName, Is.EqualTo("Corrective"));
            Assert.That(seasonalName, Is.EqualTo("Seasonal"));
            Assert.That(emergencyName, Is.EqualTo("Emergency"));
            Assert.That(inspectionName, Is.EqualTo("Inspection"));
        });
    }

    [Test]
    public void MaintenanceType_CanBeCompared()
    {
        // Arrange
        var type1 = MaintenanceType.Preventive;
        var type2 = MaintenanceType.Preventive;
        var type3 = MaintenanceType.Emergency;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(type1, Is.EqualTo(type2));
            Assert.That(type1, Is.Not.EqualTo(type3));
        });
    }

    [Test]
    public void MaintenanceType_CanBeUsedInSwitch()
    {
        // Arrange
        var type = MaintenanceType.Seasonal;
        string result;

        // Act
        result = type switch
        {
            MaintenanceType.Preventive => "Preventive",
            MaintenanceType.Corrective => "Corrective",
            MaintenanceType.Seasonal => "Seasonal",
            MaintenanceType.Emergency => "Emergency",
            MaintenanceType.Inspection => "Inspection",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Seasonal"));
    }

    [Test]
    public void MaintenanceType_EnumParse_WorksCorrectly()
    {
        // Arrange
        var typeName = "Emergency";

        // Act
        var parsed = Enum.Parse<MaintenanceType>(typeName);

        // Assert
        Assert.That(parsed, Is.EqualTo(MaintenanceType.Emergency));
    }
}
