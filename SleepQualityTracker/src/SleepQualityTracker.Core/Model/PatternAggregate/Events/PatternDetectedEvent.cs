// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Event raised when a sleep pattern is detected.
/// </summary>
public record PatternDetectedEvent
{
    /// <summary>
    /// Gets the pattern ID.
    /// </summary>
    public Guid PatternId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the pattern name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the pattern type.
    /// </summary>
    public string PatternType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the confidence level.
    /// </summary>
    public int ConfidenceLevel { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
