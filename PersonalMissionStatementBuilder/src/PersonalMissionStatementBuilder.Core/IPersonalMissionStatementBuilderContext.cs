// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Represents the persistence surface for the PersonalMissionStatementBuilder system.
/// </summary>
public interface IPersonalMissionStatementBuilderContext
{
    /// <summary>
    /// Gets or sets the DbSet of mission statements.
    /// </summary>
    DbSet<MissionStatement> MissionStatements { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of values.
    /// </summary>
    DbSet<Value> Values { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of goals.
    /// </summary>
    DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of progress entries.
    /// </summary>
    DbSet<Progress> Progresses { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
