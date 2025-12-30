// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FishingLogSpotTrackerContext.
/// </summary>
[TestFixture]
public class FishingLogSpotTrackerContextTests
{
    private FishingLogSpotTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FishingLogSpotTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FishingLogSpotTrackerContext(options);
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
    /// Tests that Spots can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Spots_CanAddAndRetrieve()
    {
        // Arrange
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Fishing Spot",
            LocationType = LocationType.Lake,
            Latitude = 45.5234m,
            Longitude = -122.6762m,
            Description = "Great fishing location",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Spots.Add(spot);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Spots.FindAsync(spot.SpotId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Fishing Spot"));
        Assert.That(retrieved.LocationType, Is.EqualTo(LocationType.Lake));
        Assert.That(retrieved.IsFavorite, Is.True);
    }

    /// <summary>
    /// Tests that Trips can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Trips_CanAddAndRetrieve()
    {
        // Arrange
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Spot",
            LocationType = LocationType.Lake,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = spot.UserId,
            SpotId = spot.SpotId,
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow.AddHours(-4),
            EndTime = DateTime.UtcNow,
            WeatherConditions = "Sunny",
            WaterTemperature = 68.5m,
            AirTemperature = 75.0m,
            Notes = "Great day fishing",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Spots.Add(spot);
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SpotId, Is.EqualTo(spot.SpotId));
        Assert.That(retrieved.WeatherConditions, Is.EqualTo("Sunny"));
        Assert.That(retrieved.WaterTemperature, Is.EqualTo(68.5m));
    }

    /// <summary>
    /// Tests that Catches can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Catches_CanAddAndRetrieve()
    {
        // Arrange
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Spot",
            LocationType = LocationType.River,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = spot.UserId,
            SpotId = spot.SpotId,
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow.AddHours(-4),
            CreatedAt = DateTime.UtcNow,
        };

        var catchRecord = new Catch
        {
            CatchId = Guid.NewGuid(),
            UserId = spot.UserId,
            TripId = trip.TripId,
            Species = FishSpecies.LargemouthBass,
            Length = 18.5m,
            Weight = 4.2m,
            CatchTime = DateTime.UtcNow,
            BaitUsed = "Plastic worm",
            WasReleased = true,
            Notes = "Beautiful fish",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Spots.Add(spot);
        _context.Trips.Add(trip);
        _context.Catches.Add(catchRecord);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Catches.FindAsync(catchRecord.CatchId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TripId, Is.EqualTo(trip.TripId));
        Assert.That(retrieved.Species, Is.EqualTo(FishSpecies.LargemouthBass));
        Assert.That(retrieved.Length, Is.EqualTo(18.5m));
        Assert.That(retrieved.WasReleased, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for Trip and Catches.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedCatches()
    {
        // Arrange
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Spot",
            LocationType = LocationType.Lake,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = spot.UserId,
            SpotId = spot.SpotId,
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var catchRecord = new Catch
        {
            CatchId = Guid.NewGuid(),
            UserId = spot.UserId,
            TripId = trip.TripId,
            Species = FishSpecies.RainbowTrout,
            CatchTime = DateTime.UtcNow,
            WasReleased = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Spots.Add(spot);
        _context.Trips.Add(trip);
        _context.Catches.Add(catchRecord);
        await _context.SaveChangesAsync();

        // Act
        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();

        var retrievedCatch = await _context.Catches.FindAsync(catchRecord.CatchId);

        // Assert
        Assert.That(retrievedCatch, Is.Null);
    }

    /// <summary>
    /// Tests that deleting a Spot sets Trip.SpotId to null.
    /// </summary>
    [Test]
    public async Task DeleteSpot_SetsTripSpotIdToNull()
    {
        // Arrange
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Spot",
            LocationType = LocationType.Lake,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = spot.UserId,
            SpotId = spot.SpotId,
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Spots.Add(spot);
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        // Act
        _context.Spots.Remove(spot);
        await _context.SaveChangesAsync();

        var retrievedTrip = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrievedTrip, Is.Not.Null);
        Assert.That(retrievedTrip!.SpotId, Is.Null);
    }
}
