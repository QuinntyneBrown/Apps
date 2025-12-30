// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core;

public record BudgetCreatedEvent
{
    public Guid BudgetId { get; init; }
    public Guid ProjectId { get; init; }
    public decimal AllocatedAmount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
