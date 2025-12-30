// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeImprovementProjectManager system.
/// </summary>
public class HomeImprovementProjectManagerContext : DbContext, IHomeImprovementProjectManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeImprovementProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeImprovementProjectManagerContext(DbContextOptions<HomeImprovementProjectManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Budget> Budgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contractor> Contractors { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Material> Materials { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeImprovementProjectManagerContext).Assembly);
    }
}
