// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents a neighborhood event.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the neighbor ID who created this event.
    /// </summary>
    public Guid CreatedByNeighborId { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event date and time.
    /// </summary>
    public DateTime EventDateTime { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the event is public.
    /// </summary>
    public bool IsPublic { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
