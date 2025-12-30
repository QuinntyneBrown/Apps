// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class DeductionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDeduction()
    {
        // Arrange
        var deductionId = Guid.NewGuid();
        var taxYearId = Guid.NewGuid();
        var description = "Medical expenses";
        var amount = 500.00m;
        var date = new DateTime(2024, 3, 15);
        var category = DeductionCategory.MedicalExpenses;
        var notes = "Doctor visit";

        // Act
        var deduction = new Deduction
        {
            DeductionId = deductionId,
            TaxYearId = taxYearId,
            Description = description,
            Amount = amount,
            Date = date,
            Category = category,
            Notes = notes,
            HasReceipt = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(deduction.DeductionId, Is.EqualTo(deductionId));
            Assert.That(deduction.TaxYearId, Is.EqualTo(taxYearId));
            Assert.That(deduction.Description, Is.EqualTo(description));
            Assert.That(deduction.Amount, Is.EqualTo(amount));
            Assert.That(deduction.Date, Is.EqualTo(date));
            Assert.That(deduction.Category, Is.EqualTo(category));
            Assert.That(deduction.Notes, Is.EqualTo(notes));
            Assert.That(deduction.HasReceipt, Is.False);
        });
    }

    [Test]
    public void AttachReceipt_SetsHasReceiptToTrue()
    {
        // Arrange
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Charitable donation",
            Amount = 1000m,
            Date = DateTime.Now,
            Category = DeductionCategory.CharitableDonations,
            HasReceipt = false
        };

        // Act
        deduction.AttachReceipt();

        // Assert
        Assert.That(deduction.HasReceipt, Is.True);
    }

    [Test]
    public void DeductionCategory_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Deduction { Category = DeductionCategory.MedicalExpenses }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.CharitableDonations }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.MortgageInterest }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.StateAndLocalTaxes }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.BusinessExpenses }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.EducationExpenses }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.HomeOffice }, Throws.Nothing);
            Assert.That(() => new Deduction { Category = DeductionCategory.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void Deduction_WithoutNotes_DefaultsToNull()
    {
        // Arrange & Act
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Business expense",
            Amount = 250m,
            Date = DateTime.Now,
            Category = DeductionCategory.BusinessExpenses
        };

        // Assert
        Assert.That(deduction.Notes, Is.Null);
    }

    [Test]
    public void Deduction_WithLargeAmount_StoresCorrectly()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Large expense",
            Amount = largeAmount,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        };

        // Assert
        Assert.That(deduction.Amount, Is.EqualTo(largeAmount));
    }

    [Test]
    public void Deduction_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Zero amount deduction",
            Amount = 0m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        };

        // Assert
        Assert.That(deduction.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Deduction_TaxYearNavigation_CanBeSet()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Test deduction",
            Amount = 100m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        };

        // Act
        deduction.TaxYear = taxYear;

        // Assert
        Assert.That(deduction.TaxYear, Is.Not.Null);
        Assert.That(deduction.TaxYear.TaxYearId, Is.EqualTo(taxYear.TaxYearId));
    }

    [Test]
    public void AttachReceipt_CalledMultipleTimes_RemainsTrue()
    {
        // Arrange
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Test",
            Amount = 100m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        };

        // Act
        deduction.AttachReceipt();
        deduction.AttachReceipt();
        deduction.AttachReceipt();

        // Assert
        Assert.That(deduction.HasReceipt, Is.True);
    }

    [Test]
    public void Deduction_WithPastDate_IsValid()
    {
        // Arrange
        var pastDate = new DateTime(2020, 1, 1);

        // Act
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Old deduction",
            Amount = 100m,
            Date = pastDate,
            Category = DeductionCategory.Other
        };

        // Assert
        Assert.That(deduction.Date, Is.EqualTo(pastDate));
    }
}
