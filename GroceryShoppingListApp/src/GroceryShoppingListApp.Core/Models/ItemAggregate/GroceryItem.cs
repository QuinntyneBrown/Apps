// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core;

public class GroceryItem
{
    public Guid GroceryItemId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid? GroceryListId { get; set; }
    public GroceryList? GroceryList { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
