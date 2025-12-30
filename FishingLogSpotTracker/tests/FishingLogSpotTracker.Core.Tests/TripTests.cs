// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Core.Tests;

public class TripTests
{
    [Test]
    public void Trip_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var spotId = Guid.NewGuid();
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddHours(4);

        // Act
        var trip = new Trip
        {
            TripId = tripId,
            UserId = userId,
            SpotId = spotId,
            StartTime = startTime,
            EndTime = endTime,
            WeatherConditions = "Sunny",
            WaterTemperature = 72.5m,
            AirTemperature = 75.0m,
            Notes = "Great fishing day"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.EqualTo(tripId));
            Assert.That(trip.UserId, Is.EqualTo(userId));
            Assert.That(trip.SpotId, Is.EqualTo(spotId));
            Assert.That(trip.StartTime, Is.EqualTo(startTime));
            Assert.That(trip.EndTime, Is.EqualTo(endTime));
            Assert.That(trip.WeatherConditions, Is.EqualTo("Sunny"));
            Assert.That(trip.WaterTemperature, Is.EqualTo(72.5m));
            Assert.That(trip.AirTemperature, Is.EqualTo(75.0m));
            Assert.That(trip.Notes, Is.EqualTo("Great fishing day"));
            Assert.That(trip.TripDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(trip.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(trip.Catches, Is.Not.Null);
        });
    }

    [Test]
    public void Trip_DefaultValues_AreSetCorrectly()
    {
        // Act
        var trip = new Trip();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(trip.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(trip.Catches, Is.Not.Null);
            Assert.That(trip.Catches.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void GetDurationInHours_WithEndTime_ReturnsCorrectDuration()
    {
        // Arrange
        var startTime = new DateTime(2024, 6, 15, 8, 0, 0);
        var endTime = new DateTime(2024, 6, 15, 14, 30, 0);
        var trip = new Trip
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = trip.GetDurationInHours();

        // Assert
        Assert.That(duration, Is.EqualTo(6.5m).Within(0.01m));
    }

    [Test]
    public void GetDurationInHours_WithoutEndTime_ReturnsNull()
    {
        // Arrange
        var trip = new Trip
        {
            StartTime = DateTime.UtcNow,
            EndTime = null
        };

        // Act
        var duration = trip.GetDurationInHours();

        // Assert
        Assert.That(duration, Is.Null);
    }

    [Test]
    public void GetDurationInHours_WithZeroDuration_ReturnsZero()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var trip = new Trip
        {
            StartTime = startTime,
            EndTime = startTime
        };

        // Act
        var duration = trip.GetDurationInHours();

        // Assert
        Assert.That(duration, Is.EqualTo(0m));
    }

    [Test]
    public void GetDurationInHours_WithFractionalHours_ReturnsCorrectDecimal()
    {
        // Arrange
        var startTime = new DateTime(2024, 6, 15, 8, 0, 0);
        var endTime = new DateTime(2024, 6, 15, 8, 45, 0); // 45 minutes = 0.75 hours
        var trip = new Trip
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = trip.GetDurationInHours();

        // Assert
        Assert.That(duration, Is.EqualTo(0.75m).Within(0.01m));
    }

    [Test]
    public void GetTotalCatchCount_WithNoCatches_ReturnsZero()
    {
        // Arrange
        var trip = new Trip();

        // Act
        var count = trip.GetTotalCatchCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalCatchCount_WithMultipleCatches_ReturnsCorrectCount()
    {
        // Arrange
        var trip = new Trip
        {
            Catches = new List<Catch>
            {
                new Catch { CatchId = Guid.NewGuid(), Species = FishSpecies.Bass },
                new Catch { CatchId = Guid.NewGuid(), Species = FishSpecies.Trout },
                new Catch { CatchId = Guid.NewGuid(), Species = FishSpecies.Walleye },
                new Catch { CatchId = Guid.NewGuid(), Species = FishSpecies.Pike }
            }
        };

        // Act
        var count = trip.GetTotalCatchCount();

        // Assert
        Assert.That(count, Is.EqualTo(4));
    }

    [Test]
    public void GetTotalCatchCount_WithNullCatches_ReturnsZero()
    {
        // Arrange
        var trip = new Trip { Catches = null! };

        // Act
        var count = trip.GetTotalCatchCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Trip_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var trip = new Trip
        {
            SpotId = null,
            EndTime = null,
            WeatherConditions = null,
            WaterTemperature = null,
            AirTemperature = null,
            Notes = null,
            Spot = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.SpotId, Is.Null);
            Assert.That(trip.EndTime, Is.Null);
            Assert.That(trip.WeatherConditions, Is.Null);
            Assert.That(trip.WaterTemperature, Is.Null);
            Assert.That(trip.AirTemperature, Is.Null);
            Assert.That(trip.Notes, Is.Null);
            Assert.That(trip.Spot, Is.Null);
        });
    }

    [Test]
    public void Trip_WithNegativeTemperatures_IsValid()
    {
        // Arrange & Act
        var trip = new Trip
        {
            WaterTemperature = -5.5m,
            AirTemperature = -10.0m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.WaterTemperature, Is.EqualTo(-5.5m));
            Assert.That(trip.AirTemperature, Is.EqualTo(-10.0m));
        });
    }

    [Test]
    public void Trip_WithHighTemperatures_IsValid()
    {
        // Arrange & Act
        var trip = new Trip
        {
            WaterTemperature = 95.0m,
            AirTemperature = 105.5m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.WaterTemperature, Is.EqualTo(95.0m));
            Assert.That(trip.AirTemperature, Is.EqualTo(105.5m));
        });
    }
}
