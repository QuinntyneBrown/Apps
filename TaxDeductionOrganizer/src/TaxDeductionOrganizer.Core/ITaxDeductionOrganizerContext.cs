// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using TaxDeductionOrganizer.Core.Models.UserAggregate;
using TaxDeductionOrganizer.Core.Models.UserAggregate.Entities;
namespace TaxDeductionOrganizer.Core;

/// <summary>
/// Represents the persistence surface for the TaxDeductionOrganizer system.
/// </summary>
public interface ITaxDeductionOrganizerContext
{
    /// <summary>
    /// Gets or sets the DbSet of deductions.
    /// </summary>
    DbSet<Deduction> Deductions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of receipts.
    /// </summary>
    DbSet<Receipt> Receipts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tax years.
    /// </summary>
    DbSet<TaxYear> TaxYears { get; set; }

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
