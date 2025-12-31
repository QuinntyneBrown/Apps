// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Represents a subscription service.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Gets or sets the unique identifier for the subscription.
    /// </summary>
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the service name.
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cost per billing cycle.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets the billing cycle.
    /// </summary>
    public BillingCycle BillingCycle { get; set; }

    /// <summary>
    /// Gets or sets the next billing date.
    /// </summary>
    public DateTime NextBillingDate { get; set; }

    /// <summary>
    /// Gets or sets the subscription status.
    /// </summary>
    public SubscriptionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the cancellation date.
    /// </summary>
    public DateTime? CancellationDate { get; set; }

    /// <summary>
    /// Gets or sets the reference to the category.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Gets or sets notes about the subscription.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the category.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Calculates the annual cost of the subscription.
    /// </summary>
    public decimal CalculateAnnualCost()
    {
        return BillingCycle switch
        {
            BillingCycle.Monthly => Cost * 12,
            BillingCycle.Quarterly => Cost * 4,
            BillingCycle.Annual => Cost,
            BillingCycle.Weekly => Cost * 52,
            _ => 0
        };
    }

    /// <summary>
    /// Cancels the subscription.
    /// </summary>
    public void Cancel()
    {
        Status = SubscriptionStatus.Cancelled;
        CancellationDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Reactivates the subscription.
    /// </summary>
    public void Reactivate()
    {
        Status = SubscriptionStatus.Active;
        CancellationDate = null;
    }
}
