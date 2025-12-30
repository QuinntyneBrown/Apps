// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Api.Features.Distraction;

/// <summary>
/// Data transfer object for distraction.
/// </summary>
public class DistractionDto
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid DistractionId { get; set; }

    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the distraction type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets when the distraction occurred.
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    public double? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the distraction is internal.
    /// </summary>
    public bool IsInternal { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
