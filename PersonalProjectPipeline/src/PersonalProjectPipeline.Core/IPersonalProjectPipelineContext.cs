// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents the persistence surface for the PersonalProjectPipeline system.
/// </summary>
public interface IPersonalProjectPipelineContext
{
    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tasks.
    /// </summary>
    DbSet<ProjectTask> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of milestones.
    /// </summary>
    DbSet<Milestone> Milestones { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
