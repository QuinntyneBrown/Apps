// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;

namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the LifeAdminDashboard system.
/// </summary>
public class LifeAdminDashboardContext : DbContext, ILifeAdminDashboardContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LifeAdminDashboardContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public LifeAdminDashboardContext(DbContextOptions<LifeAdminDashboardContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<AdminTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Renewal> Renewals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Deadline> Deadlines { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LifeAdminDashboardContext).Assembly);
    }
}
