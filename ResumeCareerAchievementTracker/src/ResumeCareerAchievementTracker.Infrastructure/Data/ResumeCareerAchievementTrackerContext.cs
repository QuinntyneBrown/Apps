// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace ResumeCareerAchievementTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ResumeCareerAchievementTracker system.
/// </summary>
public class ResumeCareerAchievementTrackerContext : DbContext, IResumeCareerAchievementTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeCareerAchievementTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ResumeCareerAchievementTrackerContext(DbContextOptions<ResumeCareerAchievementTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Achievement> Achievements { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Skill> Skills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResumeCareerAchievementTrackerContext).Assembly);
    }
}
