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
    /// <summary>
    /// Initializes a new instance of the <see cref="WoodworkingProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WoodworkingProjectManagerContext(DbContextOptions<WoodworkingProjectManagerContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WoodworkingProjectManagerContext).Assembly);
    }
}
