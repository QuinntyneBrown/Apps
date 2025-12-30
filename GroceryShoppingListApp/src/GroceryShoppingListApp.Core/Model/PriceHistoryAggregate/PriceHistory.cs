// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core;

public class PriceHistory
{
    public Guid PriceHistoryId { get; set; }
    public Guid GroceryItemId { get; set; }
    public Guid StoreId { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
