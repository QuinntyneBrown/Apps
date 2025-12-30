using PetCareManager.Api.Features.Pets;
using PetCareManager.Api.Features.Medications;
using PetCareManager.Api.Features.VetAppointments;
using PetCareManager.Api.Features.Vaccinations;
using PetCareManager.Core;

namespace PetCareManager.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void PetDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Max",
            PetType = PetType.Dog,
            Breed = "Golden Retriever",
            DateOfBirth = new DateTime(2020, 1, 1),
            Color = "Golden",
            Weight = 30.5m,
            MicrochipNumber = "123456789",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = pet.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PetId, Is.EqualTo(pet.PetId));
            Assert.That(dto.UserId, Is.EqualTo(pet.UserId));
            Assert.That(dto.Name, Is.EqualTo(pet.Name));
            Assert.That(dto.PetType, Is.EqualTo(pet.PetType));
            Assert.That(dto.Breed, Is.EqualTo(pet.Breed));
            Assert.That(dto.DateOfBirth, Is.EqualTo(pet.DateOfBirth));
            Assert.That(dto.Color, Is.EqualTo(pet.Color));
            Assert.That(dto.Weight, Is.EqualTo(pet.Weight));
            Assert.That(dto.MicrochipNumber, Is.EqualTo(pet.MicrochipNumber));
            Assert.That(dto.CreatedAt, Is.EqualTo(pet.CreatedAt));
        });
    }

    [Test]
    public void MedicationDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "Heartgard",
            Dosage = "10mg",
            Frequency = "Monthly",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(6),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = medication.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MedicationId, Is.EqualTo(medication.MedicationId));
            Assert.That(dto.PetId, Is.EqualTo(medication.PetId));
            Assert.That(dto.Name, Is.EqualTo(medication.Name));
            Assert.That(dto.Dosage, Is.EqualTo(medication.Dosage));
            Assert.That(dto.Frequency, Is.EqualTo(medication.Frequency));
            Assert.That(dto.StartDate, Is.EqualTo(medication.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(medication.EndDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(medication.CreatedAt));
        });
    }

    [Test]
    public void VetAppointmentDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var appointment = new VetAppointment
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            AppointmentDate = DateTime.UtcNow,
            VetName = "Dr. Smith",
            Reason = "Annual checkup",
            Notes = "Pet is healthy",
            Cost = 75.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = appointment.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.VetAppointmentId, Is.EqualTo(appointment.VetAppointmentId));
            Assert.That(dto.PetId, Is.EqualTo(appointment.PetId));
            Assert.That(dto.AppointmentDate, Is.EqualTo(appointment.AppointmentDate));
            Assert.That(dto.VetName, Is.EqualTo(appointment.VetName));
            Assert.That(dto.Reason, Is.EqualTo(appointment.Reason));
            Assert.That(dto.Notes, Is.EqualTo(appointment.Notes));
            Assert.That(dto.Cost, Is.EqualTo(appointment.Cost));
            Assert.That(dto.CreatedAt, Is.EqualTo(appointment.CreatedAt));
        });
    }

    [Test]
    public void VaccinationDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var vaccination = new Vaccination
        {
            VaccinationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "Rabies",
            DateAdministered = DateTime.UtcNow,
            NextDueDate = DateTime.UtcNow.AddYears(1),
            VetName = "Dr. Johnson",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = vaccination.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.VaccinationId, Is.EqualTo(vaccination.VaccinationId));
            Assert.That(dto.PetId, Is.EqualTo(vaccination.PetId));
            Assert.That(dto.Name, Is.EqualTo(vaccination.Name));
            Assert.That(dto.DateAdministered, Is.EqualTo(vaccination.DateAdministered));
            Assert.That(dto.NextDueDate, Is.EqualTo(vaccination.NextDueDate));
            Assert.That(dto.VetName, Is.EqualTo(vaccination.VetName));
            Assert.That(dto.CreatedAt, Is.EqualTo(vaccination.CreatedAt));
        });
    }
}
