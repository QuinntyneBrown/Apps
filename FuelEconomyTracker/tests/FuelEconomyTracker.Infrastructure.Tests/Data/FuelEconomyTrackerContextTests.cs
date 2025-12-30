// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FuelEconomyTrackerContext.
/// </summary>
[TestFixture]
public class FuelEconomyTrackerContextTests
{
    private FuelEconomyTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FuelEconomyTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FuelEconomyTrackerContext(options);
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
            UserId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            FuelType = FuelType.Gasoline,
            TankCapacity = 15.8m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vehicles.FindAsync(vehicle.VehicleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Make, Is.EqualTo("Toyota"));
        Assert.That(retrieved.Model, Is.EqualTo("Camry"));
        Assert.That(retrieved.Year, Is.EqualTo(2020));
    }

    /// <summary>
    /// Tests that FillUps can be added and retrieved.
    /// </summary>
    [Test]
    public async Task FillUps_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            FuelType = FuelType.Gasoline,
            TankCapacity = 12.4m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var fillUp = new FillUp
        {
            FillUpId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            Date = DateTime.UtcNow,
            Odometer = 25000,
            GallonsFilled = 12.5m,
            PricePerGallon = 3.45m,
            TotalCost = 43.13m,
            IsFullTank = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.FillUps.Add(fillUp);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FillUps.FindAsync(fillUp.FillUpId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Odometer, Is.EqualTo(25000));
        Assert.That(retrieved.GallonsFilled, Is.EqualTo(12.5m));
        Assert.That(retrieved.TotalCost, Is.EqualTo(43.13m));
    }

    /// <summary>
    /// Tests that Trips can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Trips_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150",
            Year = 2021,
            FuelType = FuelType.Gasoline,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(-5),
            StartOdometer = 20000,
            EndOdometer = 20350,
            TripType = TripType.Business,
            Purpose = "Client visit",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.StartOdometer, Is.EqualTo(20000));
        Assert.That(retrieved.EndOdometer, Is.EqualTo(20350));
        Assert.That(retrieved.TripType, Is.EqualTo(TripType.Business));
    }

    /// <summary>
    /// Tests that EfficiencyReports can be added and retrieved.
    /// </summary>
    [Test]
    public async Task EfficiencyReports_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Make = "Tesla",
            Model = "Model 3",
            Year = 2022,
            FuelType = FuelType.Electric,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var report = new EfficiencyReport
        {
            EfficiencyReportId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            PeriodStartDate = DateTime.UtcNow.AddDays(-30),
            PeriodEndDate = DateTime.UtcNow,
            TotalMilesDriven = 850,
            TotalGallonsUsed = 36.5m,
            AverageMPG = 23.3m,
            TotalCost = 125.81m,
            AverageCostPerMile = 0.148m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.EfficiencyReports.Add(report);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EfficiencyReports.FindAsync(report.EfficiencyReportId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalMilesDriven, Is.EqualTo(850));
        Assert.That(retrieved.AverageMPG, Is.EqualTo(23.3m));
        Assert.That(retrieved.TotalCost, Is.EqualTo(125.81m));
    }

    /// <summary>
    /// Tests that cascade delete works for Vehicle and FillUps.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedFillUps()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Make = "Mazda",
            Model = "CX-5",
            Year = 2020,
            FuelType = FuelType.Gasoline,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var fillUp = new FillUp
        {
            FillUpId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            Date = DateTime.UtcNow,
            Odometer = 15000,
            GallonsFilled = 10.0m,
            IsFullTank = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Vehicles.Add(vehicle);
        _context.FillUps.Add(fillUp);
        await _context.SaveChangesAsync();

        // Act
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        var retrievedFillUp = await _context.FillUps.FindAsync(fillUp.FillUpId);

        // Assert
        Assert.That(retrievedFillUp, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for Vehicle and Trips.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedTrips()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Make = "Subaru",
            Model = "Outback",
            Year = 2021,
            FuelType = FuelType.Gasoline,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            StartDate = DateTime.UtcNow,
            StartOdometer = 10000,
            TripType = TripType.Personal,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Vehicles.Add(vehicle);
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        // Act
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        var retrievedTrip = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrievedTrip, Is.Null);
    }
}
