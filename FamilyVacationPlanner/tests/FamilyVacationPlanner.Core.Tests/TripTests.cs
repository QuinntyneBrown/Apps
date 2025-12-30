// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class TripTests
{
    [Test]
    public void Constructor_CreatesTrip_WithDefaultValues()
    {
        // Arrange & Act
        var trip = new Trip();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.EqualTo(Guid.Empty));
            Assert.That(trip.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(trip.Name, Is.EqualTo(string.Empty));
            Assert.That(trip.Destination, Is.Null);
            Assert.That(trip.StartDate, Is.Null);
            Assert.That(trip.EndDate, Is.Null);
            Assert.That(trip.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(trip.Itineraries, Is.Not.Null);
            Assert.That(trip.Itineraries, Is.Empty);
            Assert.That(trip.Bookings, Is.Not.Null);
            Assert.That(trip.Bookings, Is.Empty);
            Assert.That(trip.Budgets, Is.Not.Null);
            Assert.That(trip.Budgets, Is.Empty);
            Assert.That(trip.PackingLists, Is.Not.Null);
            Assert.That(trip.PackingLists, Is.Empty);
        });
    }

    [Test]
    public void TripId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var trip = new Trip();
        var expectedId = Guid.NewGuid();

        // Act
        trip.TripId = expectedId;

        // Assert
        Assert.That(trip.TripId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var trip = new Trip();
        var expectedName = "Hawaii Vacation 2024";

        // Act
        trip.Name = expectedName;

        // Assert
        Assert.That(trip.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Destination_CanBeSet_AndRetrieved()
    {
        // Arrange
        var trip = new Trip();
        var expectedDestination = "Honolulu, Hawaii";

        // Act
        trip.Destination = expectedDestination;

        // Assert
        Assert.That(trip.Destination, Is.EqualTo(expectedDestination));
    }

    [Test]
    public void StartDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var trip = new Trip();
        var expectedDate = new DateTime(2024, 6, 1);

        // Act
        trip.StartDate = expectedDate;

        // Assert
        Assert.That(trip.StartDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void EndDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var trip = new Trip();
        var expectedDate = new DateTime(2024, 6, 10);

        // Act
        trip.EndDate = expectedDate;

        // Assert
        Assert.That(trip.EndDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Itineraries_CanBeAdded()
    {
        // Arrange
        var trip = new Trip();
        var itinerary = new Itinerary { ItineraryId = Guid.NewGuid() };

        // Act
        trip.Itineraries.Add(itinerary);

        // Assert
        Assert.That(trip.Itineraries, Has.Count.EqualTo(1));
    }

    [Test]
    public void Bookings_CanBeAdded()
    {
        // Arrange
        var trip = new Trip();
        var booking = new Booking { BookingId = Guid.NewGuid() };

        // Act
        trip.Bookings.Add(booking);

        // Assert
        Assert.That(trip.Bookings, Has.Count.EqualTo(1));
    }

    [Test]
    public void Budgets_CanBeAdded()
    {
        // Arrange
        var trip = new Trip();
        var budget = new VacationBudget { VacationBudgetId = Guid.NewGuid() };

        // Act
        trip.Budgets.Add(budget);

        // Assert
        Assert.That(trip.Budgets, Has.Count.EqualTo(1));
    }

    [Test]
    public void PackingLists_CanBeAdded()
    {
        // Arrange
        var trip = new Trip();
        var packingItem = new PackingList { PackingListId = Guid.NewGuid() };

        // Act
        trip.PackingLists.Add(packingItem);

        // Assert
        Assert.That(trip.PackingLists, Has.Count.EqualTo(1));
    }

    [Test]
    public void Trip_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "European Adventure";
        var destination = "Paris, France";
        var startDate = new DateTime(2024, 7, 15);
        var endDate = new DateTime(2024, 7, 30);
        var createdAt = DateTime.UtcNow;

        // Act
        var trip = new Trip
        {
            TripId = tripId,
            UserId = userId,
            Name = name,
            Destination = destination,
            StartDate = startDate,
            EndDate = endDate,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.EqualTo(tripId));
            Assert.That(trip.UserId, Is.EqualTo(userId));
            Assert.That(trip.Name, Is.EqualTo(name));
            Assert.That(trip.Destination, Is.EqualTo(destination));
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
            Assert.That(trip.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
