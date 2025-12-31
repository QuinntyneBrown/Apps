// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate;
using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate.Entities;
namespace RealEstateInvestmentAnalyzer.Core;

/// <summary>
/// Represents the persistence surface for the RealEstateInvestmentAnalyzer system.
/// </summary>
public interface IRealEstateInvestmentAnalyzerContext
{
    /// <summary>
    /// Gets or sets the DbSet of properties.
    /// </summary>
    DbSet<Property> Properties { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of cash flows.
    /// </summary>
    DbSet<CashFlow> CashFlows { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of expenses.
    /// </summary>
    DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of leases.
    /// </summary>
    DbSet<Lease> Leases { get; set; }

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
