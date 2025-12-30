// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class GroceryItemDto
{
    public Guid GroceryItemId { get; set; }
    public Guid? GroceryListId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
    public DateTime CreatedAt { get; set; }
}
