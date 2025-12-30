// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.TaxYears;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class TaxYearCommandTests
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
    public async Task CreateTaxYear_ShouldCreateTaxYear()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new CreateTaxYear.Handler(context);
        var command = new CreateTaxYear.Command
        {
            Year = 2024,
            Notes = "Test tax year"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Year, Is.EqualTo(2024));
        Assert.That(result.Notes, Is.EqualTo("Test tax year"));
        Assert.That(result.IsFiled, Is.False);
        Assert.That(result.TotalDeductions, Is.EqualTo(0));

        var savedTaxYear = await context.TaxYears.FindAsync(result.TaxYearId);
        Assert.That(savedTaxYear, Is.Not.Null);
    }

    [Test]
    public async Task UpdateTaxYear_ShouldUpdateTaxYear()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2023,
            IsFiled = false,
            Notes = "Original notes"
        };
        context.TaxYears.Add(taxYear);
        await context.SaveChangesAsync();

        var handler = new UpdateTaxYear.Handler(context);
        var command = new UpdateTaxYear.Command
        {
            TaxYearId = taxYear.TaxYearId,
            Year = 2024,
            IsFiled = true,
            FilingDate = DateTime.UtcNow,
            Notes = "Updated notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Year, Is.EqualTo(2024));
        Assert.That(result.IsFiled, Is.True);
        Assert.That(result.Notes, Is.EqualTo("Updated notes"));
    }

    [Test]
    public async Task UpdateTaxYear_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new UpdateTaxYear.Handler(context);
        var command = new UpdateTaxYear.Command
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }

    [Test]
    public async Task DeleteTaxYear_ShouldDeleteTaxYear()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2023,
            IsFiled = false
        };
        context.TaxYears.Add(taxYear);
        await context.SaveChangesAsync();

        var handler = new DeleteTaxYear.Handler(context);
        var command = new DeleteTaxYear.Command { TaxYearId = taxYear.TaxYearId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedTaxYear = await context.TaxYears.FindAsync(taxYear.TaxYearId);
        Assert.That(deletedTaxYear, Is.Null);
    }

    [Test]
    public async Task DeleteTaxYear_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new DeleteTaxYear.Handler(context);
        var command = new DeleteTaxYear.Command { TaxYearId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
