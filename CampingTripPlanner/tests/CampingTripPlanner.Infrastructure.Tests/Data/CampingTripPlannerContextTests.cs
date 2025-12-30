// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the CampingTripPlannerContext.
/// </summary>
[TestFixture]
public class CampingTripPlannerContextTests
{
    private CampingTripPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CampingTripPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CampingTripPlannerContext(options);
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
    /// Tests that Campsites can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Campsites_CanAddAndRetrieve()
    {
        // Arrange
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Campsite",
            Location = "Test Location",
            CampsiteType = CampsiteType.Tent,
            Description = "Test Description",
            HasElectricity = true,
            HasWater = true,
            CostPerNight = 30.00m,
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Campsites.Add(campsite);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Campsites.FindAsync(campsite.CampsiteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Campsite"));
        Assert.That(retrieved.CampsiteType, Is.EqualTo(CampsiteType.Tent));
        Assert.That(retrieved.IsFavorite, Is.True);
    }

    /// <summary>
    /// Tests that Trips can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Trips_CanAddAndRetrieve()
    {
        // Arrange
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Campsite",
            Location = "Test Location",
            CampsiteType = CampsiteType.RV,
            HasElectricity = true,
            HasWater = true,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = campsite.UserId,
            Name = "Test Trip",
            CampsiteId = campsite.CampsiteId,
            StartDate = DateTime.UtcNow.AddDays(7),
            EndDate = DateTime.UtcNow.AddDays(10),
            NumberOfPeople = 4,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Campsites.Add(campsite);
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Trip"));
        Assert.That(retrieved.NumberOfPeople, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that GearChecklists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task GearChecklists_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            NumberOfPeople = 2,
            CreatedAt = DateTime.UtcNow,
        };

        var gearChecklist = new GearChecklist
        {
            GearChecklistId = Guid.NewGuid(),
            UserId = trip.UserId,
            TripId = trip.TripId,
            ItemName = "Tent",
            IsPacked = false,
            Quantity = 1,
            Notes = "4-person tent",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        _context.GearChecklists.Add(gearChecklist);
        await _context.SaveChangesAsync();

        var retrieved = await _context.GearChecklists.FindAsync(gearChecklist.GearChecklistId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ItemName, Is.EqualTo("Tent"));
        Assert.That(retrieved.IsPacked, Is.False);
    }

    /// <summary>
    /// Tests that Reviews can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reviews_CanAddAndRetrieve()
    {
        // Arrange
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Campsite",
            Location = "Test Location",
            CampsiteType = CampsiteType.Primitive,
            HasElectricity = false,
            HasWater = false,
            CreatedAt = DateTime.UtcNow,
        };

        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = campsite.UserId,
            CampsiteId = campsite.CampsiteId,
            Rating = 5,
            ReviewText = "Amazing campsite!",
            ReviewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Campsites.Add(campsite);
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reviews.FindAsync(review.ReviewId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(5));
        Assert.That(retrieved.ReviewText, Is.EqualTo("Amazing campsite!"));
    }
}
