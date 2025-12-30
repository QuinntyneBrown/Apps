// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public record HoldingAddedEvent
{
    public Guid CryptoHoldingId { get; init; }
    public Guid WalletId { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
