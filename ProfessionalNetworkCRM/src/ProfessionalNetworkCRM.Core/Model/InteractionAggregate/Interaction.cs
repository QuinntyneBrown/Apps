// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents an interaction with a contact.
/// </summary>
public class Interaction
{
    /// <summary>
    /// Gets or sets the unique identifier for the interaction.
    /// </summary>
    public Guid InteractionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this interaction.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the type of interaction (Email, Meeting, Call, Message, etc.).
    /// </summary>
    public string InteractionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of the interaction.
    /// </summary>
    public DateTime InteractionDate { get; set; }

    /// <summary>
    /// Gets or sets the subject or title of the interaction.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets notes about the interaction.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the outcome or result of the interaction.
    /// </summary>
    public string? Outcome { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes (for calls/meetings).
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the contact.
    /// </summary>
    public Contact? Contact { get; set; }

    /// <summary>
    /// Updates the outcome of the interaction.
    /// </summary>
    /// <param name="outcome">The outcome to set.</param>
    public void UpdateOutcome(string outcome)
    {
        Outcome = outcome;
        UpdatedAt = DateTime.UtcNow;
    }
}
