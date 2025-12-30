// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace WoodworkingProjectManager.Core;

/// <summary>
/// Represents the persistence surface for the WoodworkingProjectManager system.
/// </summary>
public interface IWoodworkingProjectManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of materials.
    /// </summary>
    DbSet<Material> Materials { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tools.
    /// </summary>
    DbSet<Tool> Tools { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
