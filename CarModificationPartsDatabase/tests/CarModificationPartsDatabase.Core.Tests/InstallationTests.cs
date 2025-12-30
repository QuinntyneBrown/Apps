// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core.Tests;

public class InstallationTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange
        var installationDate = new DateTime(2024, 6, 15);

        // Act
        var installation = new Installation
        {
            InstallationId = Guid.NewGuid(),
            ModificationId = Guid.NewGuid(),
            VehicleInfo = "2020 Honda Civic",
            InstallationDate = installationDate,
            InstalledBy = "Joe's Auto Shop"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(installation.InstallationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(installation.ModificationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(installation.VehicleInfo, Is.EqualTo("2020 Honda Civic"));
            Assert.That(installation.InstallationDate, Is.EqualTo(installationDate));
            Assert.That(installation.InstalledBy, Is.EqualTo("Joe's Auto Shop"));
            Assert.That(installation.IsCompleted, Is.False);
            Assert.That(installation.PartsUsed, Is.Not.Null);
            Assert.That(installation.Photos, Is.Not.Null);
        });
    }

    [Test]
    public void MarkAsCompleted_SetsIsCompletedAndDate()
    {
        // Arrange
        var installation = new Installation
        {
            IsCompleted = false
        };
        var completionDate = new DateTime(2024, 7, 1);

        // Act
        installation.MarkAsCompleted(completionDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(installation.IsCompleted, Is.True);
            Assert.That(installation.InstallationDate, Is.EqualTo(completionDate));
        });
    }

    [Test]
    public void AddParts_AddsSinglePart()
    {
        // Arrange
        var installation = new Installation();
        var parts = new[] { "Cold air intake" };

        // Act
        installation.AddParts(parts);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(installation.PartsUsed, Has.Count.EqualTo(1));
            Assert.That(installation.PartsUsed[0], Is.EqualTo("Cold air intake"));
        });
    }

    [Test]
    public void AddParts_AddsMultipleParts()
    {
        // Arrange
        var installation = new Installation();
        var parts = new[] { "Turbocharger", "Intercooler", "Exhaust manifold" };

        // Act
        installation.AddParts(parts);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(installation.PartsUsed, Has.Count.EqualTo(3));
            Assert.That(installation.PartsUsed, Contains.Item("Turbocharger"));
            Assert.That(installation.PartsUsed, Contains.Item("Intercooler"));
            Assert.That(installation.PartsUsed, Contains.Item("Exhaust manifold"));
        });
    }

    [Test]
    public void SetSatisfactionRating_ValidRating_SetsRating()
    {
        // Arrange
        var installation = new Installation();

        // Act
        installation.SetSatisfactionRating(5);

        // Assert
        Assert.That(installation.SatisfactionRating, Is.EqualTo(5));
    }

    [Test]
    public void SetSatisfactionRating_Rating1_IsValid()
    {
        // Arrange
        var installation = new Installation();

        // Act
        installation.SetSatisfactionRating(1);

        // Assert
        Assert.That(installation.SatisfactionRating, Is.EqualTo(1));
    }

    [Test]
    public void SetSatisfactionRating_Rating0_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var installation = new Installation();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => installation.SetSatisfactionRating(0));
    }

    [Test]
    public void SetSatisfactionRating_Rating6_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var installation = new Installation();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => installation.SetSatisfactionRating(6));
    }

    [Test]
    public void SetSatisfactionRating_NegativeRating_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var installation = new Installation();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => installation.SetSatisfactionRating(-1));
    }

    [Test]
    public void AddPhotos_AddsSinglePhoto()
    {
        // Arrange
        var installation = new Installation();
        var photos = new[] { "https://example.com/photo1.jpg" };

        // Act
        installation.AddPhotos(photos);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(installation.Photos, Has.Count.EqualTo(1));
            Assert.That(installation.Photos[0], Is.EqualTo("https://example.com/photo1.jpg"));
        });
    }

    [Test]
    public void AddPhotos_AddsMultiplePhotos()
    {
        // Arrange
        var installation = new Installation();
        var photos = new[] { "photo1.jpg", "photo2.jpg", "photo3.jpg" };

        // Act
        installation.AddPhotos(photos);

        // Assert
        Assert.That(installation.Photos, Has.Count.EqualTo(3));
    }

    [Test]
    public void GetTotalCost_WithBothCosts_ReturnsSum()
    {
        // Arrange
        var installation = new Installation
        {
            InstallationCost = 500m,
            PartsCost = 1200m
        };

        // Act
        var totalCost = installation.GetTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(1700m));
    }

    [Test]
    public void GetTotalCost_WithOnlyPartsCost_ReturnsPartsCost()
    {
        // Arrange
        var installation = new Installation
        {
            InstallationCost = null,
            PartsCost = 800m
        };

        // Act
        var totalCost = installation.GetTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(800m));
    }

    [Test]
    public void GetTotalCost_WithNoCosts_ReturnsZero()
    {
        // Arrange
        var installation = new Installation
        {
            InstallationCost = null,
            PartsCost = null
        };

        // Act
        var totalCost = installation.GetTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(0m));
    }

    [Test]
    public void Installation_WithLaborHours_StoresHoursCorrectly()
    {
        // Arrange & Act
        var installation = new Installation
        {
            LaborHours = 12.5m
        };

        // Assert
        Assert.That(installation.LaborHours, Is.EqualTo(12.5m));
    }

    [Test]
    public void Installation_WithDifficultyRating_StoresRatingCorrectly()
    {
        // Arrange & Act
        var installation = new Installation
        {
            DifficultyRating = 4
        };

        // Assert
        Assert.That(installation.DifficultyRating, Is.EqualTo(4));
    }

    [Test]
    public void Installation_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var installation = new Installation
        {
            Notes = "Installation went smoothly"
        };

        // Assert
        Assert.That(installation.Notes, Is.EqualTo("Installation went smoothly"));
    }
}
