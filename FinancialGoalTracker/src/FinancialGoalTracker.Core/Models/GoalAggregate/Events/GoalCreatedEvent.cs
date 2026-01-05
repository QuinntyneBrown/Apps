// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core;

public record GoalCreatedEvent
{
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
