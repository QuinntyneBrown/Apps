// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalHealthDashboard system.
/// </summary>
public class PersonalHealthDashboardContext : DbContext, IPersonalHealthDashboardContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalHealthDashboardContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalHealthDashboardContext(DbContextOptions<PersonalHealthDashboardContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Vital> Vitals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WearableData> WearableData { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<HealthTrend> HealthTrends { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalHealthDashboardContext).Assembly);
    }
}
