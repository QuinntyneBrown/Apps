// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core.Tests;

public class MaintenanceScheduleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMaintenanceSchedule()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var serviceType = ServiceType.OilChange;
        var description = "Regular oil change";
        var mileageInterval = 5000m;
        var monthsInterval = 6;
        var notes = "Use synthetic oil";

        // Act
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = scheduleId,
            VehicleId = vehicleId,
            ServiceType = serviceType,
            Description = description,
            MileageInterval = mileageInterval,
            MonthsInterval = monthsInterval,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.MaintenanceScheduleId, Is.EqualTo(scheduleId));
            Assert.That(schedule.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(schedule.ServiceType, Is.EqualTo(serviceType));
            Assert.That(schedule.Description, Is.EqualTo(description));
            Assert.That(schedule.MileageInterval, Is.EqualTo(mileageInterval));
            Assert.That(schedule.MonthsInterval, Is.EqualTo(monthsInterval));
            Assert.That(schedule.Notes, Is.EqualTo(notes));
            Assert.That(schedule.IsActive, Is.True);
        });
    }

    [Test]
    public void RecordService_UpdatesLastServiceAndCalculatesNext()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            MonthsInterval = 6
        };

        var serviceMileage = 30000m;
        var serviceDate = new DateTime(2024, 3, 15);

        // Act
        schedule.RecordService(serviceMileage, serviceDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.LastServiceMileage, Is.EqualTo(30000m));
            Assert.That(schedule.LastServiceDate, Is.EqualTo(serviceDate));
            Assert.That(schedule.NextServiceMileage, Is.EqualTo(35000m));
            Assert.That(schedule.NextServiceDate, Is.EqualTo(serviceDate.AddMonths(6)));
        });
    }

    [Test]
    public void IsDue_ByMileage_ReturnsTrue()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            NextServiceMileage = 35000m,
            IsActive = true
        };

        // Act
        var isDue = schedule.IsDue(36000m, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.True);
    }

    [Test]
    public void IsDue_ByDate_ReturnsTrue()
    {
        // Arrange
        var nextServiceDate = DateTime.UtcNow.AddDays(-1);
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.Inspection,
            Description = "Annual inspection",
            MonthsInterval = 12,
            NextServiceDate = nextServiceDate,
            IsActive = true
        };

        // Act
        var isDue = schedule.IsDue(30000m, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.True);
    }

    [Test]
    public void IsDue_NotDue_ReturnsFalse()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            NextServiceMileage = 40000m,
            NextServiceDate = DateTime.UtcNow.AddMonths(3),
            IsActive = true
        };

        // Act
        var isDue = schedule.IsDue(35000m, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.False);
    }

    [Test]
    public void IsDue_WhenInactive_ReturnsFalse()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            NextServiceMileage = 30000m,
            IsActive = false
        };

        // Act
        var isDue = schedule.IsDue(35000m, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.False);
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            IsActive = true
        };

        // Act
        schedule.Deactivate();

        // Assert
        Assert.That(schedule.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_SetsIsActiveToTrue()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            MileageInterval = 5000m,
            IsActive = false
        };

        // Act
        schedule.Reactivate();

        // Assert
        Assert.That(schedule.IsActive, Is.True);
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.TireService,
            Description = "Tire rotation"
        };

        // Assert
        Assert.That(schedule.IsActive, Is.True);
    }

    [Test]
    public void RecordService_OnlyMileageInterval_UpdatesMileageOnly()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.TireService,
            Description = "Tire rotation",
            MileageInterval = 7500m
        };

        var serviceMileage = 20000m;
        var serviceDate = DateTime.UtcNow;

        // Act
        schedule.RecordService(serviceMileage, serviceDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.NextServiceMileage, Is.EqualTo(27500m));
            Assert.That(schedule.NextServiceDate, Is.Null);
        });
    }

    [Test]
    public void RecordService_OnlyMonthsInterval_UpdatesDateOnly()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.Inspection,
            Description = "Annual inspection",
            MonthsInterval = 12
        };

        var serviceMileage = 30000m;
        var serviceDate = new DateTime(2024, 3, 15);

        // Act
        schedule.RecordService(serviceMileage, serviceDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.NextServiceMileage, Is.Null);
            Assert.That(schedule.NextServiceDate, Is.EqualTo(new DateTime(2025, 3, 15)));
        });
    }

    [Test]
    public void IsDue_ExactMileage_ReturnsTrue()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Oil change",
            NextServiceMileage = 35000m,
            IsActive = true
        };

        // Act
        var isDue = schedule.IsDue(35000m, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.True);
    }
}
