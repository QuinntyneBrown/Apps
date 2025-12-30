// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core.Tests;

public class VehicleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesVehicle()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var make = "Tesla";
        var model = "Model 3";
        var year = 2022;
        var trim = "Long Range";
        var vin = "5YJ3E1EA1KF123456";
        var currentMileage = 15000m;
        var purchasePrice = 45000m;
        var purchaseDate = new DateTime(2022, 3, 15);
        var color = "Midnight Silver";
        var interiorType = "Black Premium";
        var engineType = "Electric";
        var transmission = "Automatic";

        // Act
        var vehicle = new Vehicle
        {
            VehicleId = vehicleId,
            Make = make,
            Model = model,
            Year = year,
            Trim = trim,
            VIN = vin,
            CurrentMileage = currentMileage,
            PurchasePrice = purchasePrice,
            PurchaseDate = purchaseDate,
            Color = color,
            InteriorType = interiorType,
            EngineType = engineType,
            Transmission = transmission
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(vehicle.Make, Is.EqualTo(make));
            Assert.That(vehicle.Model, Is.EqualTo(model));
            Assert.That(vehicle.Year, Is.EqualTo(year));
            Assert.That(vehicle.Trim, Is.EqualTo(trim));
            Assert.That(vehicle.VIN, Is.EqualTo(vin));
            Assert.That(vehicle.CurrentMileage, Is.EqualTo(currentMileage));
            Assert.That(vehicle.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(vehicle.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(vehicle.Color, Is.EqualTo(color));
            Assert.That(vehicle.InteriorType, Is.EqualTo(interiorType));
            Assert.That(vehicle.EngineType, Is.EqualTo(engineType));
            Assert.That(vehicle.Transmission, Is.EqualTo(transmission));
            Assert.That(vehicle.IsCurrentlyOwned, Is.True);
        });
    }

    [Test]
    public void UpdateMileage_ValidMileage_UpdatesCurrentMileage()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "BMW",
            Model = "M3",
            Year = 2021,
            CurrentMileage = 10000m
        };

        // Act
        vehicle.UpdateMileage(15000m);

        // Assert
        Assert.That(vehicle.CurrentMileage, Is.EqualTo(15000m));
    }

    [Test]
    public void UpdateMileage_LowerMileage_ThrowsArgumentException()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Porsche",
            Model = "911",
            Year = 2020,
            CurrentMileage = 20000m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => vehicle.UpdateMileage(15000m));
        Assert.That(ex.Message, Does.Contain("New mileage cannot be less than current mileage"));
    }

    [Test]
    public void MarkAsSold_SetsIsCurrentlyOwnedToFalse()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Mercedes",
            Model = "C-Class",
            Year = 2019,
            CurrentMileage = 30000m,
            IsCurrentlyOwned = true
        };

        // Act
        vehicle.MarkAsSold();

        // Assert
        Assert.That(vehicle.IsCurrentlyOwned, Is.False);
    }

    [Test]
    public void GetMostRecentAssessment_WithMultipleAssessments_ReturnsLatest()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Audi",
            Model = "A4",
            Year = 2020,
            CurrentMileage = 25000m
        };

        var assessment1 = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            AssessmentDate = new DateTime(2023, 1, 1),
            EstimatedValue = 30000m,
            MileageAtAssessment = 20000m,
            ConditionGrade = ConditionGrade.Good
        };

        var assessment2 = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            AssessmentDate = new DateTime(2024, 1, 1),
            EstimatedValue = 28000m,
            MileageAtAssessment = 25000m,
            ConditionGrade = ConditionGrade.Good
        };

        vehicle.ValueAssessments.Add(assessment1);
        vehicle.ValueAssessments.Add(assessment2);

        // Act
        var mostRecent = vehicle.GetMostRecentAssessment();

        // Assert
        Assert.That(mostRecent, Is.Not.Null);
        Assert.That(mostRecent.AssessmentDate, Is.EqualTo(new DateTime(2024, 1, 1)));
    }

    [Test]
    public void GetMostRecentAssessment_WithNoAssessments_ReturnsNull()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Lexus",
            Model = "RX",
            Year = 2021,
            CurrentMileage = 15000m
        };

        // Act
        var mostRecent = vehicle.GetMostRecentAssessment();

        // Assert
        Assert.That(mostRecent, Is.Null);
    }

    [Test]
    public void IsCurrentlyOwned_DefaultsToTrue()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Volvo",
            Model = "XC90",
            Year = 2022,
            CurrentMileage = 5000m
        };

        // Assert
        Assert.That(vehicle.IsCurrentlyOwned, Is.True);
    }

    [Test]
    public void ValueAssessments_InitializesAsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Jaguar",
            Model = "F-Type",
            Year = 2021,
            CurrentMileage = 8000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.ValueAssessments, Is.Not.Null);
            Assert.That(vehicle.ValueAssessments, Is.Empty);
        });
    }

    [Test]
    public void MarketComparisons_InitializesAsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Maserati",
            Model = "Ghibli",
            Year = 2020,
            CurrentMileage = 12000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.MarketComparisons, Is.Not.Null);
            Assert.That(vehicle.MarketComparisons, Is.Empty);
        });
    }

    [Test]
    public void UpdateMileage_SameMileage_DoesNotThrow()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Land Rover",
            Model = "Range Rover",
            Year = 2021,
            CurrentMileage = 20000m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => vehicle.UpdateMileage(20000m));
        Assert.That(vehicle.CurrentMileage, Is.EqualTo(20000m));
    }

    [Test]
    public void Vehicle_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Cadillac",
            Model = "Escalade",
            Year = 2022,
            CurrentMileage = 10000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.Trim, Is.Null);
            Assert.That(vehicle.VIN, Is.Null);
            Assert.That(vehicle.PurchasePrice, Is.Null);
            Assert.That(vehicle.PurchaseDate, Is.Null);
            Assert.That(vehicle.Color, Is.Null);
            Assert.That(vehicle.InteriorType, Is.Null);
            Assert.That(vehicle.EngineType, Is.Null);
            Assert.That(vehicle.Transmission, Is.Null);
            Assert.That(vehicle.Notes, Is.Null);
        });
    }
}
