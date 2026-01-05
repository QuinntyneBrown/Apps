// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Event raised when a subscription is added.
/// </summary>
public record SubscriptionAddedEvent
{
    /// <summary>
    /// Gets the subscription ID.
    /// </summary>
    public Guid SubscriptionId { get; init; }

    /// <summary>
    /// Gets the service name.
    /// </summary>
    public string ServiceName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the cost.
    /// </summary>
    public decimal Cost { get; init; }

    /// <summary>
    /// Gets the billing cycle.
    /// </summary>
    public BillingCycle BillingCycle { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
