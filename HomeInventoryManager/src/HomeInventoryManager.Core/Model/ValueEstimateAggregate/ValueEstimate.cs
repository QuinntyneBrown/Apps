// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core;

/// <summary>
/// Represents a value estimate for an inventory item at a specific point in time.
/// </summary>
public class ValueEstimate
{
    /// <summary>
    /// Gets or sets the unique identifier for the value estimate.
    /// </summary>
    public Guid ValueEstimateId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    public Item? Item { get; set; }

    /// <summary>
    /// Gets or sets the estimated value.
    /// </summary>
    public decimal EstimatedValue { get; set; }

    /// <summary>
    /// Gets or sets the estimation date.
    /// </summary>
    public DateTime EstimationDate { get; set; }

    /// <summary>
    /// Gets or sets the source of the estimate (e.g., Professional Appraisal, Online Tool, Personal).
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets notes about the estimate.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
