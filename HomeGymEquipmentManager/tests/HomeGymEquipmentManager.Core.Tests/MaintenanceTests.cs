// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class MaintenanceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMaintenance()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var maintenanceDate = DateTime.UtcNow;
        var description = "Oil change and belt adjustment";
        var cost = 75.50m;
        var nextMaintenanceDate = DateTime.UtcNow.AddMonths(3);
        var notes = "Everything running smoothly";

        // Act
        var maintenance = new Maintenance
        {
            MaintenanceId = maintenanceId,
            UserId = userId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Description = description,
            Cost = cost,
            NextMaintenanceDate = nextMaintenanceDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(maintenance.MaintenanceId, Is.EqualTo(maintenanceId));
            Assert.That(maintenance.UserId, Is.EqualTo(userId));
            Assert.That(maintenance.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(maintenance.MaintenanceDate, Is.EqualTo(maintenanceDate));
            Assert.That(maintenance.Description, Is.EqualTo(description));
            Assert.That(maintenance.Cost, Is.EqualTo(cost));
            Assert.That(maintenance.NextMaintenanceDate, Is.EqualTo(nextMaintenanceDate));
            Assert.That(maintenance.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Maintenance_DefaultValues_AreSetCorrectly()
    {
        // Act
        var maintenance = new Maintenance();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(maintenance.Description, Is.EqualTo(string.Empty));
            Assert.That(maintenance.Cost, Is.Null);
            Assert.That(maintenance.NextMaintenanceDate, Is.Null);
            Assert.That(maintenance.Notes, Is.Null);
            Assert.That(maintenance.Equipment, Is.Null);
            Assert.That(maintenance.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsDueSoon_WithNextMaintenanceInFiveDays_ReturnsTrue()
    {
        // Arrange
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Regular maintenance",
            NextMaintenanceDate = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var isDueSoon = maintenance.IsDueSoon();

        // Assert
        Assert.That(isDueSoon, Is.True);
    }

    [Test]
    public void IsDueSoon_WithNextMaintenanceInSevenDays_ReturnsTrue()
    {
        // Arrange
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Regular maintenance",
            NextMaintenanceDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        var isDueSoon = maintenance.IsDueSoon();

        // Assert
        Assert.That(isDueSoon, Is.True);
    }

    [Test]
    public void IsDueSoon_WithNextMaintenanceInEightDays_ReturnsFalse()
    {
        // Arrange
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Regular maintenance",
            NextMaintenanceDate = DateTime.UtcNow.AddDays(8)
        };

        // Act
        var isDueSoon = maintenance.IsDueSoon();

        // Assert
        Assert.That(isDueSoon, Is.False);
    }

    [Test]
    public void IsDueSoon_WithOverdueNextMaintenance_ReturnsTrue()
    {
        // Arrange
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Overdue maintenance",
            NextMaintenanceDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var isDueSoon = maintenance.IsDueSoon();

        // Assert
        Assert.That(isDueSoon, Is.True);
    }

    [Test]
    public void IsDueSoon_WithoutNextMaintenanceDate_ReturnsFalse()
    {
        // Arrange
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Maintenance without next date",
            NextMaintenanceDate = null
        };

        // Act
        var isDueSoon = maintenance.IsDueSoon();

        // Assert
        Assert.That(isDueSoon, Is.False);
    }

    [Test]
    public void Maintenance_MaintenanceDate_DefaultsToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Test maintenance"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(maintenance.MaintenanceDate, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Maintenance_WithoutCost_IsValid()
    {
        // Arrange & Act
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Free maintenance"
        };

        // Assert
        Assert.That(maintenance.Cost, Is.Null);
    }

    [Test]
    public void Maintenance_WithCost_IsValid()
    {
        // Arrange
        var cost = 125.75m;

        // Act
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Paid maintenance",
            Cost = cost
        };

        // Assert
        Assert.That(maintenance.Cost, Is.EqualTo(cost));
    }

    [Test]
    public void Maintenance_WithoutNotes_IsValid()
    {
        // Arrange & Act
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Simple maintenance"
        };

        // Assert
        Assert.That(maintenance.Notes, Is.Null);
    }

    [Test]
    public void Maintenance_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            Description = "Test"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(maintenance.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Maintenance_AllProperties_CanBeSet()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var maintenanceDate = new DateTime(2024, 1, 15);
        var description = "Complete service";
        var cost = 200m;
        var nextMaintenanceDate = new DateTime(2024, 4, 15);
        var notes = "Replaced parts";
        var createdAt = DateTime.UtcNow.AddDays(-10);

        // Act
        var maintenance = new Maintenance
        {
            MaintenanceId = maintenanceId,
            UserId = userId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Description = description,
            Cost = cost,
            NextMaintenanceDate = nextMaintenanceDate,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(maintenance.MaintenanceId, Is.EqualTo(maintenanceId));
            Assert.That(maintenance.UserId, Is.EqualTo(userId));
            Assert.That(maintenance.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(maintenance.MaintenanceDate, Is.EqualTo(maintenanceDate));
            Assert.That(maintenance.Description, Is.EqualTo(description));
            Assert.That(maintenance.Cost, Is.EqualTo(cost));
            Assert.That(maintenance.NextMaintenanceDate, Is.EqualTo(nextMaintenanceDate));
            Assert.That(maintenance.Notes, Is.EqualTo(notes));
            Assert.That(maintenance.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
