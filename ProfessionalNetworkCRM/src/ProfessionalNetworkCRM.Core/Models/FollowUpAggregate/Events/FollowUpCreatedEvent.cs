// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Event raised when a new follow-up is created.
/// </summary>
public record FollowUpCreatedEvent
{
    /// <summary>
    /// Gets the follow-up ID.
    /// </summary>
    public Guid FollowUpId { get; init; }

    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the due date.
    /// </summary>
    public DateTime DueDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
