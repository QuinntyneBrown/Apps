// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FreelanceProjectManager.Core.Models.UserAggregate;
using FreelanceProjectManager.Core.Models.UserAggregate.Entities;
namespace FreelanceProjectManager.Core;

/// <summary>
/// Represents the persistence surface for the FreelanceProjectManager system.
/// </summary>
public interface IFreelanceProjectManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of clients.
    /// </summary>
    DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of time entries.
    /// </summary>
    DbSet<TimeEntry> TimeEntries { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of invoices.
    /// </summary>
    DbSet<Invoice> Invoices { get; set; }

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
