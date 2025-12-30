// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents a message between neighbors.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the unique identifier for the message.
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Gets or sets the sender neighbor ID.
    /// </summary>
    public Guid SenderNeighborId { get; set; }

    /// <summary>
    /// Gets or sets the recipient neighbor ID.
    /// </summary>
    public Guid RecipientNeighborId { get; set; }

    /// <summary>
    /// Gets or sets the subject of the message.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the message.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the message has been read.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the date the message was read.
    /// </summary>
    public DateTime? ReadAt { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the sender neighbor.
    /// </summary>
    public Neighbor? SenderNeighbor { get; set; }

    /// <summary>
    /// Marks the message as read.
    /// </summary>
    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTime.UtcNow;
    }
}
