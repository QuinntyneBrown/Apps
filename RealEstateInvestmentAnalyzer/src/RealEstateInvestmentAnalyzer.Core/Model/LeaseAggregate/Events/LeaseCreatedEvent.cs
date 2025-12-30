// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public record LeaseCreatedEvent
{
    public Guid LeaseId { get; init; }
    public Guid PropertyId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public decimal MonthlyRent { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
