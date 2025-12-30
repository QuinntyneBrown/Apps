// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FreelanceProjectManager system.
/// </summary>
public class FreelanceProjectManagerContext : DbContext, IFreelanceProjectManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FreelanceProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FreelanceProjectManagerContext(DbContextOptions<FreelanceProjectManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Client> Clients { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Invoice> Invoices { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Client>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TimeEntry>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Invoice>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreelanceProjectManagerContext).Assembly);
    }
}
