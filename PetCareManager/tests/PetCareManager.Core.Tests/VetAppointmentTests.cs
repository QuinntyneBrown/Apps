// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class VetAppointmentTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesVetAppointment()
    {
        // Arrange & Act
        var appointment = new VetAppointment();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appointment.VetAppointmentId, Is.EqualTo(Guid.Empty));
            Assert.That(appointment.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(appointment.Pet, Is.Null);
            Assert.That(appointment.AppointmentDate, Is.EqualTo(default(DateTime)));
            Assert.That(appointment.VetName, Is.Null);
            Assert.That(appointment.Reason, Is.Null);
            Assert.That(appointment.Notes, Is.Null);
            Assert.That(appointment.Cost, Is.Null);
            Assert.That(appointment.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(7);

        // Act
        var appointment = new VetAppointment
        {
            VetAppointmentId = appointmentId,
            PetId = petId,
            AppointmentDate = appointmentDate,
            VetName = "Dr. Smith",
            Reason = "Annual checkup",
            Notes = "Pet is healthy",
            Cost = 75.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appointment.VetAppointmentId, Is.EqualTo(appointmentId));
            Assert.That(appointment.PetId, Is.EqualTo(petId));
            Assert.That(appointment.AppointmentDate, Is.EqualTo(appointmentDate));
            Assert.That(appointment.VetName, Is.EqualTo("Dr. Smith"));
            Assert.That(appointment.Reason, Is.EqualTo("Annual checkup"));
            Assert.That(appointment.Notes, Is.EqualTo("Pet is healthy"));
            Assert.That(appointment.Cost, Is.EqualTo(75.00m));
        });
    }

    [Test]
    public void Pet_NavigationProperty_CanBeSet()
    {
        // Arrange
        var appointment = new VetAppointment();
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            Name = "Max"
        };

        // Act
        appointment.Pet = pet;

        // Assert
        Assert.That(appointment.Pet, Is.EqualTo(pet));
    }

    [Test]
    public void VetName_CanBeNull()
    {
        // Arrange & Act
        var appointment = new VetAppointment { VetName = null };

        // Assert
        Assert.That(appointment.VetName, Is.Null);
    }

    [Test]
    public void Reason_CanBeNull()
    {
        // Arrange & Act
        var appointment = new VetAppointment { Reason = null };

        // Assert
        Assert.That(appointment.Reason, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var appointment = new VetAppointment { Notes = null };

        // Assert
        Assert.That(appointment.Notes, Is.Null);
    }

    [Test]
    public void Cost_CanBeNull()
    {
        // Arrange & Act
        var appointment = new VetAppointment { Cost = null };

        // Assert
        Assert.That(appointment.Cost, Is.Null);
    }

    [Test]
    public void Cost_AcceptsDecimalValues()
    {
        // Arrange & Act
        var appointment = new VetAppointment { Cost = 125.50m };

        // Assert
        Assert.That(appointment.Cost, Is.EqualTo(125.50m));
    }
}
