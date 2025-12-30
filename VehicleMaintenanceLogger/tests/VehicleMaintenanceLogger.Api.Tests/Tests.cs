using VehicleMaintenanceLogger.Api.Features.Vehicles;
using VehicleMaintenanceLogger.Api.Features.ServiceRecords;
using VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;
using VehicleMaintenanceLogger.Core;

namespace VehicleMaintenanceLogger.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void VehicleDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2023,
            VIN = "1HGCM82633A123456",
            LicensePlate = "ABC-1234",
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 15000,
            PurchaseDate = new DateTime(2023, 1, 15),
            Notes = "Test vehicle",
            IsActive = true,
        };

        // Act
        var dto = vehicle.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.VehicleId, Is.EqualTo(vehicle.VehicleId));
            Assert.That(dto.Make, Is.EqualTo(vehicle.Make));
            Assert.That(dto.Model, Is.EqualTo(vehicle.Model));
            Assert.That(dto.Year, Is.EqualTo(vehicle.Year));
            Assert.That(dto.VIN, Is.EqualTo(vehicle.VIN));
            Assert.That(dto.LicensePlate, Is.EqualTo(vehicle.LicensePlate));
            Assert.That(dto.VehicleType, Is.EqualTo(vehicle.VehicleType));
            Assert.That(dto.CurrentMileage, Is.EqualTo(vehicle.CurrentMileage));
            Assert.That(dto.PurchaseDate, Is.EqualTo(vehicle.PurchaseDate));
            Assert.That(dto.Notes, Is.EqualTo(vehicle.Notes));
            Assert.That(dto.IsActive, Is.EqualTo(vehicle.IsActive));
        });
    }

    [Test]
    public void ServiceRecordDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            ServiceDate = new DateTime(2023, 6, 15),
            MileageAtService = 15000,
            Cost = 75.50m,
            ServiceProvider = "Quick Oil",
            Description = "Regular oil change",
            Notes = "Used synthetic oil",
            PartsReplaced = new List<string> { "Oil Filter", "Engine Oil" },
            InvoiceNumber = "INV-12345",
            WarrantyExpirationDate = new DateTime(2024, 6, 15),
        };

        // Act
        var dto = serviceRecord.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ServiceRecordId, Is.EqualTo(serviceRecord.ServiceRecordId));
            Assert.That(dto.VehicleId, Is.EqualTo(serviceRecord.VehicleId));
            Assert.That(dto.ServiceType, Is.EqualTo(serviceRecord.ServiceType));
            Assert.That(dto.ServiceDate, Is.EqualTo(serviceRecord.ServiceDate));
            Assert.That(dto.MileageAtService, Is.EqualTo(serviceRecord.MileageAtService));
            Assert.That(dto.Cost, Is.EqualTo(serviceRecord.Cost));
            Assert.That(dto.ServiceProvider, Is.EqualTo(serviceRecord.ServiceProvider));
            Assert.That(dto.Description, Is.EqualTo(serviceRecord.Description));
            Assert.That(dto.Notes, Is.EqualTo(serviceRecord.Notes));
            Assert.That(dto.PartsReplaced, Is.EqualTo(serviceRecord.PartsReplaced));
            Assert.That(dto.InvoiceNumber, Is.EqualTo(serviceRecord.InvoiceNumber));
            Assert.That(dto.WarrantyExpirationDate, Is.EqualTo(serviceRecord.WarrantyExpirationDate));
        });
    }

    [Test]
    public void MaintenanceScheduleDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change every 5000 miles",
            MileageInterval = 5000,
            MonthsInterval = 6,
            LastServiceMileage = 10000,
            LastServiceDate = new DateTime(2023, 1, 15),
            NextServiceMileage = 15000,
            NextServiceDate = new DateTime(2023, 7, 15),
            IsActive = true,
            Notes = "Use synthetic oil",
        };

        // Act
        var dto = schedule.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MaintenanceScheduleId, Is.EqualTo(schedule.MaintenanceScheduleId));
            Assert.That(dto.VehicleId, Is.EqualTo(schedule.VehicleId));
            Assert.That(dto.ServiceType, Is.EqualTo(schedule.ServiceType));
            Assert.That(dto.Description, Is.EqualTo(schedule.Description));
            Assert.That(dto.MileageInterval, Is.EqualTo(schedule.MileageInterval));
            Assert.That(dto.MonthsInterval, Is.EqualTo(schedule.MonthsInterval));
            Assert.That(dto.LastServiceMileage, Is.EqualTo(schedule.LastServiceMileage));
            Assert.That(dto.LastServiceDate, Is.EqualTo(schedule.LastServiceDate));
            Assert.That(dto.NextServiceMileage, Is.EqualTo(schedule.NextServiceMileage));
            Assert.That(dto.NextServiceDate, Is.EqualTo(schedule.NextServiceDate));
            Assert.That(dto.IsActive, Is.EqualTo(schedule.IsActive));
            Assert.That(dto.Notes, Is.EqualTo(schedule.Notes));
        });
    }

    [Test]
    public void MaintenanceSchedule_IsDue_ReturnsTrueWhenDueByMileage()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change",
            MileageInterval = 5000,
            NextServiceMileage = 15000,
            IsActive = true,
        };

        // Act
        var isDue = schedule.IsDue(15500, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.True);
    }

    [Test]
    public void MaintenanceSchedule_IsDue_ReturnsTrueWhenDueByDate()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change",
            MonthsInterval = 6,
            NextServiceDate = DateTime.UtcNow.AddDays(-1),
            IsActive = true,
        };

        // Act
        var isDue = schedule.IsDue(10000, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.True);
    }

    [Test]
    public void MaintenanceSchedule_IsDue_ReturnsFalseWhenNotDue()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change",
            MileageInterval = 5000,
            NextServiceMileage = 15000,
            NextServiceDate = DateTime.UtcNow.AddMonths(2),
            IsActive = true,
        };

        // Act
        var isDue = schedule.IsDue(10000, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.False);
    }

    [Test]
    public void MaintenanceSchedule_IsDue_ReturnsFalseWhenInactive()
    {
        // Arrange
        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change",
            MileageInterval = 5000,
            NextServiceMileage = 10000,
            IsActive = false,
        };

        // Act
        var isDue = schedule.IsDue(15000, DateTime.UtcNow);

        // Assert
        Assert.That(isDue, Is.False);
    }

    [Test]
    public void Vehicle_UpdateMileage_UpdatesCurrentMileage()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2023,
            CurrentMileage = 10000,
        };

        // Act
        vehicle.UpdateMileage(15000);

        // Assert
        Assert.That(vehicle.CurrentMileage, Is.EqualTo(15000));
    }

    [Test]
    public void Vehicle_UpdateMileage_ThrowsExceptionWhenMileageLessThanCurrent()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2023,
            CurrentMileage = 15000,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => vehicle.UpdateMileage(10000));
    }

    [Test]
    public void ServiceRecord_UpdateCost_UpdatesCost()
    {
        // Arrange
        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Cost = 50.00m,
        };

        // Act
        serviceRecord.UpdateCost(75.50m);

        // Assert
        Assert.That(serviceRecord.Cost, Is.EqualTo(75.50m));
    }

    [Test]
    public void ServiceRecord_UpdateCost_ThrowsExceptionWhenCostNegative()
    {
        // Arrange
        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Cost = 50.00m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => serviceRecord.UpdateCost(-10.00m));
    }
}
