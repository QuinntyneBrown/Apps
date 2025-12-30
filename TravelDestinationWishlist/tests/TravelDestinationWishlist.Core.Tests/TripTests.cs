// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core.Tests;

public class TripTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTrip()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var destinationId = Guid.NewGuid();
        var startDate = new DateTime(2024, 6, 1);
        var endDate = new DateTime(2024, 6, 15);
        var totalCost = 2500.50m;
        var accommodation = "Hotel XYZ";
        var transportation = "Flight + Rental Car";
        var notes = "Summer vacation";

        // Act
        var trip = new Trip
        {
            TripId = tripId,
            UserId = userId,
            DestinationId = destinationId,
            StartDate = startDate,
            EndDate = endDate,
            TotalCost = totalCost,
            Accommodation = accommodation,
            Transportation = transportation,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.EqualTo(tripId));
            Assert.That(trip.UserId, Is.EqualTo(userId));
            Assert.That(trip.DestinationId, Is.EqualTo(destinationId));
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
            Assert.That(trip.TotalCost, Is.EqualTo(totalCost));
            Assert.That(trip.Accommodation, Is.EqualTo(accommodation));
            Assert.That(trip.Transportation, Is.EqualTo(transportation));
            Assert.That(trip.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(trip.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Trip_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TotalCost, Is.Null);
            Assert.That(trip.Accommodation, Is.Null);
            Assert.That(trip.Transportation, Is.Null);
            Assert.That(trip.Notes, Is.Null);
        });
    }

    [Test]
    public void Destination_NavigationProperty_CanBeSet()
    {
        // Arrange
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Paris",
            Country = "France",
            DestinationType = DestinationType.City
        };

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = destination.DestinationId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        trip.Destination = destination;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.Destination, Is.Not.Null);
            Assert.That(trip.Destination.DestinationId, Is.EqualTo(destination.DestinationId));
        });
    }

    [Test]
    public void Memories_InitializesAsEmptyList()
    {
        // Arrange & Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.Memories, Is.Not.Null);
            Assert.That(trip.Memories, Is.Empty);
        });
    }

    [Test]
    public void TotalCost_CanBeZero()
    {
        // Arrange & Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            TotalCost = 0m
        };

        // Assert
        Assert.That(trip.TotalCost, Is.EqualTo(0m));
    }

    [Test]
    public void Trip_WithLargeCost_StoresCorrectly()
    {
        // Arrange
        var largeCost = 99999.99m;

        // Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            TotalCost = largeCost
        };

        // Assert
        Assert.That(trip.TotalCost, Is.EqualTo(largeCost));
    }

    [Test]
    public void Trip_SingleDayTrip_IsValid()
    {
        // Arrange
        var date = new DateTime(2024, 6, 1);

        // Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = date,
            EndDate = date
        };

        // Assert
        Assert.That(trip.StartDate, Is.EqualTo(trip.EndDate));
    }

    [Test]
    public void Trip_LongDuration_IsValid()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);

        // Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
        });
    }
}
