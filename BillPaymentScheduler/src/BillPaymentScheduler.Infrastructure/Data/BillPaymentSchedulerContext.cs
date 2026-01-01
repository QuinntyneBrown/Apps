// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;

using BillPaymentScheduler.Core.Model.UserAggregate;
using BillPaymentScheduler.Core.Model.UserAggregate.Entities;
namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BillPaymentScheduler system.
/// </summary>
public class BillPaymentSchedulerContext : DbContext, IBillPaymentSchedulerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BillPaymentSchedulerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BillPaymentSchedulerContext(DbContextOptions<BillPaymentSchedulerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Bill> Bills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Payment> Payments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Payee> Payees { get; set; } = null!;


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Bill>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Payment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Payee>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillPaymentSchedulerContext).Assembly);
    }
}
