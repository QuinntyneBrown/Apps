// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class VaccinationTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesVaccination()
    {
        // Arrange & Act
        var vaccination = new Vaccination();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vaccination.VaccinationId, Is.EqualTo(Guid.Empty));
            Assert.That(vaccination.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(vaccination.Pet, Is.Null);
            Assert.That(vaccination.Name, Is.EqualTo(string.Empty));
            Assert.That(vaccination.DateAdministered, Is.EqualTo(default(DateTime)));
            Assert.That(vaccination.NextDueDate, Is.Null);
            Assert.That(vaccination.VetName, Is.Null);
            Assert.That(vaccination.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var vaccinationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var dateAdministered = DateTime.UtcNow.AddMonths(-6);
        var nextDueDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var vaccination = new Vaccination
        {
            VaccinationId = vaccinationId,
            PetId = petId,
            Name = "Rabies",
            DateAdministered = dateAdministered,
            NextDueDate = nextDueDate,
            VetName = "Dr. Johnson"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vaccination.VaccinationId, Is.EqualTo(vaccinationId));
            Assert.That(vaccination.PetId, Is.EqualTo(petId));
            Assert.That(vaccination.Name, Is.EqualTo("Rabies"));
            Assert.That(vaccination.DateAdministered, Is.EqualTo(dateAdministered));
            Assert.That(vaccination.NextDueDate, Is.EqualTo(nextDueDate));
            Assert.That(vaccination.VetName, Is.EqualTo("Dr. Johnson"));
        });
    }

    [Test]
    public void Pet_NavigationProperty_CanBeSet()
    {
        // Arrange
        var vaccination = new Vaccination();
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            Name = "Buddy"
        };

        // Act
        vaccination.Pet = pet;

        // Assert
        Assert.That(vaccination.Pet, Is.EqualTo(pet));
    }

    [Test]
    public void NextDueDate_CanBeNull()
    {
        // Arrange & Act
        var vaccination = new Vaccination { NextDueDate = null };

        // Assert
        Assert.That(vaccination.NextDueDate, Is.Null);
    }

    [Test]
    public void VetName_CanBeNull()
    {
        // Arrange & Act
        var vaccination = new Vaccination { VetName = null };

        // Assert
        Assert.That(vaccination.VetName, Is.Null);
    }

    [Test]
    public void Name_CanBeSet()
    {
        // Arrange & Act
        var vaccination = new Vaccination { Name = "DHPP" };

        // Assert
        Assert.That(vaccination.Name, Is.EqualTo("DHPP"));
    }
}
