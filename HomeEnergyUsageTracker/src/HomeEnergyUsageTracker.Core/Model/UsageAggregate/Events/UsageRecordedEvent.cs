// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core;

public record UsageRecordedEvent
{
    public Guid UsageId { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
