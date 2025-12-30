// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalProjectPipeline.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalProjectPipeline system.
/// </summary>
public class PersonalProjectPipelineContext : DbContext, IPersonalProjectPipelineContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalProjectPipelineContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalProjectPipelineContext(DbContextOptions<PersonalProjectPipelineContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ProjectTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalProjectPipelineContext).Assembly);
    }
}
