// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using DigitalLegacyPlanner.Core.Models.UserAggregate;
using DigitalLegacyPlanner.Core.Models.UserAggregate.Entities;
namespace DigitalLegacyPlanner.Core;

/// <summary>
/// Represents the persistence surface for the DigitalLegacyPlanner system.
/// </summary>
public interface IDigitalLegacyPlannerContext
{
    /// <summary>
    /// Gets or sets the DbSet of accounts.
    /// </summary>
    DbSet<DigitalAccount> Accounts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of instructions.
    /// </summary>
    DbSet<LegacyInstruction> Instructions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contacts.
    /// </summary>
    DbSet<TrustedContact> Contacts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of documents.
    /// </summary>
    DbSet<LegacyDocument> Documents { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
