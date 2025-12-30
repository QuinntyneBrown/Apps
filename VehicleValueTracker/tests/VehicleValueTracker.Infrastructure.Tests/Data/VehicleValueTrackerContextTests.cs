// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the VehicleValueTrackerContext.
/// </summary>
[TestFixture]
public class VehicleValueTrackerContextTests
{
    private VehicleValueTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VehicleValueTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VehicleValueTrackerContext(options);
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
            Make = "Tesla",
            Model = "Model 3",
            Year = 2021,
            Trim = "Long Range",
            VIN = "5YJ3E1EA5MF123456",
            CurrentMileage = 25000,
            PurchasePrice = 52000m,
            IsCurrentlyOwned = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vehicles.FindAsync(vehicle.VehicleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Make, Is.EqualTo("Tesla"));
        Assert.That(retrieved.Model, Is.EqualTo("Model 3"));
        Assert.That(retrieved.PurchasePrice, Is.EqualTo(52000m));
    }

    /// <summary>
    /// Tests that ValueAssessments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ValueAssessments_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "BMW",
            Model = "X5",
            Year = 2019,
            CurrentMileage = 45000,
            IsCurrentlyOwned = true,
        };

        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 38000m,
            MileageAtAssessment = 45000,
            ConditionGrade = ConditionGrade.VeryGood,
            ValuationSource = "KBB",
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.ValueAssessments.Add(assessment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ValueAssessments.FindAsync(assessment.ValueAssessmentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VehicleId, Is.EqualTo(vehicle.VehicleId));
        Assert.That(retrieved.EstimatedValue, Is.EqualTo(38000m));
        Assert.That(retrieved.ConditionGrade, Is.EqualTo(ConditionGrade.VeryGood));
    }

    /// <summary>
    /// Tests that MarketComparisons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MarketComparisons_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Accord",
            Year = 2020,
            CurrentMileage = 30000,
            IsCurrentlyOwned = true,
        };

        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "Autotrader",
            ComparableYear = 2020,
            ComparableMake = "Honda",
            ComparableModel = "Accord",
            ComparableMileage = 32000,
            AskingPrice = 24500m,
            IsActive = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.MarketComparisons.Add(comparison);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MarketComparisons.FindAsync(comparison.MarketComparisonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VehicleId, Is.EqualTo(vehicle.VehicleId));
        Assert.That(retrieved.AskingPrice, Is.EqualTo(24500m));
        Assert.That(retrieved.ListingSource, Is.EqualTo("Autotrader"));
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
            Make = "Ford",
            Model = "Mustang",
            Year = 2018,
            CurrentMileage = 50000,
            IsCurrentlyOwned = true,
        };

        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 28000m,
            MileageAtAssessment = 50000,
            ConditionGrade = ConditionGrade.Good,
        };

        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "CarGurus",
            ComparableYear = 2018,
            ComparableMake = "Ford",
            ComparableModel = "Mustang",
            ComparableMileage = 48000,
            AskingPrice = 29000m,
            IsActive = true,
        };

        _context.Vehicles.Add(vehicle);
        _context.ValueAssessments.Add(assessment);
        _context.MarketComparisons.Add(comparison);
        await _context.SaveChangesAsync();

        // Act
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        var retrievedAssessment = await _context.ValueAssessments.FindAsync(assessment.ValueAssessmentId);
        var retrievedComparison = await _context.MarketComparisons.FindAsync(comparison.MarketComparisonId);

        // Assert
        Assert.That(retrievedAssessment, Is.Null);
        Assert.That(retrievedComparison, Is.Null);
    }
}
