// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Core;

/// <summary>
/// Represents the persistence surface for the GroceryShoppingListApp system.
/// </summary>
public interface IGroceryShoppingListAppContext
{
    /// <summary>
    /// Gets or sets the DbSet of grocery lists.
    /// </summary>
    DbSet<GroceryList> GroceryLists { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of grocery items.
    /// </summary>
    DbSet<GroceryItem> GroceryItems { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of stores.
    /// </summary>
    DbSet<Store> Stores { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of price histories.
    /// </summary>
    DbSet<PriceHistory> PriceHistories { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
