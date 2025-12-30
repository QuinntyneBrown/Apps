// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WineCellarInventory.Core;
using Microsoft.EntityFrameworkCore;

namespace WineCellarInventory.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WineCellarInventory system.
/// </summary>
public class WineCellarInventoryContext : DbContext, IWineCellarInventoryContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="WineCellarInventoryContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WineCellarInventoryContext(DbContextOptions<WineCellarInventoryContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Wine> Wines { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TastingNote> TastingNotes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<DrinkingWindow> DrinkingWindows { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Wine>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TastingNote>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<DrinkingWindow>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WineCellarInventoryContext).Assembly);
    }
}
