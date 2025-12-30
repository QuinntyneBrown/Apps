// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core.Tests;

public class ServiceRecordTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesServiceRecord()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var serviceType = ServiceType.OilChange;
        var serviceDate = new DateTime(2024, 3, 15);
        var mileageAtService = 30000m;
        var cost = 45.99m;
        var serviceProvider = "Quick Lube";
        var description = "Oil change and filter replacement";
        var notes = "Used synthetic oil";
        var invoiceNumber = "INV-12345";

        // Act
        var record = new ServiceRecord
        {
            ServiceRecordId = serviceRecordId,
            VehicleId = vehicleId,
            ServiceType = serviceType,
            ServiceDate = serviceDate,
            MileageAtService = mileageAtService,
            Cost = cost,
            ServiceProvider = serviceProvider,
            Description = description,
            Notes = notes,
            InvoiceNumber = invoiceNumber
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(record.ServiceRecordId, Is.EqualTo(serviceRecordId));
            Assert.That(record.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(record.ServiceType, Is.EqualTo(serviceType));
            Assert.That(record.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(record.MileageAtService, Is.EqualTo(mileageAtService));
            Assert.That(record.Cost, Is.EqualTo(cost));
            Assert.That(record.ServiceProvider, Is.EqualTo(serviceProvider));
            Assert.That(record.Description, Is.EqualTo(description));
            Assert.That(record.Notes, Is.EqualTo(notes));
            Assert.That(record.InvoiceNumber, Is.EqualTo(invoiceNumber));
        });
    }

    [Test]
    public void AddParts_AddsParts ToList()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.BrakeService,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 40000m,
            Cost = 300m,
            Description = "Brake replacement"
        };

        var parts = new[] { "Brake pads", "Brake rotors", "Brake fluid" };

        // Act
        record.AddParts(parts);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(record.PartsReplaced, Has.Count.EqualTo(3));
            Assert.That(record.PartsReplaced, Contains.Item("Brake pads"));
            Assert.That(record.PartsReplaced, Contains.Item("Brake rotors"));
            Assert.That(record.PartsReplaced, Contains.Item("Brake fluid"));
        });
    }

    [Test]
    public void UpdateCost_ValidCost_UpdatesCost()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 30000m,
            Cost = 45.99m,
            Description = "Oil change"
        };

        // Act
        record.UpdateCost(52.50m);

        // Assert
        Assert.That(record.Cost, Is.EqualTo(52.50m));
    }

    [Test]
    public void UpdateCost_NegativeCost_ThrowsArgumentException()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 30000m,
            Cost = 45.99m,
            Description = "Oil change"
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => record.UpdateCost(-10m));
        Assert.That(ex.Message, Does.Contain("Cost cannot be negative"));
    }

    [Test]
    public void SetWarrantyExpiration_SetsWarrantyDate()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.BatteryReplacement,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 50000m,
            Cost = 150m,
            Description = "Battery replacement"
        };

        var expirationDate = DateTime.UtcNow.AddYears(2);

        // Act
        record.SetWarrantyExpiration(expirationDate);

        // Assert
        Assert.That(record.WarrantyExpirationDate, Is.EqualTo(expirationDate));
    }

    [Test]
    public void ServiceType_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.OilChange }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.TireService }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.BrakeService }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.EngineService }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.TransmissionService }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.BatteryReplacement }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.AirFilterReplacement }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.Inspection }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.Alignment }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.CoolantFlush }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.Diagnostic }, Throws.Nothing);
            Assert.That(() => new ServiceRecord { ServiceType = ServiceType.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void PartsReplaced_InitializesAsEmptyList()
    {
        // Arrange & Act
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 30000m,
            Cost = 45.99m,
            Description = "Oil change"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(record.PartsReplaced, Is.Not.Null);
            Assert.That(record.PartsReplaced, Is.Empty);
        });
    }

    [Test]
    public void UpdateCost_ZeroCost_IsValid()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.Inspection,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 20000m,
            Cost = 50m,
            Description = "Inspection"
        };

        // Act & Assert
        Assert.DoesNotThrow(() => record.UpdateCost(0m));
        Assert.That(record.Cost, Is.EqualTo(0m));
    }

    [Test]
    public void AddParts_MultipleCalls_AccumulatesParts()
    {
        // Arrange
        var record = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.EngineService,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 60000m,
            Cost = 800m,
            Description = "Engine service"
        };

        // Act
        record.AddParts(new[] { "Spark plugs" });
        record.AddParts(new[] { "Air filter", "Oil filter" });

        // Assert
        Assert.That(record.PartsReplaced, Has.Count.EqualTo(3));
    }
}
