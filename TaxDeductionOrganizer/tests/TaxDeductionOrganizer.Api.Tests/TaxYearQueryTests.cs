// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.TaxYears;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class TaxYearQueryTests
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
    public async Task GetAllTaxYears_ShouldReturnAllTaxYears()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        context.TaxYears.AddRange(
            new TaxYear { TaxYearId = Guid.NewGuid(), Year = 2023, IsFiled = true },
            new TaxYear { TaxYearId = Guid.NewGuid(), Year = 2024, IsFiled = false }
        );
        await context.SaveChangesAsync();

        var handler = new GetAllTaxYears.Handler(context);
        var query = new GetAllTaxYears.Query();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Year, Is.EqualTo(2024)); // Ordered by year descending
        Assert.That(result[1].Year, Is.EqualTo(2023));
    }

    [Test]
    public async Task GetAllTaxYears_WithNoData_ShouldReturnEmptyList()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new GetAllTaxYears.Handler(context);
        var query = new GetAllTaxYears.Query();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetTaxYearById_ShouldReturnTaxYear()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            Notes = "Test notes"
        };
        context.TaxYears.Add(taxYear);
        await context.SaveChangesAsync();

        var handler = new GetTaxYearById.Handler(context);
        var query = new GetTaxYearById.Query { TaxYearId = taxYear.TaxYearId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TaxYearId, Is.EqualTo(taxYear.TaxYearId));
        Assert.That(result.Year, Is.EqualTo(2024));
        Assert.That(result.Notes, Is.EqualTo("Test notes"));
    }

    [Test]
    public async Task GetTaxYearById_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new GetTaxYearById.Handler(context);
        var query = new GetTaxYearById.Query { TaxYearId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(query, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
