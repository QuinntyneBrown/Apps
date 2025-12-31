// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using SideHustleIncomeTracker.Core.Model.UserAggregate;
using SideHustleIncomeTracker.Core.Model.UserAggregate.Entities;
namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Represents the persistence surface for the SideHustleIncomeTracker system.
/// </summary>
public interface ISideHustleIncomeTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of businesses.
    /// </summary>
    DbSet<Business> Businesses { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of incomes.
    /// </summary>
    DbSet<Income> Incomes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of expenses.
    /// </summary>
    DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tax estimates.
    /// </summary>
    DbSet<TaxEstimate> TaxEstimates { get; set; }

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
