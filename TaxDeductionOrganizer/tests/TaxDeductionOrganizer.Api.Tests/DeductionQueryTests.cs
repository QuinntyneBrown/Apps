// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.Deductions;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class DeductionQueryTests
{
    private DbContextOptions<TaxDeductionOrganizerContext> _options = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<TaxDeductionOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Test]
    public async Task GetAllDeductions_ShouldReturnAllDeductions()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYearId = Guid.NewGuid();
        context.Deductions.AddRange(
            new Deduction
            {
                DeductionId = Guid.NewGuid(),
                TaxYearId = taxYearId,
                Description = "Deduction 1",
                Amount = 100m,
                Date = DateTime.UtcNow.AddDays(-1),
                Category = DeductionCategory.MedicalExpenses
            },
            new Deduction
            {
                DeductionId = Guid.NewGuid(),
                TaxYearId = taxYearId,
                Description = "Deduction 2",
                Amount = 200m,
                Date = DateTime.UtcNow,
                Category = DeductionCategory.CharitableDonations
            }
        );
        await context.SaveChangesAsync();

        var handler = new GetAllDeductions.Handler(context);
        var query = new GetAllDeductions.Query();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAllDeductions_FilteredByTaxYear_ShouldReturnFilteredDeductions()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYearId1 = Guid.NewGuid();
        var taxYearId2 = Guid.NewGuid();
        context.Deductions.AddRange(
            new Deduction
            {
                DeductionId = Guid.NewGuid(),
                TaxYearId = taxYearId1,
                Description = "Deduction 1",
                Amount = 100m,
                Date = DateTime.UtcNow,
                Category = DeductionCategory.MedicalExpenses
            },
            new Deduction
            {
                DeductionId = Guid.NewGuid(),
                TaxYearId = taxYearId2,
                Description = "Deduction 2",
                Amount = 200m,
                Date = DateTime.UtcNow,
                Category = DeductionCategory.CharitableDonations
            }
        );
        await context.SaveChangesAsync();

        var handler = new GetAllDeductions.Handler(context);
        var query = new GetAllDeductions.Query { TaxYearId = taxYearId1 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].TaxYearId, Is.EqualTo(taxYearId1));
    }

    [Test]
    public async Task GetDeductionById_ShouldReturnDeduction()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Test deduction",
            Amount = 150.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.BusinessExpenses,
            Notes = "Test notes"
        };
        context.Deductions.Add(deduction);
        await context.SaveChangesAsync();

        var handler = new GetDeductionById.Handler(context);
        var query = new GetDeductionById.Query { DeductionId = deduction.DeductionId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.DeductionId, Is.EqualTo(deduction.DeductionId));
        Assert.That(result.Description, Is.EqualTo("Test deduction"));
        Assert.That(result.Amount, Is.EqualTo(150.00m));
    }

    [Test]
    public async Task GetDeductionById_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new GetDeductionById.Handler(context);
        var query = new GetDeductionById.Query { DeductionId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(query, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
