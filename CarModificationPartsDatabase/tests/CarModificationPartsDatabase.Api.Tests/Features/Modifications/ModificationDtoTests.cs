// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Api.Tests;

/// <summary>
/// Unit tests for ModificationDto mapping.
/// </summary>
[TestFixture]
public class ModificationDtoTests
{
    /// <summary>
    /// Tests that ToDto maps all properties correctly.
    /// </summary>
    [Test]
    public void ToDto_MapsAllProperties()
    {
        // Arrange
        var modification = new Modification
        {
            ModificationId = Guid.NewGuid(),
            Name = "Cold Air Intake",
            Category = ModCategory.Engine,
            Description = "High-flow cold air intake system",
            Manufacturer = "K&N",
            EstimatedCost = 350.00m,
            DifficultyLevel = 2,
            EstimatedInstallationTime = 1.5m,
            PerformanceGain = "10-15 HP increase",
            CompatibleVehicles = new List<string> { "Honda Civic 2020-2023", "Honda Accord 2018-2023" },
            RequiredTools = new List<string> { "Socket set", "Screwdriver" },
            Notes = "Easy installation",
        };

        // Act
        var dto = modification.ToDto();

        // Assert
        Assert.That(dto.ModificationId, Is.EqualTo(modification.ModificationId));
        Assert.That(dto.Name, Is.EqualTo(modification.Name));
        Assert.That(dto.Category, Is.EqualTo(modification.Category));
        Assert.That(dto.Description, Is.EqualTo(modification.Description));
        Assert.That(dto.Manufacturer, Is.EqualTo(modification.Manufacturer));
        Assert.That(dto.EstimatedCost, Is.EqualTo(modification.EstimatedCost));
        Assert.That(dto.DifficultyLevel, Is.EqualTo(modification.DifficultyLevel));
        Assert.That(dto.EstimatedInstallationTime, Is.EqualTo(modification.EstimatedInstallationTime));
        Assert.That(dto.PerformanceGain, Is.EqualTo(modification.PerformanceGain));
        Assert.That(dto.CompatibleVehicles, Is.EqualTo(modification.CompatibleVehicles));
        Assert.That(dto.RequiredTools, Is.EqualTo(modification.RequiredTools));
        Assert.That(dto.Notes, Is.EqualTo(modification.Notes));
    }
}
