// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MorningRoutineBuilder system.
/// </summary>
public class MorningRoutineBuilderContext : DbContext, IMorningRoutineBuilderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MorningRoutineBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MorningRoutineBuilderContext(DbContextOptions<MorningRoutineBuilderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Routine> Routines { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RoutineTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<CompletionLog> CompletionLogs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Streak> Streaks { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MorningRoutineBuilderContext).Assembly);
    }
}
