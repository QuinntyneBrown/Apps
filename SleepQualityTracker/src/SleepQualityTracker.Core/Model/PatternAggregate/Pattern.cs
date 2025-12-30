// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Represents a detected sleep pattern or insight.
/// </summary>
public class Pattern
{
    /// <summary>
    /// Gets or sets the unique identifier for the pattern.
    /// </summary>
    public Guid PatternId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this pattern.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the pattern name or title.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the pattern.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern type (e.g., Weekly, Monthly, Seasonal).
    /// </summary>
    public string PatternType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the pattern observation.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the pattern observation.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the confidence level of the pattern (0-100).
    /// </summary>
    public int ConfidenceLevel { get; set; }

    /// <summary>
    /// Gets or sets insights or recommendations based on the pattern.
    /// </summary>
    public string? Insights { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the pattern has high confidence (>70%).
    /// </summary>
    /// <returns>True if high confidence; otherwise, false.</returns>
    public bool IsHighConfidence()
    {
        return ConfidenceLevel > 70;
    }

    /// <summary>
    /// Gets the duration of the pattern observation in days.
    /// </summary>
    /// <returns>The duration in days.</returns>
    public int GetDuration()
    {
        return (EndDate - StartDate).Days;
    }
}
