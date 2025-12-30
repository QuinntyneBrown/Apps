// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class MedicationTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesMedication()
    {
        // Arrange & Act
        var medication = new Medication();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.MedicationId, Is.EqualTo(Guid.Empty));
            Assert.That(medication.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(medication.Pet, Is.Null);
            Assert.That(medication.Name, Is.EqualTo(string.Empty));
            Assert.That(medication.Dosage, Is.Null);
            Assert.That(medication.Frequency, Is.Null);
            Assert.That(medication.StartDate, Is.Null);
            Assert.That(medication.EndDate, Is.Null);
            Assert.That(medication.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(30);

        // Act
        var medication = new Medication
        {
            MedicationId = medicationId,
            PetId = petId,
            Name = "Antibiotics",
            Dosage = "10mg",
            Frequency = "Twice daily",
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.MedicationId, Is.EqualTo(medicationId));
            Assert.That(medication.PetId, Is.EqualTo(petId));
            Assert.That(medication.Name, Is.EqualTo("Antibiotics"));
            Assert.That(medication.Dosage, Is.EqualTo("10mg"));
            Assert.That(medication.Frequency, Is.EqualTo("Twice daily"));
            Assert.That(medication.StartDate, Is.EqualTo(startDate));
            Assert.That(medication.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void Pet_NavigationProperty_CanBeSet()
    {
        // Arrange
        var medication = new Medication();
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            Name = "Fluffy"
        };

        // Act
        medication.Pet = pet;

        // Assert
        Assert.That(medication.Pet, Is.EqualTo(pet));
    }

    [Test]
    public void Dosage_CanBeNull()
    {
        // Arrange & Act
        var medication = new Medication { Dosage = null };

        // Assert
        Assert.That(medication.Dosage, Is.Null);
    }

    [Test]
    public void Frequency_CanBeNull()
    {
        // Arrange & Act
        var medication = new Medication { Frequency = null };

        // Assert
        Assert.That(medication.Frequency, Is.Null);
    }

    [Test]
    public void StartDate_CanBeNull()
    {
        // Arrange & Act
        var medication = new Medication { StartDate = null };

        // Assert
        Assert.That(medication.StartDate, Is.Null);
    }

    [Test]
    public void EndDate_CanBeNull()
    {
        // Arrange & Act
        var medication = new Medication { EndDate = null };

        // Assert
        Assert.That(medication.EndDate, Is.Null);
    }

    [Test]
    public void Name_CanBeSet()
    {
        // Arrange & Act
        var medication = new Medication { Name = "Pain Relief" };

        // Assert
        Assert.That(medication.Name, Is.EqualTo("Pain Relief"));
    }
}
