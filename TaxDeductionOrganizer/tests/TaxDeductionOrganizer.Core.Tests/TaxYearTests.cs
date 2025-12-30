// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class TaxYearTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTaxYear()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var year = 2024;
        var notes = "Tax year notes";

        // Act
        var taxYear = new TaxYear
        {
            TaxYearId = taxYearId,
            Year = year,
            IsFiled = false,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxYear.TaxYearId, Is.EqualTo(taxYearId));
            Assert.That(taxYear.Year, Is.EqualTo(year));
            Assert.That(taxYear.IsFiled, Is.False);
            Assert.That(taxYear.FilingDate, Is.Null);
            Assert.That(taxYear.TotalDeductions, Is.EqualTo(0m));
            Assert.That(taxYear.Notes, Is.EqualTo(notes));
            Assert.That(taxYear.Deductions, Is.Not.Null);
            Assert.That(taxYear.Deductions, Is.Empty);
        });
    }

    [Test]
    public void CalculateTotalDeductions_WithMultipleDeductions_CalculatesCorrectTotal()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Deduction 1",
            Amount = 100m,
            Date = DateTime.Now,
            Category = DeductionCategory.MedicalExpenses
        });

        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Deduction 2",
            Amount = 250.50m,
            Date = DateTime.Now,
            Category = DeductionCategory.CharitableDonations
        });

        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Deduction 3",
            Amount = 500m,
            Date = DateTime.Now,
            Category = DeductionCategory.BusinessExpenses
        });

        // Act
        taxYear.CalculateTotalDeductions();

        // Assert
        Assert.That(taxYear.TotalDeductions, Is.EqualTo(850.50m));
    }

    [Test]
    public void CalculateTotalDeductions_WithNoDeductions_ReturnsZero()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        // Act
        taxYear.CalculateTotalDeductions();

        // Assert
        Assert.That(taxYear.TotalDeductions, Is.EqualTo(0m));
    }

    [Test]
    public void MarkAsFiled_SetsIsFiledToTrueAndFilingDate()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        taxYear.MarkAsFiled();

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxYear.IsFiled, Is.True);
            Assert.That(taxYear.FilingDate, Is.Not.Null);
            Assert.That(taxYear.FilingDate.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(taxYear.FilingDate.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void MarkAsFiled_CalledMultipleTimes_UpdatesFilingDate()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        // Act
        taxYear.MarkAsFiled();
        var firstFilingDate = taxYear.FilingDate;

        Thread.Sleep(10); // Small delay to ensure different timestamps

        taxYear.MarkAsFiled();
        var secondFilingDate = taxYear.FilingDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxYear.IsFiled, Is.True);
            Assert.That(secondFilingDate, Is.GreaterThanOrEqualTo(firstFilingDate));
        });
    }

    [Test]
    public void TaxYear_WithoutNotes_DefaultsToNull()
    {
        // Arrange & Act
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        // Assert
        Assert.That(taxYear.Notes, Is.Null);
    }

    [Test]
    public void CalculateTotalDeductions_WithSingleDeduction_ReturnsCorrectAmount()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Single deduction",
            Amount = 123.45m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        });

        // Act
        taxYear.CalculateTotalDeductions();

        // Assert
        Assert.That(taxYear.TotalDeductions, Is.EqualTo(123.45m));
    }

    [Test]
    public void TaxYear_Year_CanBeSetToHistoricalYear()
    {
        // Arrange & Act
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2015
        };

        // Assert
        Assert.That(taxYear.Year, Is.EqualTo(2015));
    }

    [Test]
    public void TaxYear_Year_CanBeSetToFutureYear()
    {
        // Arrange & Act
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2030
        };

        // Assert
        Assert.That(taxYear.Year, Is.EqualTo(2030));
    }

    [Test]
    public void TaxYear_Deductions_InitializesAsEmptyList()
    {
        // Arrange & Act
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxYear.Deductions, Is.Not.Null);
            Assert.That(taxYear.Deductions, Is.Empty);
            Assert.That(taxYear.Deductions.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void CalculateTotalDeductions_AfterAddingDeductions_UpdatesTotal()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "First",
            Amount = 100m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        });

        taxYear.CalculateTotalDeductions();
        var firstTotal = taxYear.TotalDeductions;

        // Act
        taxYear.Deductions.Add(new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Second",
            Amount = 200m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        });

        taxYear.CalculateTotalDeductions();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstTotal, Is.EqualTo(100m));
            Assert.That(taxYear.TotalDeductions, Is.EqualTo(300m));
        });
    }
}
