// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents the persistence surface for the ProfessionalNetworkCRM system.
/// </summary>
public interface IProfessionalNetworkCRMContext
{
    /// <summary>
    /// Gets or sets the DbSet of contacts.
    /// </summary>
    DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of interactions.
    /// </summary>
    DbSet<Interaction> Interactions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of follow-ups.
    /// </summary>
    DbSet<FollowUp> FollowUps { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
