// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;

namespace FuelEconomyTracker.Core.Tests;

public class VehicleTests
{
    [Test]
    public void Vehicle_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var make = "Toyota";
        var model = "Camry";
        var year = 2024;
        var vin = "1HGCM82633A123456";
        var licensePlate = "ABC-1234";

        // Act
        var vehicle = new Vehicle
        {
            VehicleId = vehicleId,
            Make = make,
            Model = model,
            Year = year,
            VIN = vin,
            LicensePlate = licensePlate,
            TankCapacity = 15.8m,
            EPACityMPG = 28m,
            EPAHighwayMPG = 39m,
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(vehicle.Make, Is.EqualTo(make));
            Assert.That(vehicle.Model, Is.EqualTo(model));
            Assert.That(vehicle.Year, Is.EqualTo(year));
            Assert.That(vehicle.VIN, Is.EqualTo(vin));
            Assert.That(vehicle.LicensePlate, Is.EqualTo(licensePlate));
            Assert.That(vehicle.TankCapacity, Is.EqualTo(15.8m));
            Assert.That(vehicle.EPACityMPG, Is.EqualTo(28m));
            Assert.That(vehicle.EPAHighwayMPG, Is.EqualTo(39m));
            Assert.That(vehicle.IsActive, Is.True);
            Assert.That(vehicle.FillUps, Is.Not.Null);
            Assert.That(vehicle.Trips, Is.Not.Null);
            Assert.That(vehicle.EfficiencyReports, Is.Not.Null);
        });
    }

    [Test]
    public void Vehicle_DefaultValues_AreSetCorrectly()
    {
        // Act
        var vehicle = new Vehicle();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.Make, Is.EqualTo(string.Empty));
            Assert.That(vehicle.Model, Is.EqualTo(string.Empty));
            Assert.That(vehicle.IsActive, Is.True);
            Assert.That(vehicle.FillUps, Is.Not.Null);
            Assert.That(vehicle.FillUps.Count, Is.EqualTo(0));
            Assert.That(vehicle.Trips, Is.Not.Null);
            Assert.That(vehicle.Trips.Count, Is.EqualTo(0));
            Assert.That(vehicle.EfficiencyReports, Is.Not.Null);
            Assert.That(vehicle.EfficiencyReports.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void CalculateOverallMPG_WithNoFillUps_ReturnsNull()
    {
        // Arrange
        var vehicle = new Vehicle();

        // Act
        var mpg = vehicle.CalculateOverallMPG();

        // Assert
        Assert.That(mpg, Is.Null);
    }

    [Test]
    public void CalculateOverallMPG_WithFillUpsWithoutMPG_ReturnsNull()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            FillUps = new List<FillUp>
            {
                new FillUp { MilesPerGallon = null },
                new FillUp { MilesPerGallon = null }
            }
        };

        // Act
        var mpg = vehicle.CalculateOverallMPG();

        // Assert
        Assert.That(mpg, Is.Null);
    }

    [Test]
    public void CalculateOverallMPG_WithMultipleFillUps_ReturnsAverage()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            FillUps = new List<FillUp>
            {
                new FillUp { MilesPerGallon = 25.0m },
                new FillUp { MilesPerGallon = 30.0m },
                new FillUp { MilesPerGallon = 27.5m }
            }
        };

        // Act
        var mpg = vehicle.CalculateOverallMPG();

        // Assert
        Assert.That(mpg, Is.EqualTo(27.5m));
    }

    [Test]
    public void CalculateOverallMPG_WithMixedFillUps_CalculatesOnlyThoseWithMPG()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            FillUps = new List<FillUp>
            {
                new FillUp { MilesPerGallon = 28.0m },
                new FillUp { MilesPerGallon = null },
                new FillUp { MilesPerGallon = 32.0m }
            }
        };

        // Act
        var mpg = vehicle.CalculateOverallMPG();

        // Assert
        Assert.That(mpg, Is.EqualTo(30.0m));
    }

    [Test]
    public void CalculateOverallMPG_RoundsToTwoDecimalPlaces()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            FillUps = new List<FillUp>
            {
                new FillUp { MilesPerGallon = 25.123m },
                new FillUp { MilesPerGallon = 30.789m }
            }
        };

        // Act
        var mpg = vehicle.CalculateOverallMPG();

        // Assert
        Assert.That(mpg, Is.EqualTo(27.96m));
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var vehicle = new Vehicle { IsActive = true };

        // Act
        vehicle.Deactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.False);
    }

    [Test]
    public void Deactivate_WhenAlreadyInactive_RemainsInactive()
    {
        // Arrange
        var vehicle = new Vehicle { IsActive = false };

        // Act
        vehicle.Deactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_SetsIsActiveToTrue()
    {
        // Arrange
        var vehicle = new Vehicle { IsActive = false };

        // Act
        vehicle.Reactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void Reactivate_WhenAlreadyActive_RemainsActive()
    {
        // Arrange
        var vehicle = new Vehicle { IsActive = true };

        // Act
        vehicle.Reactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void Vehicle_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VIN = null,
            LicensePlate = null,
            TankCapacity = null,
            EPACityMPG = null,
            EPAHighwayMPG = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VIN, Is.Null);
            Assert.That(vehicle.LicensePlate, Is.Null);
            Assert.That(vehicle.TankCapacity, Is.Null);
            Assert.That(vehicle.EPACityMPG, Is.Null);
            Assert.That(vehicle.EPAHighwayMPG, Is.Null);
        });
    }

    [Test]
    public void Vehicle_Year_CanBeOldYear()
    {
        // Arrange & Act
        var vehicle = new Vehicle { Year = 1990 };

        // Assert
        Assert.That(vehicle.Year, Is.EqualTo(1990));
    }

    [Test]
    public void Vehicle_Year_CanBeRecentYear()
    {
        // Arrange & Act
        var vehicle = new Vehicle { Year = 2024 };

        // Assert
        Assert.That(vehicle.Year, Is.EqualTo(2024));
    }

    [Test]
    public void Vehicle_TankCapacity_CanBeSmallValue()
    {
        // Arrange & Act
        var vehicle = new Vehicle { TankCapacity = 10.5m };

        // Assert
        Assert.That(vehicle.TankCapacity, Is.EqualTo(10.5m));
    }

    [Test]
    public void Vehicle_TankCapacity_CanBeLargeValue()
    {
        // Arrange & Act
        var vehicle = new Vehicle { TankCapacity = 35.0m };

        // Assert
        Assert.That(vehicle.TankCapacity, Is.EqualTo(35.0m));
    }

    [Test]
    public void Vehicle_EPAMPGValues_CanBeSet()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            EPACityMPG = 25.5m,
            EPAHighwayMPG = 35.8m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.EPACityMPG, Is.EqualTo(25.5m));
            Assert.That(vehicle.EPAHighwayMPG, Is.EqualTo(35.8m));
        });
    }
}
