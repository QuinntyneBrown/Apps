// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using NutritionLabelScanner.Core.Models.UserAggregate;
using NutritionLabelScanner.Core.Models.UserAggregate.Entities;
namespace NutritionLabelScanner.Core;

/// <summary>
/// Represents the persistence surface for the NutritionLabelScanner system.
/// </summary>
public interface INutritionLabelScannerContext
{
    /// <summary>
    /// Gets or sets the DbSet of products.
    /// </summary>
    DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of nutrition information.
    /// </summary>
    DbSet<NutritionInfo> NutritionInfos { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of comparisons.
    /// </summary>
    DbSet<Comparison> Comparisons { get; set; }

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
