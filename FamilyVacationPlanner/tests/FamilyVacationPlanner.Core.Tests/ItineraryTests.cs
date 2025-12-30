// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class ItineraryTests
{
    [Test]
    public void Constructor_CreatesItinerary_WithDefaultValues()
    {
        // Arrange & Act
        var itinerary = new Itinerary();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itinerary.ItineraryId, Is.EqualTo(Guid.Empty));
            Assert.That(itinerary.TripId, Is.EqualTo(Guid.Empty));
            Assert.That(itinerary.Trip, Is.Null);
            Assert.That(itinerary.Date, Is.EqualTo(default(DateTime)));
            Assert.That(itinerary.Activity, Is.Null);
            Assert.That(itinerary.Location, Is.Null);
            Assert.That(itinerary.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ItineraryId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var itinerary = new Itinerary();
        var expectedId = Guid.NewGuid();

        // Act
        itinerary.ItineraryId = expectedId;

        // Assert
        Assert.That(itinerary.ItineraryId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Date_CanBeSet_AndRetrieved()
    {
        // Arrange
        var itinerary = new Itinerary();
        var expectedDate = new DateTime(2024, 6, 5, 10, 0, 0);

        // Act
        itinerary.Date = expectedDate;

        // Assert
        Assert.That(itinerary.Date, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Activity_CanBeSet_AndRetrieved()
    {
        // Arrange
        var itinerary = new Itinerary();
        var expectedActivity = "Beach day and snorkeling";

        // Act
        itinerary.Activity = expectedActivity;

        // Assert
        Assert.That(itinerary.Activity, Is.EqualTo(expectedActivity));
    }

    [Test]
    public void Location_CanBeSet_AndRetrieved()
    {
        // Arrange
        var itinerary = new Itinerary();
        var expectedLocation = "Waikiki Beach";

        // Act
        itinerary.Location = expectedLocation;

        // Assert
        Assert.That(itinerary.Location, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void Activity_CanBeNull()
    {
        // Arrange
        var itinerary = new Itinerary();

        // Act
        itinerary.Activity = null;

        // Assert
        Assert.That(itinerary.Activity, Is.Null);
    }

    [Test]
    public void Itinerary_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var itineraryId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var date = new DateTime(2024, 6, 10, 14, 30, 0);
        var activity = "Visit Eiffel Tower";
        var location = "Paris, France";
        var createdAt = DateTime.UtcNow;

        // Act
        var itinerary = new Itinerary
        {
            ItineraryId = itineraryId,
            TripId = tripId,
            Date = date,
            Activity = activity,
            Location = location,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itinerary.ItineraryId, Is.EqualTo(itineraryId));
            Assert.That(itinerary.TripId, Is.EqualTo(tripId));
            Assert.That(itinerary.Date, Is.EqualTo(date));
            Assert.That(itinerary.Activity, Is.EqualTo(activity));
            Assert.That(itinerary.Location, Is.EqualTo(location));
            Assert.That(itinerary.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Itinerary_WithoutActivity_CanBeCreated()
    {
        // Arrange
        var itineraryId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var date = new DateTime(2024, 6, 15);

        // Act
        var itinerary = new Itinerary
        {
            ItineraryId = itineraryId,
            TripId = tripId,
            Date = date
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itinerary.ItineraryId, Is.EqualTo(itineraryId));
            Assert.That(itinerary.TripId, Is.EqualTo(tripId));
            Assert.That(itinerary.Date, Is.EqualTo(date));
            Assert.That(itinerary.Activity, Is.Null);
            Assert.That(itinerary.Location, Is.Null);
        });
    }
}
