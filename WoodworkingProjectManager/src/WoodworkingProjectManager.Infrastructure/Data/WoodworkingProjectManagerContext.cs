// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WoodworkingProjectManager.Core;
using Microsoft.EntityFrameworkCore;

namespace WoodworkingProjectManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WoodworkingProjectManager system.
/// </summary>
public class WoodworkingProjectManagerContext : DbContext, IWoodworkingProjectManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="WoodworkingProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WoodworkingProjectManagerContext(DbContextOptions<WoodworkingProjectManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Material> Materials { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tool> Tools { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Material>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Tool>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WoodworkingProjectManagerContext).Assembly);
    }
}
