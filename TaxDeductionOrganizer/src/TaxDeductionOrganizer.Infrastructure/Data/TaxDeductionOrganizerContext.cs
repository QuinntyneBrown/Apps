// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;

namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the TaxDeductionOrganizer system.
/// </summary>
public class TaxDeductionOrganizerContext : DbContext, ITaxDeductionOrganizerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TaxDeductionOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public TaxDeductionOrganizerContext(DbContextOptions<TaxDeductionOrganizerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Deduction> Deductions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Receipt> Receipts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxYear> TaxYears { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaxDeductionOrganizerContext).Assembly);
    }
}
