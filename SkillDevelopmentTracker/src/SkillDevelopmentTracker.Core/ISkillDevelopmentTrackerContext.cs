// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents the persistence surface for the SkillDevelopmentTracker system.
/// </summary>
public interface ISkillDevelopmentTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of skills.
    /// </summary>
    DbSet<Skill> Skills { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of courses.
    /// </summary>
    DbSet<Course> Courses { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of certifications.
    /// </summary>
    DbSet<Certification> Certifications { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of learning paths.
    /// </summary>
    DbSet<LearningPath> LearningPaths { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
