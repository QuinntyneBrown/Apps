// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FamilyVacationPlannerContext.
/// </summary>
[TestFixture]
public class FamilyVacationPlannerContextTests
{
    private FamilyVacationPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyVacationPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyVacationPlannerContext(options);
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
    /// Tests that Trips can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Trips_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Vacation",
            Destination = "Test Destination",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Trips.FindAsync(trip.TripId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Vacation"));
        Assert.That(retrieved.Destination, Is.EqualTo("Test Destination"));
    }

    /// <summary>
    /// Tests that Itineraries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Itineraries_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            CreatedAt = DateTime.UtcNow,
        };

        var itinerary = new Itinerary
        {
            ItineraryId = Guid.NewGuid(),
            TripId = trip.TripId,
            Date = DateTime.UtcNow,
            Activity = "Test Activity",
            Location = "Test Location",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        _context.Itineraries.Add(itinerary);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Itineraries.FindAsync(itinerary.ItineraryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Activity, Is.EqualTo("Test Activity"));
        Assert.That(retrieved.Location, Is.EqualTo("Test Location"));
    }

    /// <summary>
    /// Tests that Bookings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Bookings_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            CreatedAt = DateTime.UtcNow,
        };

        var booking = new Booking
        {
            BookingId = Guid.NewGuid(),
            TripId = trip.TripId,
            Type = "Hotel",
            ConfirmationNumber = "TEST-12345",
            Cost = 500.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Bookings.FindAsync(booking.BookingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Type, Is.EqualTo("Hotel"));
        Assert.That(retrieved.ConfirmationNumber, Is.EqualTo("TEST-12345"));
        Assert.That(retrieved.Cost, Is.EqualTo(500.00m));
    }

    /// <summary>
    /// Tests that VacationBudgets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task VacationBudgets_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            CreatedAt = DateTime.UtcNow,
        };

        var budget = new VacationBudget
        {
            VacationBudgetId = Guid.NewGuid(),
            TripId = trip.TripId,
            Category = "Food",
            AllocatedAmount = 1000.00m,
            SpentAmount = 500.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        _context.VacationBudgets.Add(budget);
        await _context.SaveChangesAsync();

        var retrieved = await _context.VacationBudgets.FindAsync(budget.VacationBudgetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Category, Is.EqualTo("Food"));
        Assert.That(retrieved.AllocatedAmount, Is.EqualTo(1000.00m));
        Assert.That(retrieved.SpentAmount, Is.EqualTo(500.00m));
    }

    /// <summary>
    /// Tests that PackingLists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task PackingLists_CanAddAndRetrieve()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            CreatedAt = DateTime.UtcNow,
        };

        var packingItem = new PackingList
        {
            PackingListId = Guid.NewGuid(),
            TripId = trip.TripId,
            ItemName = "Toothbrush",
            IsPacked = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trips.Add(trip);
        _context.PackingLists.Add(packingItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.PackingLists.FindAsync(packingItem.PackingListId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ItemName, Is.EqualTo("Toothbrush"));
        Assert.That(retrieved.IsPacked, Is.False);
    }

    /// <summary>
    /// Tests cascade delete for Trip and related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Trip to Delete",
            CreatedAt = DateTime.UtcNow,
        };

        var itinerary = new Itinerary
        {
            ItineraryId = Guid.NewGuid(),
            TripId = trip.TripId,
            Date = DateTime.UtcNow,
            Activity = "Activity to Delete",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Trips.Add(trip);
        _context.Itineraries.Add(itinerary);
        await _context.SaveChangesAsync();

        // Act
        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();

        var retrievedItinerary = await _context.Itineraries.FindAsync(itinerary.ItineraryId);

        // Assert
        Assert.That(retrievedItinerary, Is.Null);
    }
}
