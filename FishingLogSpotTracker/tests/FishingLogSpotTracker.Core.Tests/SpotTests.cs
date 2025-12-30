// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Core.Tests;

public class SpotTests
{
    [Test]
    public void Spot_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var spotId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Bass Lake Cove";
        var locationType = LocationType.Lake;
        var latitude = 34.5678m;
        var longitude = -118.1234m;

        // Act
        var spot = new Spot
        {
            SpotId = spotId,
            UserId = userId,
            Name = name,
            LocationType = locationType,
            Latitude = latitude,
            Longitude = longitude,
            Description = "Great spot for bass",
            WaterBodyName = "Bass Lake",
            Directions = "Take exit 45",
            IsFavorite = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(spot.SpotId, Is.EqualTo(spotId));
            Assert.That(spot.UserId, Is.EqualTo(userId));
            Assert.That(spot.Name, Is.EqualTo(name));
            Assert.That(spot.LocationType, Is.EqualTo(locationType));
            Assert.That(spot.Latitude, Is.EqualTo(latitude));
            Assert.That(spot.Longitude, Is.EqualTo(longitude));
            Assert.That(spot.Description, Is.EqualTo("Great spot for bass"));
            Assert.That(spot.WaterBodyName, Is.EqualTo("Bass Lake"));
            Assert.That(spot.Directions, Is.EqualTo("Take exit 45"));
            Assert.That(spot.IsFavorite, Is.True);
            Assert.That(spot.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(spot.Trips, Is.Not.Null);
        });
    }

    [Test]
    public void Spot_DefaultValues_AreSetCorrectly()
    {
        // Act
        var spot = new Spot();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(spot.Name, Is.EqualTo(string.Empty));
            Assert.That(spot.IsFavorite, Is.False);
            Assert.That(spot.Trips, Is.Not.Null);
            Assert.That(spot.Trips.Count, Is.EqualTo(0));
            Assert.That(spot.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ToggleFavorite_WhenNotFavorite_SetsFavoriteToTrue()
    {
        // Arrange
        var spot = new Spot { IsFavorite = false };

        // Act
        spot.ToggleFavorite();

        // Assert
        Assert.That(spot.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenFavorite_SetsFavoriteToFalse()
    {
        // Arrange
        var spot = new Spot { IsFavorite = true };

        // Act
        spot.ToggleFavorite();

        // Assert
        Assert.That(spot.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_CalledTwice_ReturnsToOriginalState()
    {
        // Arrange
        var spot = new Spot { IsFavorite = false };

        // Act
        spot.ToggleFavorite();
        spot.ToggleFavorite();

        // Assert
        Assert.That(spot.IsFavorite, Is.False);
    }

    [Test]
    public void GetTripCount_WithNoTrips_ReturnsZero()
    {
        // Arrange
        var spot = new Spot();

        // Act
        var count = spot.GetTripCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetTripCount_WithMultipleTrips_ReturnsCorrectCount()
    {
        // Arrange
        var spot = new Spot
        {
            Trips = new List<Trip>
            {
                new Trip { TripId = Guid.NewGuid() },
                new Trip { TripId = Guid.NewGuid() },
                new Trip { TripId = Guid.NewGuid() }
            }
        };

        // Act
        var count = spot.GetTripCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void GetTripCount_WithNullTrips_ReturnsZero()
    {
        // Arrange
        var spot = new Spot { Trips = null! };

        // Act
        var count = spot.GetTripCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Spot_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var spot = new Spot
        {
            Latitude = null,
            Longitude = null,
            Description = null,
            WaterBodyName = null,
            Directions = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(spot.Latitude, Is.Null);
            Assert.That(spot.Longitude, Is.Null);
            Assert.That(spot.Description, Is.Null);
            Assert.That(spot.WaterBodyName, Is.Null);
            Assert.That(spot.Directions, Is.Null);
        });
    }

    [Test]
    public void Spot_LocationType_CanBeSetToAllValues()
    {
        // Arrange
        var spot = new Spot();

        // Act & Assert
        foreach (LocationType locationType in Enum.GetValues(typeof(LocationType)))
        {
            spot.LocationType = locationType;
            Assert.That(spot.LocationType, Is.EqualTo(locationType));
        }
    }
}
