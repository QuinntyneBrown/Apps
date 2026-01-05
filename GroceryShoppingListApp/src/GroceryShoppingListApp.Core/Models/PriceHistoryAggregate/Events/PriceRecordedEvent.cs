// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core;

public record PriceRecordedEvent
{
    public Guid PriceHistoryId { get; init; }
    public decimal Price { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
