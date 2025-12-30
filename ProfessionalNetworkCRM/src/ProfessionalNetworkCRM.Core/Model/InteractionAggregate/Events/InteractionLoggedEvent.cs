// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Event raised when an interaction is logged.
/// </summary>
public record InteractionLoggedEvent
{
    /// <summary>
    /// Gets the interaction ID.
    /// </summary>
    public Guid InteractionId { get; init; }

    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the interaction type.
    /// </summary>
    public string InteractionType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the interaction date.
    /// </summary>
    public DateTime InteractionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
