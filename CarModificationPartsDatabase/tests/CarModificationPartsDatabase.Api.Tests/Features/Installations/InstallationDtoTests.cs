// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Api.Tests;

/// <summary>
/// Unit tests for InstallationDto mapping.
/// </summary>
[TestFixture]
public class InstallationDtoTests
{
    /// <summary>
    /// Tests that ToDto maps all properties correctly and calculates total cost.
    /// </summary>
    [Test]
    public void ToDto_MapsAllProperties_AndCalculatesTotalCost()
    {
        // Arrange
        var installation = new Installation
        {
            InstallationId = Guid.NewGuid(),
            ModificationId = Guid.NewGuid(),
            VehicleInfo = "2020 Honda Civic Si",
            InstallationDate = DateTime.UtcNow,
            InstalledBy = "AutoWorks Performance",
            InstallationCost = 300.00m,
            PartsCost = 2500.00m,
            LaborHours = 6.0m,
            PartsUsed = new List<string> { "Turbocharger", "Intercooler", "Piping" },
            Notes = "Installation went smoothly",
            DifficultyRating = 5,
            SatisfactionRating = 5,
            Photos = new List<string> { "photo1.jpg", "photo2.jpg" },
            IsCompleted = true,
        };

        // Act
        var dto = installation.ToDto();

        // Assert
        Assert.That(dto.InstallationId, Is.EqualTo(installation.InstallationId));
        Assert.That(dto.ModificationId, Is.EqualTo(installation.ModificationId));
        Assert.That(dto.VehicleInfo, Is.EqualTo(installation.VehicleInfo));
        Assert.That(dto.InstallationDate, Is.EqualTo(installation.InstallationDate));
        Assert.That(dto.InstalledBy, Is.EqualTo(installation.InstalledBy));
        Assert.That(dto.InstallationCost, Is.EqualTo(installation.InstallationCost));
        Assert.That(dto.PartsCost, Is.EqualTo(installation.PartsCost));
        Assert.That(dto.LaborHours, Is.EqualTo(installation.LaborHours));
        Assert.That(dto.PartsUsed, Is.EqualTo(installation.PartsUsed));
        Assert.That(dto.Notes, Is.EqualTo(installation.Notes));
        Assert.That(dto.DifficultyRating, Is.EqualTo(installation.DifficultyRating));
        Assert.That(dto.SatisfactionRating, Is.EqualTo(installation.SatisfactionRating));
        Assert.That(dto.Photos, Is.EqualTo(installation.Photos));
        Assert.That(dto.IsCompleted, Is.EqualTo(installation.IsCompleted));
        Assert.That(dto.TotalCost, Is.EqualTo(2800.00m));
    }
}
