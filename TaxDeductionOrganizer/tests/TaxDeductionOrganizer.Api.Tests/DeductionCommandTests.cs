// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.Deductions;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class DeductionCommandTests
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
    public async Task CreateDeduction_ShouldCreateDeduction()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYearId = Guid.NewGuid();
        var handler = new CreateDeduction.Handler(context);
        var command = new CreateDeduction.Command
        {
            TaxYearId = taxYearId,
            Description = "Medical expense",
            Amount = 500.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.MedicalExpenses,
            Notes = "Doctor visit"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo("Medical expense"));
        Assert.That(result.Amount, Is.EqualTo(500.00m));
        Assert.That(result.Category, Is.EqualTo(DeductionCategory.MedicalExpenses));
        Assert.That(result.HasReceipt, Is.False);

        var savedDeduction = await context.Deductions.FindAsync(result.DeductionId);
        Assert.That(savedDeduction, Is.Not.Null);
    }

    [Test]
    public async Task UpdateDeduction_ShouldUpdateDeduction()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Original description",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Other
        };
        context.Deductions.Add(deduction);
        await context.SaveChangesAsync();

        var handler = new UpdateDeduction.Handler(context);
        var command = new UpdateDeduction.Command
        {
            DeductionId = deduction.DeductionId,
            Description = "Updated description",
            Amount = 200.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.CharitableDonations,
            Notes = "Updated notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo("Updated description"));
        Assert.That(result.Amount, Is.EqualTo(200.00m));
        Assert.That(result.Category, Is.EqualTo(DeductionCategory.CharitableDonations));
    }

    [Test]
    public async Task UpdateDeduction_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new UpdateDeduction.Handler(context);
        var command = new UpdateDeduction.Command
        {
            DeductionId = Guid.NewGuid(),
            Description = "Test",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Other
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }

    [Test]
    public async Task DeleteDeduction_ShouldDeleteDeduction()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Test deduction",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Other
        };
        context.Deductions.Add(deduction);
        await context.SaveChangesAsync();

        var handler = new DeleteDeduction.Handler(context);
        var command = new DeleteDeduction.Command { DeductionId = deduction.DeductionId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedDeduction = await context.Deductions.FindAsync(deduction.DeductionId);
        Assert.That(deletedDeduction, Is.Null);
    }

    [Test]
    public async Task DeleteDeduction_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new DeleteDeduction.Handler(context);
        var command = new DeleteDeduction.Command { DeductionId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
