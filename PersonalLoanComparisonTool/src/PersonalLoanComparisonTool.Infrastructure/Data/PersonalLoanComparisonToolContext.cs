// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalLoanComparisonTool.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalLoanComparisonTool system.
/// </summary>
public class PersonalLoanComparisonToolContext : DbContext, IPersonalLoanComparisonToolContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalLoanComparisonToolContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalLoanComparisonToolContext(DbContextOptions<PersonalLoanComparisonToolContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Loan> Loans { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Offer> Offers { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PaymentSchedule> PaymentSchedules { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalLoanComparisonToolContext).Assembly);
    }
}
