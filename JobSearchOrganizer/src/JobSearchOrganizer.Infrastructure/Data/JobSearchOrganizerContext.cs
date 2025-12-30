// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the JobSearchOrganizer system.
/// </summary>
public class JobSearchOrganizerContext : DbContext, IJobSearchOrganizerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JobSearchOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public JobSearchOrganizerContext(DbContextOptions<JobSearchOrganizerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Application> Applications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Company> Companies { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Interview> Interviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Offer> Offers { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobSearchOrganizerContext).Assembly);
    }
}
