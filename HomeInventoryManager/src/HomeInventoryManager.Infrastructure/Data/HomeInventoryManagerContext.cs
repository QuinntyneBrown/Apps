// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeInventoryManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeInventoryManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeInventoryManager system.
/// </summary>
public class HomeInventoryManagerContext : DbContext, IHomeInventoryManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeInventoryManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeInventoryManagerContext(DbContextOptions<HomeInventoryManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Item> Items { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ValueEstimate> ValueEstimates { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeInventoryManagerContext).Assembly);
    }
}
