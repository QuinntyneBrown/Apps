// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BillPaymentScheduler system.
/// </summary>
public class BillPaymentSchedulerContext : DbContext, IBillPaymentSchedulerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BillPaymentSchedulerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BillPaymentSchedulerContext(DbContextOptions<BillPaymentSchedulerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Bill> Bills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Payment> Payments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Payee> Payees { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillPaymentSchedulerContext).Assembly);
    }
}
