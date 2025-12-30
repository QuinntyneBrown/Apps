// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalMissionStatementBuilder system.
/// </summary>
public class PersonalMissionStatementBuilderContext : DbContext, IPersonalMissionStatementBuilderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalMissionStatementBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalMissionStatementBuilderContext(DbContextOptions<PersonalMissionStatementBuilderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<MissionStatement> MissionStatements { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Value> Values { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Progress> Progresses { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalMissionStatementBuilderContext).Assembly);
    }
}
