// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Models.ReferralAggregate;

/// <summary>
/// Represents a professional referral received.
/// </summary>
public class Referral
{
    /// <summary>
    /// Gets or sets the unique identifier for the referral.
    /// </summary>
    public Guid ReferralId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID who provided the referral.
    /// </summary>
    public Guid SourceContactId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the description of the referral.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the outcome of the referral.
    /// </summary>
    public string? Outcome { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this referral.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a thank you note has been sent.
    /// </summary>
    public bool ThankYouSent { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Records that a thank you note has been sent.
    /// </summary>
    public void MarkThankYouSent()
    {
        ThankYouSent = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the outcome of the referral.
    /// </summary>
    /// <param name="outcome">The outcome.</param>
    public void UpdateOutcome(string outcome)
    {
        Outcome = outcome;
        UpdatedAt = DateTime.UtcNow;
    }
}
