// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core.Tests;

public class ModificationTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var modification = new Modification
        {
            ModificationId = Guid.NewGuid(),
            Name = "Cold Air Intake",
            Category = ModCategory.Engine,
            Description = "Improves airflow to engine",
            Manufacturer = "K&N"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modification.ModificationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(modification.Name, Is.EqualTo("Cold Air Intake"));
            Assert.That(modification.Category, Is.EqualTo(ModCategory.Engine));
            Assert.That(modification.Description, Is.EqualTo("Improves airflow to engine"));
            Assert.That(modification.Manufacturer, Is.EqualTo("K&N"));
            Assert.That(modification.CompatibleVehicles, Is.Not.Null);
            Assert.That(modification.RequiredTools, Is.Not.Null);
            Assert.That(modification.Installations, Is.Not.Null);
        });
    }

    [Test]
    public void AddCompatibleVehicles_AddsSingleVehicle()
    {
        // Arrange
        var modification = new Modification();
        var vehicles = new[] { "2020 Honda Civic" };

        // Act
        modification.AddCompatibleVehicles(vehicles);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modification.CompatibleVehicles, Has.Count.EqualTo(1));
            Assert.That(modification.CompatibleVehicles[0], Is.EqualTo("2020 Honda Civic"));
        });
    }

    [Test]
    public void AddCompatibleVehicles_AddsMultipleVehicles()
    {
        // Arrange
        var modification = new Modification();
        var vehicles = new[] { "2019-2021 Honda Civic", "2020-2022 Honda Accord", "2021 Acura TLX" };

        // Act
        modification.AddCompatibleVehicles(vehicles);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modification.CompatibleVehicles, Has.Count.EqualTo(3));
            Assert.That(modification.CompatibleVehicles, Contains.Item("2019-2021 Honda Civic"));
            Assert.That(modification.CompatibleVehicles, Contains.Item("2020-2022 Honda Accord"));
        });
    }

    [Test]
    public void AddRequiredTools_AddsSingleTool()
    {
        // Arrange
        var modification = new Modification();
        var tools = new[] { "Socket wrench set" };

        // Act
        modification.AddRequiredTools(tools);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modification.RequiredTools, Has.Count.EqualTo(1));
            Assert.That(modification.RequiredTools[0], Is.EqualTo("Socket wrench set"));
        });
    }

    [Test]
    public void AddRequiredTools_AddsMultipleTools()
    {
        // Arrange
        var modification = new Modification();
        var tools = new[] { "Socket set", "Torque wrench", "Jack stands", "Floor jack" };

        // Act
        modification.AddRequiredTools(tools);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modification.RequiredTools, Has.Count.EqualTo(4));
            Assert.That(modification.RequiredTools, Contains.Item("Torque wrench"));
            Assert.That(modification.RequiredTools, Contains.Item("Floor jack"));
        });
    }

    [Test]
    public void SetDifficultyLevel_ValidLevel_SetsLevel()
    {
        // Arrange
        var modification = new Modification();

        // Act
        modification.SetDifficultyLevel(3);

        // Assert
        Assert.That(modification.DifficultyLevel, Is.EqualTo(3));
    }

    [Test]
    public void SetDifficultyLevel_Level1_IsValid()
    {
        // Arrange
        var modification = new Modification();

        // Act
        modification.SetDifficultyLevel(1);

        // Assert
        Assert.That(modification.DifficultyLevel, Is.EqualTo(1));
    }

    [Test]
    public void SetDifficultyLevel_Level5_IsValid()
    {
        // Arrange
        var modification = new Modification();

        // Act
        modification.SetDifficultyLevel(5);

        // Assert
        Assert.That(modification.DifficultyLevel, Is.EqualTo(5));
    }

    [Test]
    public void SetDifficultyLevel_Level0_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var modification = new Modification();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => modification.SetDifficultyLevel(0));
    }

    [Test]
    public void SetDifficultyLevel_Level6_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var modification = new Modification();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => modification.SetDifficultyLevel(6));
    }

    [Test]
    public void SetDifficultyLevel_NegativeLevel_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var modification = new Modification();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => modification.SetDifficultyLevel(-1));
    }

    [Test]
    public void Modification_WithEstimatedCost_StoresCostCorrectly()
    {
        // Arrange & Act
        var modification = new Modification
        {
            EstimatedCost = 599.99m
        };

        // Assert
        Assert.That(modification.EstimatedCost, Is.EqualTo(599.99m));
    }

    [Test]
    public void Modification_WithEstimatedInstallationTime_StoresTimeCorrectly()
    {
        // Arrange & Act
        var modification = new Modification
        {
            EstimatedInstallationTime = 2.5m
        };

        // Assert
        Assert.That(modification.EstimatedInstallationTime, Is.EqualTo(2.5m));
    }

    [Test]
    public void Modification_WithPerformanceGain_StoresGainCorrectly()
    {
        // Arrange & Act
        var modification = new Modification
        {
            PerformanceGain = "+15 HP, +10 lb-ft torque"
        };

        // Assert
        Assert.That(modification.PerformanceGain, Is.EqualTo("+15 HP, +10 lb-ft torque"));
    }

    [Test]
    public void Modification_AllCategoriesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var categories = Enum.GetValues<ModCategory>();
        foreach (var category in categories)
        {
            var modification = new Modification { Category = category };
            Assert.That(modification.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void Modification_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var modification = new Modification
        {
            Notes = "May void warranty"
        };

        // Assert
        Assert.That(modification.Notes, Is.EqualTo("May void warranty"));
    }
}
