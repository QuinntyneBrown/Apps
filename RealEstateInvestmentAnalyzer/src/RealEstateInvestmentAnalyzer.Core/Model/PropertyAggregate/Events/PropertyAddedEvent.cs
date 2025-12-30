// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public record PropertyAddedEvent
{
    public Guid PropertyId { get; init; }
    public string Address { get; init; } = string.Empty;
    public decimal PurchasePrice { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
