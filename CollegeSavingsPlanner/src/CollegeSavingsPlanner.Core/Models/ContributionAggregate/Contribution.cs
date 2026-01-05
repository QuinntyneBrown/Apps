// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Represents a contribution to a 529 plan.
/// </summary>
public class Contribution
{
    /// <summary>
    /// Gets or sets the unique identifier for the contribution.
    /// </summary>
    public Guid ContributionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the plan.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Gets or sets the contribution amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the contribution date.
    /// </summary>
    public DateTime ContributionDate { get; set; }

    /// <summary>
    /// Gets or sets the contributor name.
    /// </summary>
    public string? Contributor { get; set; }

    /// <summary>
    /// Gets or sets notes about the contribution.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this is a recurring contribution.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the plan.
    /// </summary>
    public Plan? Plan { get; set; }

    /// <summary>
    /// Validates the contribution amount.
    /// </summary>
    public void ValidateAmount()
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Contribution amount must be positive.", nameof(Amount));
        }
    }
}
