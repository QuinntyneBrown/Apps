// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents the persistence surface for the JobSearchOrganizer system.
/// </summary>
public interface IJobSearchOrganizerContext
{
    /// <summary>
    /// Gets or sets the DbSet of job applications.
    /// </summary>
    DbSet<Application> Applications { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of companies.
    /// </summary>
    DbSet<Company> Companies { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of interviews.
    /// </summary>
    DbSet<Interview> Interviews { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of offers.
    /// </summary>
    DbSet<Offer> Offers { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
