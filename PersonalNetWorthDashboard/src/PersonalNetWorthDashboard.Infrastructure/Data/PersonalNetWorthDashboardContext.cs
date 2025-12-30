// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalNetWorthDashboard system.
/// </summary>
public class PersonalNetWorthDashboardContext : DbContext, IPersonalNetWorthDashboardContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalNetWorthDashboardContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalNetWorthDashboardContext(DbContextOptions<PersonalNetWorthDashboardContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Asset> Assets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Liability> Liabilities { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<NetWorthSnapshot> NetWorthSnapshots { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalNetWorthDashboardContext).Assembly);
    }
}
