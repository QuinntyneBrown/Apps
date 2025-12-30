// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PhotographySessionLogger system.
/// </summary>
public class PhotographySessionLoggerContext : DbContext, IPhotographySessionLoggerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhotographySessionLoggerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PhotographySessionLoggerContext(DbContextOptions<PhotographySessionLoggerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Session> Sessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Photo> Photos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Gear> Gears { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhotographySessionLoggerContext).Assembly);
    }
}
