// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using WineCellarInventory.Core.Model.UserAggregate;
using WineCellarInventory.Core.Model.UserAggregate.Entities;
namespace WineCellarInventory.Core;

/// <summary>
/// Represents the persistence surface for the WineCellarInventory system.
/// </summary>
public interface IWineCellarInventoryContext
{
    /// <summary>
    /// Gets or sets the DbSet of wines.
    /// </summary>
    DbSet<Wine> Wines { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tasting notes.
    /// </summary>
    DbSet<TastingNote> TastingNotes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of drinking windows.
    /// </summary>
    DbSet<DrinkingWindow> DrinkingWindows { get; set; }

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
