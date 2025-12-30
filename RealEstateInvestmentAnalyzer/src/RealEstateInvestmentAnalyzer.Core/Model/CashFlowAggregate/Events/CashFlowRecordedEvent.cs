// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public record CashFlowRecordedEvent
{
    public Guid CashFlowId { get; init; }
    public Guid PropertyId { get; init; }
    public decimal NetCashFlow { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
