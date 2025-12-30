// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Infrastructure.Tests;

/// <summary>
/// Unit tests for the VehicleMaintenanceLoggerContext.
/// </summary>
[TestFixture]
public class VehicleMaintenanceLoggerContextTests
{
    private VehicleMaintenanceLoggerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VehicleMaintenanceLoggerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VehicleMaintenanceLoggerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Vehicles can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Vehicles_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC123",
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 45000,
            IsActive = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vehicles.FindAsync(vehicle.VehicleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Make, Is.EqualTo("Toyota"));
        Assert.That(retrieved.Model, Is.EqualTo("Camry"));
        Assert.That(retrieved.VehicleType, Is.EqualTo(VehicleType.Sedan));
    }

    /// <summary>
    /// Tests that ServiceRecords can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ServiceRecords_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 30000,
            IsActive = true,
        };

        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ServiceType = ServiceType.OilChange,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 30000,
            Cost = 45.99m,
            Description = "Regular oil change",
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.ServiceRecords.Add(serviceRecord);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ServiceRecords.FindAsync(serviceRecord.ServiceRecordId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VehicleId, Is.EqualTo(vehicle.VehicleId));
        Assert.That(retrieved.ServiceType, Is.EqualTo(ServiceType.OilChange));
        Assert.That(retrieved.Cost, Is.EqualTo(45.99m));
    }

    /// <summary>
    /// Tests that MaintenanceSchedules can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MaintenanceSchedules_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150",
            Year = 2021,
            VehicleType = VehicleType.Truck,
            CurrentMileage = 15000,
            IsActive = true,
        };

        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change schedule",
            MileageInterval = 5000,
            MonthsInterval = 6,
            IsActive = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.MaintenanceSchedules.Add(schedule);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MaintenanceSchedules.FindAsync(schedule.MaintenanceScheduleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VehicleId, Is.EqualTo(vehicle.VehicleId));
        Assert.That(retrieved.ServiceType, Is.EqualTo(ServiceType.OilChange));
        Assert.That(retrieved.MileageInterval, Is.EqualTo(5000));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Chevrolet",
            Model = "Silverado",
            Year = 2018,
            VehicleType = VehicleType.Truck,
            CurrentMileage = 75000,
            IsActive = true,
        };

        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ServiceType = ServiceType.BrakeService,
            ServiceDate = DateTime.UtcNow,
            MileageAtService = 75000,
            Cost = 350.00m,
            Description = "Brake pad replacement",
        };

        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ServiceType = ServiceType.TireRotation,
            Description = "Tire rotation schedule",
            MileageInterval = 7500,
            IsActive = true,
        };

        _context.Vehicles.Add(vehicle);
        _context.ServiceRecords.Add(serviceRecord);
        _context.MaintenanceSchedules.Add(schedule);
        await _context.SaveChangesAsync();

        // Act
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        var retrievedServiceRecord = await _context.ServiceRecords.FindAsync(serviceRecord.ServiceRecordId);
        var retrievedSchedule = await _context.MaintenanceSchedules.FindAsync(schedule.MaintenanceScheduleId);

        // Assert
        Assert.That(retrievedServiceRecord, Is.Null);
        Assert.That(retrievedSchedule, Is.Null);
    }
}
