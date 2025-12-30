// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core.Tests;

public class EventTests
{
    [Test]
    public void VehicleRegisteredEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var make = "Toyota";
        var model = "Camry";
        var year = 2020;
        var vin = "1HGBH41JXMN109186";
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new VehicleRegisteredEvent
        {
            VehicleId = vehicleId,
            Make = make,
            Model = model,
            Year = year,
            VIN = vin,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.Make, Is.EqualTo(make));
            Assert.That(eventData.Model, Is.EqualTo(model));
            Assert.That(eventData.Year, Is.EqualTo(year));
            Assert.That(eventData.VIN, Is.EqualTo(vin));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void VehicleMileageUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var previousMileage = 30000m;
        var newMileage = 35000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new VehicleMileageUpdatedEvent
        {
            VehicleId = vehicleId,
            PreviousMileage = previousMileage,
            NewMileage = newMileage,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.PreviousMileage, Is.EqualTo(previousMileage));
            Assert.That(eventData.NewMileage, Is.EqualTo(newMileage));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ServiceRecordCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var serviceType = ServiceType.OilChange;
        var serviceDate = new DateTime(2024, 3, 15);
        var mileageAtService = 30000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new ServiceRecordCreatedEvent
        {
            ServiceRecordId = serviceRecordId,
            VehicleId = vehicleId,
            ServiceType = serviceType,
            ServiceDate = serviceDate,
            MileageAtService = mileageAtService,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ServiceRecordId, Is.EqualTo(serviceRecordId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.ServiceType, Is.EqualTo(serviceType));
            Assert.That(eventData.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(eventData.MileageAtService, Is.EqualTo(mileageAtService));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ServiceCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var cost = 45.99m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new ServiceCompletedEvent
        {
            ServiceRecordId = serviceRecordId,
            VehicleId = vehicleId,
            Cost = cost,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ServiceRecordId, Is.EqualTo(serviceRecordId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.Cost, Is.EqualTo(cost));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MaintenanceScheduleCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var serviceType = ServiceType.OilChange;
        var mileageInterval = 5000m;
        var monthsInterval = 6;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new MaintenanceScheduleCreatedEvent
        {
            MaintenanceScheduleId = scheduleId,
            VehicleId = vehicleId,
            ServiceType = serviceType,
            MileageInterval = mileageInterval,
            MonthsInterval = monthsInterval,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.MaintenanceScheduleId, Is.EqualTo(scheduleId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.ServiceType, Is.EqualTo(serviceType));
            Assert.That(eventData.MileageInterval, Is.EqualTo(mileageInterval));
            Assert.That(eventData.MonthsInterval, Is.EqualTo(monthsInterval));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MaintenanceDueEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var serviceType = ServiceType.OilChange;
        var currentMileage = 35000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new MaintenanceDueEvent
        {
            MaintenanceScheduleId = scheduleId,
            VehicleId = vehicleId,
            ServiceType = serviceType,
            CurrentMileage = currentMileage,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.MaintenanceScheduleId, Is.EqualTo(scheduleId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.ServiceType, Is.EqualTo(serviceType));
            Assert.That(eventData.CurrentMileage, Is.EqualTo(currentMileage));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Events_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var event1 = new VehicleRegisteredEvent { VehicleId = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Year = 2020 };
        var event2 = new ServiceRecordCreatedEvent { ServiceRecordId = Guid.NewGuid(), VehicleId = Guid.NewGuid(), ServiceType = ServiceType.OilChange, ServiceDate = DateTime.UtcNow, MileageAtService = 30000m };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(event1.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event2.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Events_AreRecords_SupportValueEquality()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new VehicleRegisteredEvent
        {
            VehicleId = vehicleId,
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            Timestamp = timestamp
        };

        var event2 = new VehicleRegisteredEvent
        {
            VehicleId = vehicleId,
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
