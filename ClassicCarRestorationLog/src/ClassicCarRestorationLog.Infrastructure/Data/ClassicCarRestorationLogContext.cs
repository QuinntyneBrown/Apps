// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the ClassicCarRestorationLog system.
/// </summary>
public class ClassicCarRestorationLogContext : DbContext, IClassicCarRestorationLogContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassicCarRestorationLogContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ClassicCarRestorationLogContext(DbContextOptions<ClassicCarRestorationLogContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of parts.
    /// </summary>
    public DbSet<Part> Parts { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of work logs.
    /// </summary>
    public DbSet<WorkLog> WorkLogs { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of photo logs.
    /// </summary>
    public DbSet<PhotoLog> PhotoLogs { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassicCarRestorationLogContext).Assembly);
    }
}
