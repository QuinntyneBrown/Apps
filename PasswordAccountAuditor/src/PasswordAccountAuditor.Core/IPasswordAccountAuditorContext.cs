// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the persistence surface for the PasswordAccountAuditor system.
/// </summary>
public interface IPasswordAccountAuditorContext
{
    /// <summary>
    /// Gets or sets the DbSet of accounts.
    /// </summary>
    DbSet<Account> Accounts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of security audits.
    /// </summary>
    DbSet<SecurityAudit> SecurityAudits { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of breach alerts.
    /// </summary>
    DbSet<BreachAlert> BreachAlerts { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
