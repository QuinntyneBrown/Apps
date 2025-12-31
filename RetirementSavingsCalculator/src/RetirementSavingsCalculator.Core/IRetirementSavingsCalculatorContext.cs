// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using RetirementSavingsCalculator.Core.Model.UserAggregate;
using RetirementSavingsCalculator.Core.Model.UserAggregate.Entities;
namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Represents the persistence surface for the RetirementSavingsCalculator system.
/// </summary>
public interface IRetirementSavingsCalculatorContext
{
    /// <summary>
    /// Gets or sets the DbSet of retirement scenarios.
    /// </summary>
    DbSet<RetirementScenario> RetirementScenarios { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contributions.
    /// </summary>
    DbSet<Contribution> Contributions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of withdrawal strategies.
    /// </summary>
    DbSet<WithdrawalStrategy> WithdrawalStrategies { get; set; }

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
