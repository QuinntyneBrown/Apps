// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using MortgagePayoffOptimizer.Core.Model.UserAggregate;
using MortgagePayoffOptimizer.Core.Model.UserAggregate.Entities;
namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Represents the persistence surface for the MortgagePayoffOptimizer system.
/// </summary>
public interface IMortgagePayoffOptimizerContext
{
    /// <summary>
    /// Gets or sets the DbSet of mortgages.
    /// </summary>
    DbSet<Mortgage> Mortgages { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of payments.
    /// </summary>
    DbSet<Payment> Payments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of refinance scenarios.
    /// </summary>
    DbSet<RefinanceScenario> RefinanceScenarios { get; set; }

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
