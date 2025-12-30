// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core.Tests;

public class MedicationTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMedication()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Aspirin";
        var medicationType = MedicationType.Tablet;
        var dosage = "500mg";
        var prescribingDoctor = "Dr. Smith";
        var prescriptionDate = DateTime.UtcNow.AddMonths(-1);
        var purpose = "Pain relief";
        var instructions = "Take with food";
        var sideEffects = "Nausea, headache";

        // Act
        var medication = new Medication
        {
            MedicationId = medicationId,
            UserId = userId,
            Name = name,
            MedicationType = medicationType,
            Dosage = dosage,
            PrescribingDoctor = prescribingDoctor,
            PrescriptionDate = prescriptionDate,
            Purpose = purpose,
            Instructions = instructions,
            SideEffects = sideEffects
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.MedicationId, Is.EqualTo(medicationId));
            Assert.That(medication.UserId, Is.EqualTo(userId));
            Assert.That(medication.Name, Is.EqualTo(name));
            Assert.That(medication.MedicationType, Is.EqualTo(medicationType));
            Assert.That(medication.Dosage, Is.EqualTo(dosage));
            Assert.That(medication.PrescribingDoctor, Is.EqualTo(prescribingDoctor));
            Assert.That(medication.PrescriptionDate, Is.EqualTo(prescriptionDate));
            Assert.That(medication.Purpose, Is.EqualTo(purpose));
            Assert.That(medication.Instructions, Is.EqualTo(instructions));
            Assert.That(medication.SideEffects, Is.EqualTo(sideEffects));
            Assert.That(medication.IsActive, Is.True);
            Assert.That(medication.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var medication = new Medication();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.Name, Is.EqualTo(string.Empty));
            Assert.That(medication.Dosage, Is.EqualTo(string.Empty));
            Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Tablet));
            Assert.That(medication.PrescribingDoctor, Is.Null);
            Assert.That(medication.PrescriptionDate, Is.Null);
            Assert.That(medication.Purpose, Is.Null);
            Assert.That(medication.Instructions, Is.Null);
            Assert.That(medication.SideEffects, Is.Null);
            Assert.That(medication.IsActive, Is.True);
            Assert.That(medication.DoseSchedules, Is.Not.Null);
            Assert.That(medication.DoseSchedules.Count, Is.EqualTo(0));
            Assert.That(medication.Refills, Is.Not.Null);
            Assert.That(medication.Refills.Count, Is.EqualTo(0));
            Assert.That(medication.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void RequiresRefrigeration_InjectionType_ReturnsTrue()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Insulin",
            MedicationType = MedicationType.Injection,
            Dosage = "10 units"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequiresRefrigeration_LiquidType_ReturnsTrue()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Liquid Medicine",
            MedicationType = MedicationType.Liquid,
            Dosage = "5ml"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequiresRefrigeration_TabletType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Aspirin",
            MedicationType = MedicationType.Tablet,
            Dosage = "500mg"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresRefrigeration_CapsuleType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Vitamin D",
            MedicationType = MedicationType.Capsule,
            Dosage = "1000 IU"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresRefrigeration_TopicalType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Hydrocortisone Cream",
            MedicationType = MedicationType.Topical,
            Dosage = "1%"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresRefrigeration_InhalerType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Albuterol",
            MedicationType = MedicationType.Inhaler,
            Dosage = "90 mcg"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresRefrigeration_PatchType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Nicotine Patch",
            MedicationType = MedicationType.Patch,
            Dosage = "21mg"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresRefrigeration_OtherType_ReturnsFalse()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Custom Medicine",
            MedicationType = MedicationType.Other,
            Dosage = "As directed"
        };

        // Act
        var result = medication.RequiresRefrigeration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ToggleActive_WhenActive_SetsToInactive()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet,
            IsActive = true
        };

        // Act
        medication.ToggleActive();

        // Assert
        Assert.That(medication.IsActive, Is.False);
    }

    [Test]
    public void ToggleActive_WhenInactive_SetsToActive()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet,
            IsActive = false
        };

        // Act
        medication.ToggleActive();

        // Assert
        Assert.That(medication.IsActive, Is.True);
    }

    [Test]
    public void ToggleActive_CalledTwice_ReturnsToOriginalState()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet,
            IsActive = true
        };

        // Act
        medication.ToggleActive();
        medication.ToggleActive();

        // Assert
        Assert.That(medication.IsActive, Is.True);
    }

    [Test]
    public void DoseSchedules_CanAddSchedules_ToCollection()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet
        };

        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            ScheduledTime = new TimeSpan(8, 0, 0)
        };

        // Act
        medication.DoseSchedules.Add(doseSchedule);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.DoseSchedules.Count, Is.EqualTo(1));
            Assert.That(medication.DoseSchedules.First().DoseScheduleId, Is.EqualTo(doseSchedule.DoseScheduleId));
        });
    }

    [Test]
    public void Refills_CanAddRefills_ToCollection()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet
        };

        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            RefillDate = DateTime.UtcNow,
            Quantity = 30
        };

        // Act
        medication.Refills.Add(refill);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(medication.Refills.Count, Is.EqualTo(1));
            Assert.That(medication.Refills.First().RefillId, Is.EqualTo(refill.RefillId));
        });
    }

    [Test]
    public void MedicationType_CanBeSetToAllTypes()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine"
        };

        // Act & Assert - Test all enum values
        medication.MedicationType = MedicationType.Tablet;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Tablet));

        medication.MedicationType = MedicationType.Capsule;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Capsule));

        medication.MedicationType = MedicationType.Liquid;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Liquid));

        medication.MedicationType = MedicationType.Injection;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Injection));

        medication.MedicationType = MedicationType.Topical;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Topical));

        medication.MedicationType = MedicationType.Inhaler;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Inhaler));

        medication.MedicationType = MedicationType.Patch;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Patch));

        medication.MedicationType = MedicationType.Other;
        Assert.That(medication.MedicationType, Is.EqualTo(MedicationType.Other));
    }
}
