// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;

using TaxDeductionOrganizer.Core.Model.UserAggregate;
using TaxDeductionOrganizer.Core.Model.UserAggregate.Entities;
namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the TaxDeductionOrganizer system.
/// </summary>
public class TaxDeductionOrganizerContext : DbContext, ITaxDeductionOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaxDeductionOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public TaxDeductionOrganizerContext(DbContextOptions<TaxDeductionOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Deduction> Deductions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Receipt> Receipts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxYear> TaxYears { get; set; } = null!;


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
            modelBuilder.Entity<Deduction>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Receipt>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TaxYear>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaxDeductionOrganizerContext).Assembly);
    }
}
