// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core;

public record GiftPurchasedEvent
{
    public Guid PurchaseId { get; init; }
    public Guid GiftIdeaId { get; init; }
    public decimal ActualPrice { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
