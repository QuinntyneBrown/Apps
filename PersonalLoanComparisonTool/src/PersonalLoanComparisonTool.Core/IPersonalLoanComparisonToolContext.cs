// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PersonalLoanComparisonTool.Core.Model.UserAggregate;
using PersonalLoanComparisonTool.Core.Model.UserAggregate.Entities;
namespace PersonalLoanComparisonTool.Core;

/// <summary>
/// Represents the persistence surface for the PersonalLoanComparisonTool system.
/// </summary>
public interface IPersonalLoanComparisonToolContext
{
    /// <summary>
    /// Gets or sets the DbSet of loans.
    /// </summary>
    DbSet<Loan> Loans { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of offers.
    /// </summary>
    DbSet<Offer> Offers { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of payment schedules.
    /// </summary>
    DbSet<PaymentSchedule> PaymentSchedules { get; set; }

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
