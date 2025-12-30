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
    /// <summary>
    /// Initializes a new instance of the <see cref="FreelanceProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FreelanceProjectManagerContext(DbContextOptions<FreelanceProjectManagerContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreelanceProjectManagerContext).Assembly);
    }
}
