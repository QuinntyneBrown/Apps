// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core.Tests;

public class MarketComparisonTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMarketComparison()
    {
        // Arrange
        var comparisonId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var comparisonDate = new DateTime(2024, 3, 15);
        var listingSource = "Autotrader";
        var comparableYear = 2020;
        var comparableMake = "BMW";
        var comparableModel = "M3";
        var comparableTrim = "Competition";
        var comparableMileage = 25000m;
        var askingPrice = 52000m;
        var location = "Los Angeles, CA";
        var condition = "Excellent";
        var listingUrl = "https://example.com/listing";
        var daysOnMarket = 15;

        // Act
        var comparison = new MarketComparison
        {
            MarketComparisonId = comparisonId,
            VehicleId = vehicleId,
            ComparisonDate = comparisonDate,
            ListingSource = listingSource,
            ComparableYear = comparableYear,
            ComparableMake = comparableMake,
            ComparableModel = comparableModel,
            ComparableTrim = comparableTrim,
            ComparableMileage = comparableMileage,
            AskingPrice = askingPrice,
            Location = location,
            Condition = condition,
            ListingUrl = listingUrl,
            DaysOnMarket = daysOnMarket
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(comparison.MarketComparisonId, Is.EqualTo(comparisonId));
            Assert.That(comparison.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(comparison.ComparisonDate, Is.EqualTo(comparisonDate));
            Assert.That(comparison.ListingSource, Is.EqualTo(listingSource));
            Assert.That(comparison.ComparableYear, Is.EqualTo(comparableYear));
            Assert.That(comparison.ComparableMake, Is.EqualTo(comparableMake));
            Assert.That(comparison.ComparableModel, Is.EqualTo(comparableModel));
            Assert.That(comparison.ComparableTrim, Is.EqualTo(comparableTrim));
            Assert.That(comparison.ComparableMileage, Is.EqualTo(comparableMileage));
            Assert.That(comparison.AskingPrice, Is.EqualTo(askingPrice));
            Assert.That(comparison.Location, Is.EqualTo(location));
            Assert.That(comparison.Condition, Is.EqualTo(condition));
            Assert.That(comparison.ListingUrl, Is.EqualTo(listingUrl));
            Assert.That(comparison.DaysOnMarket, Is.EqualTo(daysOnMarket));
            Assert.That(comparison.IsActive, Is.True);
        });
    }

    [Test]
    public void CalculatePriceDifference_HigherAskingPrice_ReturnsPositiveDifference()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "CarGurus",
            ComparableYear = 2021,
            ComparableMake = "Tesla",
            ComparableModel = "Model 3",
            ComparableMileage = 15000m,
            AskingPrice = 45000m
        };

        // Act
        var difference = comparison.CalculatePriceDifference(40000m);

        // Assert
        Assert.That(difference, Is.EqualTo(5000m));
    }

    [Test]
    public void CalculatePriceDifference_LowerAskingPrice_ReturnsNegativeDifference()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "eBay Motors",
            ComparableYear = 2019,
            ComparableMake = "Honda",
            ComparableModel = "Civic",
            ComparableMileage = 30000m,
            AskingPrice = 18000m
        };

        // Act
        var difference = comparison.CalculatePriceDifference(20000m);

        // Assert
        Assert.That(difference, Is.EqualTo(-2000m));
    }

    [Test]
    public void CalculatePricePerMile_ValidMileage_ReturnsCorrectValue()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "Autotrader",
            ComparableYear = 2020,
            ComparableMake = "Audi",
            ComparableModel = "A4",
            ComparableMileage = 25000m,
            AskingPrice = 30000m
        };

        // Act
        var pricePerMile = comparison.CalculatePricePerMile();

        // Assert
        Assert.That(pricePerMile, Is.EqualTo(1.20m));
    }

    [Test]
    public void CalculatePricePerMile_ZeroMileage_ReturnsZero()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "CarMax",
            ComparableYear = 2024,
            ComparableMake = "Ferrari",
            ComparableModel = "F8",
            ComparableMileage = 0m,
            AskingPrice = 350000m
        };

        // Act
        var pricePerMile = comparison.CalculatePricePerMile();

        // Assert
        Assert.That(pricePerMile, Is.EqualTo(0m));
    }

    [Test]
    public void MarkAsInactive_SetsIsActiveToFalse()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "Craigslist",
            ComparableYear = 2018,
            ComparableMake = "Ford",
            ComparableModel = "Mustang",
            ComparableMileage = 40000m,
            AskingPrice = 28000m,
            IsActive = true
        };

        // Act
        comparison.MarkAsInactive();

        // Assert
        Assert.That(comparison.IsActive, Is.False);
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "Vroom",
            ComparableYear = 2021,
            ComparableMake = "Mazda",
            ComparableModel = "CX-5",
            ComparableMileage = 20000m,
            AskingPrice = 27000m
        };

        // Assert
        Assert.That(comparison.IsActive, Is.True);
    }

    [Test]
    public void MarketComparison_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "AutoTrader",
            ComparableYear = 2020,
            ComparableMake = "Toyota",
            ComparableModel = "Camry",
            ComparableMileage = 15000m,
            AskingPrice = 25000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(comparison.ComparableTrim, Is.Null);
            Assert.That(comparison.Location, Is.Null);
            Assert.That(comparison.Condition, Is.Null);
            Assert.That(comparison.ListingUrl, Is.Null);
            Assert.That(comparison.DaysOnMarket, Is.Null);
            Assert.That(comparison.Notes, Is.Null);
        });
    }
}
