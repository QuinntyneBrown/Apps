// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core.Tests;

public class TaxReportTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var report = new TaxReport
        {
            TaxReportId = Guid.NewGuid(),
            TaxYear = 2024,
            TotalCashDonations = 1500.00m,
            TotalNonCashDonations = 500.00m,
            TotalDeductibleAmount = 2000.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.TaxReportId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(report.TaxYear, Is.EqualTo(2024));
            Assert.That(report.TotalCashDonations, Is.EqualTo(1500.00m));
            Assert.That(report.TotalNonCashDonations, Is.EqualTo(500.00m));
            Assert.That(report.TotalDeductibleAmount, Is.EqualTo(2000.00m));
            Assert.That(report.GeneratedDate, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void CalculateTotals_SumsCashAndNonCashDonations()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 1200.00m,
            TotalNonCashDonations = 800.00m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(2000.00m));
    }

    [Test]
    public void CalculateTotals_WithOnlyCashDonations_CalculatesCorrectly()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 5000.00m,
            TotalNonCashDonations = 0m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(5000.00m));
    }

    [Test]
    public void CalculateTotals_WithOnlyNonCashDonations_CalculatesCorrectly()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 0m,
            TotalNonCashDonations = 3000.00m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(3000.00m));
    }

    [Test]
    public void CalculateTotals_WithNoDonations_ReturnsZero()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 0m,
            TotalNonCashDonations = 0m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(0m));
    }

    [Test]
    public void TaxReport_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var report = new TaxReport
        {
            Notes = "Total includes donations from January through December"
        };

        // Assert
        Assert.That(report.Notes, Is.EqualTo("Total includes donations from January through December"));
    }

    [Test]
    public void TaxReport_WithNullNotes_AllowsNull()
    {
        // Arrange & Act
        var report = new TaxReport
        {
            Notes = null
        };

        // Assert
        Assert.That(report.Notes, Is.Null);
    }

    [Test]
    public void TaxReport_WithSpecificYear_StoresYearCorrectly()
    {
        // Arrange & Act
        var report = new TaxReport
        {
            TaxYear = 2023
        };

        // Assert
        Assert.That(report.TaxYear, Is.EqualTo(2023));
    }

    [Test]
    public void TaxReport_DefaultGeneratedDate_IsUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var report = new TaxReport();
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.GeneratedDate, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(report.GeneratedDate, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void TaxReport_WithLargeAmounts_CalculatesCorrectly()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 50000.00m,
            TotalNonCashDonations = 25000.00m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(75000.00m));
    }

    [Test]
    public void TaxReport_WithDecimalAmounts_CalculatesCorrectly()
    {
        // Arrange
        var report = new TaxReport
        {
            TotalCashDonations = 1234.56m,
            TotalNonCashDonations = 789.44m
        };

        // Act
        report.CalculateTotals();

        // Assert
        Assert.That(report.TotalDeductibleAmount, Is.EqualTo(2024.00m));
    }
}
