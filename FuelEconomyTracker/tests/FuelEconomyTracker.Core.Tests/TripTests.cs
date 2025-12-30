// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;

namespace FuelEconomyTracker.Core.Tests;

public class TripTests
{
    [Test]
    public void Trip_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var startDate = new DateTime(2024, 6, 15, 8, 0, 0);
        var endDate = new DateTime(2024, 6, 15, 12, 0, 0);
        var startOdometer = 50000m;
        var endOdometer = 50150m;

        // Act
        var trip = new Trip
        {
            TripId = tripId,
            VehicleId = vehicleId,
            StartDate = startDate,
            EndDate = endDate,
            StartOdometer = startOdometer,
            EndOdometer = endOdometer,
            Distance = 150m,
            Purpose = "Business meeting",
            StartLocation = "Home",
            EndLocation = "Office",
            AverageMPG = 28.5m,
            Notes = "Highway route"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.EqualTo(tripId));
            Assert.That(trip.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
            Assert.That(trip.StartOdometer, Is.EqualTo(startOdometer));
            Assert.That(trip.EndOdometer, Is.EqualTo(endOdometer));
            Assert.That(trip.Distance, Is.EqualTo(150m));
            Assert.That(trip.Purpose, Is.EqualTo("Business meeting"));
            Assert.That(trip.StartLocation, Is.EqualTo("Home"));
            Assert.That(trip.EndLocation, Is.EqualTo("Office"));
            Assert.That(trip.AverageMPG, Is.EqualTo(28.5m));
            Assert.That(trip.Notes, Is.EqualTo("Highway route"));
        });
    }

    [Test]
    public void CompleteTrip_SetsEndDateAndOdometerAndCalculatesDistance()
    {
        // Arrange
        var trip = new Trip
        {
            StartOdometer = 50000m
        };
        var endDate = DateTime.UtcNow;
        var endOdometer = 50200m;

        // Act
        trip.CompleteTrip(endDate, endOdometer);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
            Assert.That(trip.EndOdometer, Is.EqualTo(endOdometer));
            Assert.That(trip.Distance, Is.EqualTo(200m));
        });
    }

    [Test]
    public void CalculateDistance_WithValidEndOdometer_SetsDistanceCorrectly()
    {
        // Arrange
        var trip = new Trip
        {
            StartOdometer = 10000m,
            EndOdometer = 10250m
        };

        // Act
        trip.CalculateDistance();

        // Assert
        Assert.That(trip.Distance, Is.EqualTo(250m));
    }

    [Test]
    public void CalculateDistance_WithoutEndOdometer_DoesNotSetDistance()
    {
        // Arrange
        var trip = new Trip
        {
            StartOdometer = 10000m,
            EndOdometer = null,
            Distance = null
        };

        // Act
        trip.CalculateDistance();

        // Assert
        Assert.That(trip.Distance, Is.Null);
    }

    [Test]
    public void CalculateDistance_WhenEndOdometerLessThanStart_DoesNotSetDistance()
    {
        // Arrange
        var trip = new Trip
        {
            StartOdometer = 10000m,
            EndOdometer = 9500m,
            Distance = null
        };

        // Act
        trip.CalculateDistance();

        // Assert
        Assert.That(trip.Distance, Is.Null);
    }

    [Test]
    public void CalculateDistance_WhenEndOdometerEqualsStart_DoesNotSetDistance()
    {
        // Arrange
        var trip = new Trip
        {
            StartOdometer = 10000m,
            EndOdometer = 10000m,
            Distance = null
        };

        // Act
        trip.CalculateDistance();

        // Assert
        Assert.That(trip.Distance, Is.Null);
    }

    [Test]
    public void SetAverageMPG_SetsTheMPGValue()
    {
        // Arrange
        var trip = new Trip();
        var mpg = 32.5m;

        // Act
        trip.SetAverageMPG(mpg);

        // Assert
        Assert.That(trip.AverageMPG, Is.EqualTo(32.5m));
    }

    [Test]
    public void SetAverageMPG_CanBeSetToZero()
    {
        // Arrange
        var trip = new Trip();

        // Act
        trip.SetAverageMPG(0m);

        // Assert
        Assert.That(trip.AverageMPG, Is.EqualTo(0m));
    }

    [Test]
    public void SetAverageMPG_CanBeUpdatedMultipleTimes()
    {
        // Arrange
        var trip = new Trip();

        // Act
        trip.SetAverageMPG(25.0m);
        trip.SetAverageMPG(30.5m);

        // Assert
        Assert.That(trip.AverageMPG, Is.EqualTo(30.5m));
    }

    [Test]
    public void Trip_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var trip = new Trip
        {
            EndDate = null,
            EndOdometer = null,
            Distance = null,
            Purpose = null,
            StartLocation = null,
            EndLocation = null,
            AverageMPG = null,
            Notes = null,
            Vehicle = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.EndDate, Is.Null);
            Assert.That(trip.EndOdometer, Is.Null);
            Assert.That(trip.Distance, Is.Null);
            Assert.That(trip.Purpose, Is.Null);
            Assert.That(trip.StartLocation, Is.Null);
            Assert.That(trip.EndLocation, Is.Null);
            Assert.That(trip.AverageMPG, Is.Null);
            Assert.That(trip.Notes, Is.Null);
            Assert.That(trip.Vehicle, Is.Null);
        });
    }

    [Test]
    public void Trip_StartOdometer_CanBeZero()
    {
        // Arrange & Act
        var trip = new Trip { StartOdometer = 0m };

        // Assert
        Assert.That(trip.StartOdometer, Is.EqualTo(0m));
    }

    [Test]
    public void Trip_StartOdometer_CanBeLargeValue()
    {
        // Arrange & Act
        var trip = new Trip { StartOdometer = 250000m };

        // Assert
        Assert.That(trip.StartOdometer, Is.EqualTo(250000m));
    }

    [Test]
    public void Trip_Distance_CanBeSetWithDecimalPlaces()
    {
        // Arrange & Act
        var trip = new Trip { Distance = 123.45m };

        // Assert
        Assert.That(trip.Distance, Is.EqualTo(123.45m));
    }
}
