// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class EquipmentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEquipment()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Treadmill";
        var equipmentType = EquipmentType.Cardio;
        var brand = "NordicTrack";
        var model = "T 6.5 S";
        var purchaseDate = DateTime.UtcNow.AddYears(-1);
        var purchasePrice = 899.99m;
        var location = "Home Gym";
        var notes = "Great condition";

        // Act
        var equipment = new Equipment
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType,
            Brand = brand,
            Model = model,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            Location = location,
            Notes = notes,
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipment.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(equipment.UserId, Is.EqualTo(userId));
            Assert.That(equipment.Name, Is.EqualTo(name));
            Assert.That(equipment.EquipmentType, Is.EqualTo(equipmentType));
            Assert.That(equipment.Brand, Is.EqualTo(brand));
            Assert.That(equipment.Model, Is.EqualTo(model));
            Assert.That(equipment.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(equipment.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(equipment.Location, Is.EqualTo(location));
            Assert.That(equipment.Notes, Is.EqualTo(notes));
            Assert.That(equipment.IsActive, Is.True);
        });
    }

    [Test]
    public void Equipment_DefaultValues_AreSetCorrectly()
    {
        // Act
        var equipment = new Equipment();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipment.Name, Is.EqualTo(string.Empty));
            Assert.That(equipment.EquipmentType, Is.EqualTo(EquipmentType.Cardio));
            Assert.That(equipment.Brand, Is.Null);
            Assert.That(equipment.Model, Is.Null);
            Assert.That(equipment.PurchaseDate, Is.Null);
            Assert.That(equipment.PurchasePrice, Is.Null);
            Assert.That(equipment.Location, Is.Null);
            Assert.That(equipment.Notes, Is.Null);
            Assert.That(equipment.IsActive, Is.True);
            Assert.That(equipment.MaintenanceRecords, Is.Not.Null);
        });
    }

    [Test]
    public void RequiresMaintenance_CardioEquipment_ReturnsTrue()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Treadmill",
            EquipmentType = EquipmentType.Cardio
        };

        // Act
        var requiresMaintenance = equipment.RequiresMaintenance();

        // Assert
        Assert.That(requiresMaintenance, Is.True);
    }

    [Test]
    public void RequiresMaintenance_StrengthEquipment_ReturnsTrue()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Cable Machine",
            EquipmentType = EquipmentType.Strength
        };

        // Act
        var requiresMaintenance = equipment.RequiresMaintenance();

        // Assert
        Assert.That(requiresMaintenance, Is.True);
    }

    [Test]
    public void RequiresMaintenance_FlexibilityEquipment_ReturnsFalse()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Yoga Mat",
            EquipmentType = EquipmentType.Flexibility
        };

        // Act
        var requiresMaintenance = equipment.RequiresMaintenance();

        // Assert
        Assert.That(requiresMaintenance, Is.False);
    }

    [Test]
    public void RequiresMaintenance_AccessoryEquipment_ReturnsFalse()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Resistance Bands",
            EquipmentType = EquipmentType.Accessory
        };

        // Act
        var requiresMaintenance = equipment.RequiresMaintenance();

        // Assert
        Assert.That(requiresMaintenance, Is.False);
    }

    [Test]
    public void Equipment_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Dumbbells",
            EquipmentType = EquipmentType.Strength
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipment.Brand, Is.Null);
            Assert.That(equipment.Model, Is.Null);
            Assert.That(equipment.PurchaseDate, Is.Null);
            Assert.That(equipment.PurchasePrice, Is.Null);
            Assert.That(equipment.Location, Is.Null);
            Assert.That(equipment.Notes, Is.Null);
        });
    }

    [Test]
    public void Equipment_MaintenanceRecords_InitializesAsEmptyList()
    {
        // Arrange & Act
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Elliptical",
            EquipmentType = EquipmentType.Cardio
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipment.MaintenanceRecords, Is.Not.Null);
            Assert.That(equipment.MaintenanceRecords, Is.Empty);
        });
    }

    [Test]
    public void Equipment_IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Stationary Bike",
            EquipmentType = EquipmentType.Cardio
        };

        // Assert
        Assert.That(equipment.IsActive, Is.True);
    }

    [Test]
    public void Equipment_CanSetIsActiveToFalse()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Old Equipment",
            EquipmentType = EquipmentType.Strength,
            IsActive = false
        };

        // Assert
        Assert.That(equipment.IsActive, Is.False);
    }

    [Test]
    public void Equipment_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Equipment",
            EquipmentType = EquipmentType.Cardio
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(equipment.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Equipment_WithZeroPurchasePrice_IsValid()
    {
        // Arrange & Act
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Free Equipment",
            EquipmentType = EquipmentType.Accessory,
            PurchasePrice = 0m
        };

        // Assert
        Assert.That(equipment.PurchasePrice, Is.EqualTo(0m));
    }

    [Test]
    public void Equipment_AllEquipmentTypes_CanBeAssigned()
    {
        // Arrange
        var equipmentTypes = new[]
        {
            EquipmentType.Cardio,
            EquipmentType.Strength,
            EquipmentType.Flexibility,
            EquipmentType.Accessory
        };

        // Act & Assert
        foreach (var type in equipmentTypes)
        {
            var equipment = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = $"{type} Equipment",
                EquipmentType = type
            };

            Assert.That(equipment.EquipmentType, Is.EqualTo(type));
        }
    }

    [Test]
    public void Equipment_AllProperties_CanBeSet()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Complete Equipment";
        var equipmentType = EquipmentType.Cardio;
        var brand = "Brand";
        var model = "Model";
        var purchaseDate = DateTime.UtcNow.AddYears(-2);
        var purchasePrice = 1500m;
        var location = "Basement";
        var notes = "Well maintained";
        var isActive = true;
        var createdAt = DateTime.UtcNow.AddDays(-30);

        // Act
        var equipment = new Equipment
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType,
            Brand = brand,
            Model = model,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            Location = location,
            Notes = notes,
            IsActive = isActive,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipment.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(equipment.UserId, Is.EqualTo(userId));
            Assert.That(equipment.Name, Is.EqualTo(name));
            Assert.That(equipment.EquipmentType, Is.EqualTo(equipmentType));
            Assert.That(equipment.Brand, Is.EqualTo(brand));
            Assert.That(equipment.Model, Is.EqualTo(model));
            Assert.That(equipment.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(equipment.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(equipment.Location, Is.EqualTo(location));
            Assert.That(equipment.Notes, Is.EqualTo(notes));
            Assert.That(equipment.IsActive, Is.EqualTo(isActive));
            Assert.That(equipment.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
