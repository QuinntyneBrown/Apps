// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core.Tests;

public class VehicleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesVehicle()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var make = "Toyota";
        var model = "Camry";
        var year = 2020;
        var vin = "1HGBH41JXMN109186";
        var licensePlate = "ABC-1234";
        var vehicleType = VehicleType.Sedan;
        var currentMileage = 50000m;
        var purchaseDate = new DateTime(2020, 1, 15);
        var notes = "Well maintained";

        // Act
        var vehicle = new Vehicle
        {
            VehicleId = vehicleId,
            Make = make,
            Model = model,
            Year = year,
            VIN = vin,
            LicensePlate = licensePlate,
            VehicleType = vehicleType,
            CurrentMileage = currentMileage,
            PurchaseDate = purchaseDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(vehicle.Make, Is.EqualTo(make));
            Assert.That(vehicle.Model, Is.EqualTo(model));
            Assert.That(vehicle.Year, Is.EqualTo(year));
            Assert.That(vehicle.VIN, Is.EqualTo(vin));
            Assert.That(vehicle.LicensePlate, Is.EqualTo(licensePlate));
            Assert.That(vehicle.VehicleType, Is.EqualTo(vehicleType));
            Assert.That(vehicle.CurrentMileage, Is.EqualTo(currentMileage));
            Assert.That(vehicle.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(vehicle.Notes, Is.EqualTo(notes));
            Assert.That(vehicle.IsActive, Is.True);
        });
    }

    [Test]
    public void UpdateMileage_ValidMileage_UpdatesCurrentMileage()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 30000m
        };

        // Act
        vehicle.UpdateMileage(35000m);

        // Assert
        Assert.That(vehicle.CurrentMileage, Is.EqualTo(35000m));
    }

    [Test]
    public void UpdateMileage_LowerMileage_ThrowsArgumentException()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 30000m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => vehicle.UpdateMileage(25000m));
        Assert.That(ex.Message, Does.Contain("New mileage cannot be less than current mileage"));
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150",
            Year = 2018,
            VehicleType = VehicleType.Truck,
            CurrentMileage = 60000m,
            IsActive = true
        };

        // Act
        vehicle.Deactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_SetsIsActiveToTrue()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150",
            Year = 2018,
            VehicleType = VehicleType.Truck,
            CurrentMileage = 60000m,
            IsActive = false
        };

        // Act
        vehicle.Reactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void VehicleType_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Sedan }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.SUV }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Truck }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Motorcycle }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Van }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Coupe }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Convertible }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Hatchback }, Throws.Nothing);
            Assert.That(() => new Vehicle { VehicleType = VehicleType.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Nissan",
            Model = "Altima",
            Year = 2021,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 10000m
        };

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void ServiceRecords_InitializesAsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Mazda",
            Model = "CX-5",
            Year = 2020,
            VehicleType = VehicleType.SUV,
            CurrentMileage = 25000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.ServiceRecords, Is.Not.Null);
            Assert.That(vehicle.ServiceRecords, Is.Empty);
        });
    }

    [Test]
    public void MaintenanceSchedules_InitializesAsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Subaru",
            Model = "Outback",
            Year = 2019,
            VehicleType = VehicleType.SUV,
            CurrentMileage = 40000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.MaintenanceSchedules, Is.Not.Null);
            Assert.That(vehicle.MaintenanceSchedules, Is.Empty);
        });
    }

    [Test]
    public void UpdateMileage_SameMileage_DoesNotThrow()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Chevrolet",
            Model = "Malibu",
            Year = 2020,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 15000m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => vehicle.UpdateMileage(15000m));
        Assert.That(vehicle.CurrentMileage, Is.EqualTo(15000m));
    }

    [Test]
    public void Vehicle_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Hyundai",
            Model = "Elantra",
            Year = 2021,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 5000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VIN, Is.Null);
            Assert.That(vehicle.LicensePlate, Is.Null);
            Assert.That(vehicle.PurchaseDate, Is.Null);
            Assert.That(vehicle.Notes, Is.Null);
        });
    }
}
