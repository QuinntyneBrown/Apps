// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Represents a distraction during a focus session.
/// </summary>
public class Distraction
{
    /// <summary>
    /// Gets or sets the unique identifier for the distraction.
    /// </summary>
    public Guid DistractionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the session ID this distraction belongs to.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the distraction type or source.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the distraction.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets when the distraction occurred.
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Gets or sets the duration of the distraction in minutes.
    /// </summary>
    public double? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the distraction was internal (mind wandering) or external.
    /// </summary>
    public bool IsInternal { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the session.
    /// </summary>
    public FocusSession? Session { get; set; }
}
