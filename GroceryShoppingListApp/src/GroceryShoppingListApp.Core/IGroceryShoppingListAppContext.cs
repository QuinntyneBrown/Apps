// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Core;

public interface IGroceryShoppingListAppContext
{
    DbSet<GroceryList> GroceryLists { get; set; }
    DbSet<GroceryItem> GroceryItems { get; set; }
    DbSet<Store> Stores { get; set; }
    DbSet<PriceHistory> PriceHistories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
