// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;

namespace ProfessionalNetworkCRM.Core.Models.OpportunityAggregate;

/// <summary>
/// Represents a professional opportunity.
/// </summary>
public class Opportunity
{
    /// <summary>
    /// Gets or sets the unique identifier for the opportunity.
    /// </summary>
    public Guid OpportunityId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID who provided this opportunity.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the opportunity type.
    /// </summary>
    public OpportunityType Type { get; set; }

    /// <summary>
    /// Gets or sets the description of the opportunity.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the opportunity.
    /// </summary>
    public OpportunityStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the potential value of the opportunity.
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this opportunity.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Updates the status of the opportunity.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(OpportunityStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the value of the opportunity.
    /// </summary>
    /// <param name="value">The new value.</param>
    public void UpdateValue(decimal? value)
    {
        Value = value;
        UpdatedAt = DateTime.UtcNow;
    }
}
