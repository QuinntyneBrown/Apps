// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;

namespace FuelEconomyTracker.Core.Tests;

public class EfficiencyReportTests
{
    [Test]
    public void EfficiencyReport_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var startDate = new DateTime(2024, 6, 1);
        var endDate = new DateTime(2024, 6, 30);

        // Act
        var report = new EfficiencyReport
        {
            EfficiencyReportId = reportId,
            VehicleId = vehicleId,
            StartDate = startDate,
            EndDate = endDate,
            TotalMiles = 1200m,
            TotalGallons = 40m,
            AverageMPG = 30m,
            TotalFuelCost = 150m,
            CostPerMile = 0.125m,
            NumberOfFillUps = 4,
            BestMPG = 35m,
            WorstMPG = 25m,
            Notes = "Good month"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.EfficiencyReportId, Is.EqualTo(reportId));
            Assert.That(report.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(report.StartDate, Is.EqualTo(startDate));
            Assert.That(report.EndDate, Is.EqualTo(endDate));
            Assert.That(report.TotalMiles, Is.EqualTo(1200m));
            Assert.That(report.TotalGallons, Is.EqualTo(40m));
            Assert.That(report.AverageMPG, Is.EqualTo(30m));
            Assert.That(report.TotalFuelCost, Is.EqualTo(150m));
            Assert.That(report.CostPerMile, Is.EqualTo(0.125m));
            Assert.That(report.NumberOfFillUps, Is.EqualTo(4));
            Assert.That(report.BestMPG, Is.EqualTo(35m));
            Assert.That(report.WorstMPG, Is.EqualTo(25m));
            Assert.That(report.Notes, Is.EqualTo("Good month"));
        });
    }

    [Test]
    public void CalculateAverageMPG_WithValidGallons_CalculatesCorrectly()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 300m,
            TotalGallons = 10m
        };

        // Act
        report.CalculateAverageMPG();

        // Assert
        Assert.That(report.AverageMPG, Is.EqualTo(30m));
    }

    [Test]
    public void CalculateAverageMPG_RoundsToTwoDecimalPlaces()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 333m,
            TotalGallons = 12m
        };

        // Act
        report.CalculateAverageMPG();

        // Assert
        Assert.That(report.AverageMPG, Is.EqualTo(27.75m));
    }

    [Test]
    public void CalculateAverageMPG_WithZeroGallons_DoesNotCalculate()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 300m,
            TotalGallons = 0m,
            AverageMPG = 0m
        };

        // Act
        report.CalculateAverageMPG();

        // Assert
        Assert.That(report.AverageMPG, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateCostPerMile_WithValidMiles_CalculatesCorrectly()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalFuelCost = 100m,
            TotalMiles = 800m
        };

        // Act
        report.CalculateCostPerMile();

        // Assert
        Assert.That(report.CostPerMile, Is.EqualTo(0.125m));
    }

    [Test]
    public void CalculateCostPerMile_RoundsToFourDecimalPlaces()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalFuelCost = 123.45m,
            TotalMiles = 789m
        };

        // Act
        report.CalculateCostPerMile();

        // Assert
        Assert.That(report.CostPerMile, Is.EqualTo(0.1565m));
    }

    [Test]
    public void CalculateCostPerMile_WithZeroMiles_DoesNotCalculate()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalFuelCost = 100m,
            TotalMiles = 0m,
            CostPerMile = 0m
        };

        // Act
        report.CalculateCostPerMile();

        // Assert
        Assert.That(report.CostPerMile, Is.EqualTo(0m));
    }

    [Test]
    public void GenerateReport_WithFillUps_CalculatesAllMetrics()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 600m
        };
        var fillUps = new List<FillUp>
        {
            new FillUp { Gallons = 10m, TotalCost = 35m, MilesPerGallon = 28m },
            new FillUp { Gallons = 12m, TotalCost = 42m, MilesPerGallon = 32m },
            new FillUp { Gallons = 8m, TotalCost = 28m, MilesPerGallon = 25m }
        };

        // Act
        report.GenerateReport(fillUps);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.NumberOfFillUps, Is.EqualTo(3));
            Assert.That(report.TotalGallons, Is.EqualTo(30m));
            Assert.That(report.TotalFuelCost, Is.EqualTo(105m));
            Assert.That(report.BestMPG, Is.EqualTo(32m));
            Assert.That(report.WorstMPG, Is.EqualTo(25m));
            Assert.That(report.AverageMPG, Is.EqualTo(20m));
            Assert.That(report.CostPerMile, Is.EqualTo(0.175m));
        });
    }

    [Test]
    public void GenerateReport_WithNoFillUps_SetsZeroValues()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 0m
        };
        var fillUps = new List<FillUp>();

        // Act
        report.GenerateReport(fillUps);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.NumberOfFillUps, Is.EqualTo(0));
            Assert.That(report.TotalGallons, Is.EqualTo(0m));
            Assert.That(report.TotalFuelCost, Is.EqualTo(0m));
            Assert.That(report.BestMPG, Is.Null);
            Assert.That(report.WorstMPG, Is.Null);
        });
    }

    [Test]
    public void GenerateReport_WithFillUpsWithoutMPG_DoesNotSetBestWorst()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 300m
        };
        var fillUps = new List<FillUp>
        {
            new FillUp { Gallons = 10m, TotalCost = 35m, MilesPerGallon = null },
            new FillUp { Gallons = 12m, TotalCost = 42m, MilesPerGallon = null }
        };

        // Act
        report.GenerateReport(fillUps);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.BestMPG, Is.Null);
            Assert.That(report.WorstMPG, Is.Null);
            Assert.That(report.NumberOfFillUps, Is.EqualTo(2));
            Assert.That(report.TotalGallons, Is.EqualTo(22m));
        });
    }

    [Test]
    public void GenerateReport_WithMixedFillUps_CalculatesBestWorstFromThoseWithMPG()
    {
        // Arrange
        var report = new EfficiencyReport
        {
            TotalMiles = 600m
        };
        var fillUps = new List<FillUp>
        {
            new FillUp { Gallons = 10m, TotalCost = 35m, MilesPerGallon = 28m },
            new FillUp { Gallons = 12m, TotalCost = 42m, MilesPerGallon = null },
            new FillUp { Gallons = 8m, TotalCost = 28m, MilesPerGallon = 32m }
        };

        // Act
        report.GenerateReport(fillUps);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.BestMPG, Is.EqualTo(32m));
            Assert.That(report.WorstMPG, Is.EqualTo(28m));
        });
    }

    [Test]
    public void EfficiencyReport_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var report = new EfficiencyReport
        {
            BestMPG = null,
            WorstMPG = null,
            Notes = null,
            Vehicle = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.BestMPG, Is.Null);
            Assert.That(report.WorstMPG, Is.Null);
            Assert.That(report.Notes, Is.Null);
            Assert.That(report.Vehicle, Is.Null);
        });
    }

    [Test]
    public void EfficiencyReport_TotalMiles_CanBeZero()
    {
        // Arrange & Act
        var report = new EfficiencyReport { TotalMiles = 0m };

        // Assert
        Assert.That(report.TotalMiles, Is.EqualTo(0m));
    }

    [Test]
    public void EfficiencyReport_TotalGallons_CanBeZero()
    {
        // Arrange & Act
        var report = new EfficiencyReport { TotalGallons = 0m };

        // Assert
        Assert.That(report.TotalGallons, Is.EqualTo(0m));
    }

    [Test]
    public void EfficiencyReport_NumberOfFillUps_CanBeZero()
    {
        // Arrange & Act
        var report = new EfficiencyReport { NumberOfFillUps = 0 };

        // Assert
        Assert.That(report.NumberOfFillUps, Is.EqualTo(0));
    }

    [Test]
    public void EfficiencyReport_NumberOfFillUps_CanBeLargeNumber()
    {
        // Arrange & Act
        var report = new EfficiencyReport { NumberOfFillUps = 100 };

        // Assert
        Assert.That(report.NumberOfFillUps, Is.EqualTo(100));
    }
}
