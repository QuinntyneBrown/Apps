// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;

namespace FuelEconomyTracker.Core.Tests;

public class FillUpTests
{
    [Test]
    public void FillUp_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var fillUpId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var fillUpDate = new DateTime(2024, 6, 15);
        var odometer = 50000m;
        var gallons = 12.5m;
        var pricePerGallon = 3.75m;

        // Act
        var fillUp = new FillUp
        {
            FillUpId = fillUpId,
            VehicleId = vehicleId,
            FillUpDate = fillUpDate,
            Odometer = odometer,
            Gallons = gallons,
            PricePerGallon = pricePerGallon,
            TotalCost = 46.88m,
            IsFullTank = true,
            FuelGrade = "Premium",
            GasStation = "Shell",
            MilesPerGallon = 28.5m,
            Notes = "Highway fill-up"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fillUp.FillUpId, Is.EqualTo(fillUpId));
            Assert.That(fillUp.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(fillUp.FillUpDate, Is.EqualTo(fillUpDate));
            Assert.That(fillUp.Odometer, Is.EqualTo(odometer));
            Assert.That(fillUp.Gallons, Is.EqualTo(gallons));
            Assert.That(fillUp.PricePerGallon, Is.EqualTo(pricePerGallon));
            Assert.That(fillUp.TotalCost, Is.EqualTo(46.88m));
            Assert.That(fillUp.IsFullTank, Is.True);
            Assert.That(fillUp.FuelGrade, Is.EqualTo("Premium"));
            Assert.That(fillUp.GasStation, Is.EqualTo("Shell"));
            Assert.That(fillUp.MilesPerGallon, Is.EqualTo(28.5m));
            Assert.That(fillUp.Notes, Is.EqualTo("Highway fill-up"));
        });
    }

    [Test]
    public void FillUp_DefaultValues_AreSetCorrectly()
    {
        // Act
        var fillUp = new FillUp();

        // Assert
        Assert.That(fillUp.IsFullTank, Is.False);
    }

    [Test]
    public void CalculateTotalCost_MultipliesGallonsAndPricePerGallon()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Gallons = 10.0m,
            PricePerGallon = 3.50m
        };

        // Act
        fillUp.CalculateTotalCost();

        // Assert
        Assert.That(fillUp.TotalCost, Is.EqualTo(35.0m));
    }

    [Test]
    public void CalculateTotalCost_WithDecimalValues_CalculatesCorrectly()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Gallons = 12.345m,
            PricePerGallon = 3.789m
        };

        // Act
        fillUp.CalculateTotalCost();

        // Assert
        Assert.That(fillUp.TotalCost, Is.EqualTo(46.771605m));
    }

    [Test]
    public void CalculateTotalCost_WithZeroGallons_ReturnsZero()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Gallons = 0m,
            PricePerGallon = 3.50m
        };

        // Act
        fillUp.CalculateTotalCost();

        // Assert
        Assert.That(fillUp.TotalCost, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateMPG_WithValidPreviousOdometer_CalculatesMPGCorrectly()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Odometer = 50300m,
            Gallons = 10.0m
        };
        var previousOdometer = 50000m;

        // Act
        fillUp.CalculateMPG(previousOdometer);

        // Assert
        Assert.That(fillUp.MilesPerGallon, Is.EqualTo(30.0m));
    }

    [Test]
    public void CalculateMPG_RoundsToTwoDecimalPlaces()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Odometer = 50333m,
            Gallons = 12.0m
        };
        var previousOdometer = 50000m;

        // Act
        fillUp.CalculateMPG(previousOdometer);

        // Assert
        Assert.That(fillUp.MilesPerGallon, Is.EqualTo(27.75m));
    }

    [Test]
    public void CalculateMPG_WithZeroGallons_DoesNotSetMPG()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Odometer = 50300m,
            Gallons = 0m,
            MilesPerGallon = null
        };
        var previousOdometer = 50000m;

        // Act
        fillUp.CalculateMPG(previousOdometer);

        // Assert
        Assert.That(fillUp.MilesPerGallon, Is.Null);
    }

    [Test]
    public void CalculateMPG_WhenOdometerLessThanPrevious_DoesNotSetMPG()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Odometer = 50000m,
            Gallons = 10.0m,
            MilesPerGallon = null
        };
        var previousOdometer = 50300m;

        // Act
        fillUp.CalculateMPG(previousOdometer);

        // Assert
        Assert.That(fillUp.MilesPerGallon, Is.Null);
    }

    [Test]
    public void CalculateMPG_WhenOdometerEqualsPrevious_DoesNotSetMPG()
    {
        // Arrange
        var fillUp = new FillUp
        {
            Odometer = 50000m,
            Gallons = 10.0m,
            MilesPerGallon = null
        };
        var previousOdometer = 50000m;

        // Act
        fillUp.CalculateMPG(previousOdometer);

        // Assert
        Assert.That(fillUp.MilesPerGallon, Is.Null);
    }

    [Test]
    public void FillUp_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var fillUp = new FillUp
        {
            FuelGrade = null,
            GasStation = null,
            MilesPerGallon = null,
            Notes = null,
            Vehicle = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fillUp.FuelGrade, Is.Null);
            Assert.That(fillUp.GasStation, Is.Null);
            Assert.That(fillUp.MilesPerGallon, Is.Null);
            Assert.That(fillUp.Notes, Is.Null);
            Assert.That(fillUp.Vehicle, Is.Null);
        });
    }

    [Test]
    public void FillUp_Gallons_CanBeSmallValue()
    {
        // Arrange & Act
        var fillUp = new FillUp { Gallons = 0.5m };

        // Assert
        Assert.That(fillUp.Gallons, Is.EqualTo(0.5m));
    }

    [Test]
    public void FillUp_Gallons_CanBeLargeValue()
    {
        // Arrange & Act
        var fillUp = new FillUp { Gallons = 30.0m };

        // Assert
        Assert.That(fillUp.Gallons, Is.EqualTo(30.0m));
    }

    [Test]
    public void FillUp_PricePerGallon_CanBeSetToVariousValues()
    {
        // Arrange
        var prices = new[] { 2.99m, 3.45m, 4.89m, 5.25m };

        // Act & Assert
        foreach (var price in prices)
        {
            var fillUp = new FillUp { PricePerGallon = price };
            Assert.That(fillUp.PricePerGallon, Is.EqualTo(price));
        }
    }

    [Test]
    public void FillUp_IsFullTank_CanBeSetToTrue()
    {
        // Arrange & Act
        var fillUp = new FillUp { IsFullTank = true };

        // Assert
        Assert.That(fillUp.IsFullTank, Is.True);
    }

    [Test]
    public void FillUp_FuelGrade_CanBeSetToVariousGrades()
    {
        // Arrange
        var grades = new[] { "Regular", "Mid-Grade", "Premium", "Diesel" };

        // Act & Assert
        foreach (var grade in grades)
        {
            var fillUp = new FillUp { FuelGrade = grade };
            Assert.That(fillUp.FuelGrade, Is.EqualTo(grade));
        }
    }

    [Test]
    public void FillUp_Odometer_CanBeLargeValue()
    {
        // Arrange & Act
        var fillUp = new FillUp { Odometer = 250000m };

        // Assert
        Assert.That(fillUp.Odometer, Is.EqualTo(250000m));
    }
}
