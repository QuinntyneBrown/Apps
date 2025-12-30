// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public record TaxLotCreatedEvent
{
    public Guid TaxLotId { get; init; }
    public Guid CryptoHoldingId { get; init; }
    public decimal Quantity { get; init; }
    public decimal CostBasis { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
