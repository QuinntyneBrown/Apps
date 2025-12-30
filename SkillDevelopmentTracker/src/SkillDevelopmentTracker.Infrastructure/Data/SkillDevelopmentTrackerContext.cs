// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SkillDevelopmentTracker system.
/// </summary>
public class SkillDevelopmentTrackerContext : DbContext, ISkillDevelopmentTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SkillDevelopmentTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SkillDevelopmentTrackerContext(DbContextOptions<SkillDevelopmentTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Skill> Skills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Course> Courses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Certification> Certifications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LearningPath> LearningPaths { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillDevelopmentTrackerContext).Assembly);
    }
}
