// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class PetTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesPet()
    {
        // Arrange & Act
        var pet = new Pet();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pet.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(pet.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(pet.Name, Is.EqualTo(string.Empty));
            Assert.That(pet.PetType, Is.EqualTo(PetType.Dog));
            Assert.That(pet.Breed, Is.Null);
            Assert.That(pet.DateOfBirth, Is.Null);
            Assert.That(pet.Color, Is.Null);
            Assert.That(pet.Weight, Is.Null);
            Assert.That(pet.MicrochipNumber, Is.Null);
            Assert.That(pet.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(pet.VetAppointments, Is.Not.Null);
            Assert.That(pet.VetAppointments, Is.Empty);
            Assert.That(pet.Medications, Is.Not.Null);
            Assert.That(pet.Medications, Is.Empty);
            Assert.That(pet.Vaccinations, Is.Not.Null);
            Assert.That(pet.Vaccinations, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dateOfBirth = DateTime.UtcNow.AddYears(-2);

        // Act
        var pet = new Pet
        {
            PetId = petId,
            UserId = userId,
            Name = "Max",
            PetType = PetType.Dog,
            Breed = "Golden Retriever",
            DateOfBirth = dateOfBirth,
            Color = "Golden",
            Weight = 30.5m,
            MicrochipNumber = "123456789"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pet.PetId, Is.EqualTo(petId));
            Assert.That(pet.UserId, Is.EqualTo(userId));
            Assert.That(pet.Name, Is.EqualTo("Max"));
            Assert.That(pet.PetType, Is.EqualTo(PetType.Dog));
            Assert.That(pet.Breed, Is.EqualTo("Golden Retriever"));
            Assert.That(pet.DateOfBirth, Is.EqualTo(dateOfBirth));
            Assert.That(pet.Color, Is.EqualTo("Golden"));
            Assert.That(pet.Weight, Is.EqualTo(30.5m));
            Assert.That(pet.MicrochipNumber, Is.EqualTo("123456789"));
        });
    }

    [Test]
    public void PetType_CanBeSetToDog()
    {
        // Arrange & Act
        var pet = new Pet { PetType = PetType.Dog };

        // Assert
        Assert.That(pet.PetType, Is.EqualTo(PetType.Dog));
    }

    [Test]
    public void PetType_CanBeSetToCat()
    {
        // Arrange & Act
        var pet = new Pet { PetType = PetType.Cat };

        // Assert
        Assert.That(pet.PetType, Is.EqualTo(PetType.Cat));
    }

    [Test]
    public void PetType_CanBeSetToBird()
    {
        // Arrange & Act
        var pet = new Pet { PetType = PetType.Bird };

        // Assert
        Assert.That(pet.PetType, Is.EqualTo(PetType.Bird));
    }

    [Test]
    public void Weight_AcceptsDecimalValues()
    {
        // Arrange & Act
        var pet = new Pet { Weight = 12.75m };

        // Assert
        Assert.That(pet.Weight, Is.EqualTo(12.75m));
    }

    [Test]
    public void VetAppointments_Collection_CanBeModified()
    {
        // Arrange
        var pet = new Pet();
        var appointment = new VetAppointment
        {
            VetAppointmentId = Guid.NewGuid(),
            AppointmentDate = DateTime.UtcNow
        };

        // Act
        pet.VetAppointments.Add(appointment);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pet.VetAppointments.Count, Is.EqualTo(1));
            Assert.That(pet.VetAppointments.First(), Is.EqualTo(appointment));
        });
    }

    [Test]
    public void Medications_Collection_CanBeModified()
    {
        // Arrange
        var pet = new Pet();
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            Name = "Test Medication"
        };

        // Act
        pet.Medications.Add(medication);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pet.Medications.Count, Is.EqualTo(1));
            Assert.That(pet.Medications.First(), Is.EqualTo(medication));
        });
    }

    [Test]
    public void Vaccinations_Collection_CanBeModified()
    {
        // Arrange
        var pet = new Pet();
        var vaccination = new Vaccination
        {
            VaccinationId = Guid.NewGuid(),
            Name = "Rabies"
        };

        // Act
        pet.Vaccinations.Add(vaccination);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pet.Vaccinations.Count, Is.EqualTo(1));
            Assert.That(pet.Vaccinations.First(), Is.EqualTo(vaccination));
        });
    }

    [Test]
    public void DateOfBirth_CanBeNull()
    {
        // Arrange & Act
        var pet = new Pet { DateOfBirth = null };

        // Assert
        Assert.That(pet.DateOfBirth, Is.Null);
    }

    [Test]
    public void Breed_CanBeNull()
    {
        // Arrange & Act
        var pet = new Pet { Breed = null };

        // Assert
        Assert.That(pet.Breed, Is.Null);
    }

    [Test]
    public void MicrochipNumber_CanBeSet()
    {
        // Arrange & Act
        var pet = new Pet { MicrochipNumber = "ABC123XYZ" };

        // Assert
        Assert.That(pet.MicrochipNumber, Is.EqualTo("ABC123XYZ"));
    }
}
