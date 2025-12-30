// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core.Tests;

public class RefillTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRefill()
    {
        // Arrange
        var refillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var medicationId = Guid.NewGuid();
        var refillDate = DateTime.UtcNow;
        var quantity = 30;
        var pharmacyName = "CVS Pharmacy";
        var cost = 25.50m;
        var nextRefillDate = DateTime.UtcNow.AddDays(30);
        var refillsRemaining = 5;
        var notes = "Insurance covered";

        // Act
        var refill = new Refill
        {
            RefillId = refillId,
            UserId = userId,
            MedicationId = medicationId,
            RefillDate = refillDate,
            Quantity = quantity,
            PharmacyName = pharmacyName,
            Cost = cost,
            NextRefillDate = nextRefillDate,
            RefillsRemaining = refillsRemaining,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(refill.RefillId, Is.EqualTo(refillId));
            Assert.That(refill.UserId, Is.EqualTo(userId));
            Assert.That(refill.MedicationId, Is.EqualTo(medicationId));
            Assert.That(refill.RefillDate, Is.EqualTo(refillDate));
            Assert.That(refill.Quantity, Is.EqualTo(quantity));
            Assert.That(refill.PharmacyName, Is.EqualTo(pharmacyName));
            Assert.That(refill.Cost, Is.EqualTo(cost));
            Assert.That(refill.NextRefillDate, Is.EqualTo(nextRefillDate));
            Assert.That(refill.RefillsRemaining, Is.EqualTo(refillsRemaining));
            Assert.That(refill.Notes, Is.EqualTo(notes));
            Assert.That(refill.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var refill = new Refill();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(refill.Quantity, Is.EqualTo(0));
            Assert.That(refill.PharmacyName, Is.Null);
            Assert.That(refill.Cost, Is.Null);
            Assert.That(refill.NextRefillDate, Is.Null);
            Assert.That(refill.RefillsRemaining, Is.Null);
            Assert.That(refill.Notes, Is.Null);
            Assert.That(refill.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsRefillDueSoon_WithinSevenDays_ReturnsTrue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            NextRefillDate = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRefillDueSoon_ExactlySevenDays_ReturnsTrue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            NextRefillDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRefillDueSoon_MoreThanSevenDays_ReturnsFalse()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            NextRefillDate = DateTime.UtcNow.AddDays(10)
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRefillDueSoon_Overdue_ReturnsTrue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow.AddDays(-30),
            Quantity = 30,
            NextRefillDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRefillDueSoon_NoNextRefillDate_ReturnsFalse()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            NextRefillDate = null
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRefillDueSoon_DueToday_ReturnsTrue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow.AddDays(-30),
            Quantity = 30,
            NextRefillDate = DateTime.UtcNow
        };

        // Act
        var result = refill.IsRefillDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void NoRefillsRemaining_WhenZero_ReturnsTrue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            RefillsRemaining = 0
        };

        // Act
        var result = refill.NoRefillsRemaining();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void NoRefillsRemaining_WhenPositive_ReturnsFalse()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            RefillsRemaining = 3
        };

        // Act
        var result = refill.NoRefillsRemaining();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void NoRefillsRemaining_WhenNull_ReturnsFalse()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            RefillsRemaining = null
        };

        // Act
        var result = refill.NoRefillsRemaining();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void NoRefillsRemaining_WhenOne_ReturnsFalse()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            RefillsRemaining = 1
        };

        // Act
        var result = refill.NoRefillsRemaining();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Medication_CanBeAssociated_WithRefill()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Aspirin",
            MedicationType = MedicationType.Tablet
        };

        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            Medication = medication
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(refill.MedicationId, Is.EqualTo(medication.MedicationId));
            Assert.That(refill.Medication, Is.Not.Null);
            Assert.That(refill.Medication.Name, Is.EqualTo("Aspirin"));
        });
    }

    [Test]
    public void Cost_CanBeSetToDecimalValue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30
        };

        // Act
        refill.Cost = 35.75m;

        // Assert
        Assert.That(refill.Cost, Is.EqualTo(35.75m));
    }

    [Test]
    public void Quantity_CanBeSetToPositiveValue()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow
        };

        // Act
        refill.Quantity = 90;

        // Assert
        Assert.That(refill.Quantity, Is.EqualTo(90));
    }

    [Test]
    public void PharmacyName_CanBeSetToDifferentPharmacies()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30
        };

        // Act & Assert - CVS
        refill.PharmacyName = "CVS Pharmacy";
        Assert.That(refill.PharmacyName, Is.EqualTo("CVS Pharmacy"));

        // Act & Assert - Walgreens
        refill.PharmacyName = "Walgreens";
        Assert.That(refill.PharmacyName, Is.EqualTo("Walgreens"));
    }
}
