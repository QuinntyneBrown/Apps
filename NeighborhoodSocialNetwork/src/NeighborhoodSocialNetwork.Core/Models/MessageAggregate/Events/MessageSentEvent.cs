// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Event raised when a message is sent.
/// </summary>
public record MessageSentEvent
{
    /// <summary>
    /// Gets the message ID.
    /// </summary>
    public Guid MessageId { get; init; }

    /// <summary>
    /// Gets the sender neighbor ID.
    /// </summary>
    public Guid SenderNeighborId { get; init; }

    /// <summary>
    /// Gets the recipient neighbor ID.
    /// </summary>
    public Guid RecipientNeighborId { get; init; }

    /// <summary>
    /// Gets the subject.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
