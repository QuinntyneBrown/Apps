// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Represents a subscription category.
/// </summary>
public class Category
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the color code for the category.
    /// </summary>
    public string? ColorCode { get; set; }

    /// <summary>
    /// Gets or sets the collection of subscriptions in this category.
    /// </summary>
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    /// <summary>
    /// Calculates the total monthly cost for all active subscriptions in this category.
    /// </summary>
    public decimal CalculateTotalMonthlyCost()
    {
        return Subscriptions
            .Where(s => s.Status == SubscriptionStatus.Active)
            .Sum(s => s.BillingCycle == BillingCycle.Monthly ? s.Cost : s.CalculateAnnualCost() / 12);
    }
}
