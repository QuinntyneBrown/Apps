// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents an achievement during a review period.
/// </summary>
public class Achievement
{
    /// <summary>
    /// Gets or sets the unique identifier for the achievement.
    /// </summary>
    public Guid AchievementId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this achievement.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; set; }

    /// <summary>
    /// Gets or sets the title of the achievement.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date achieved.
    /// </summary>
    public DateTime AchievedDate { get; set; }

    /// <summary>
    /// Gets or sets the impact or result.
    /// </summary>
    public string? Impact { get; set; }

    /// <summary>
    /// Gets or sets the category (e.g., Technical, Leadership, Innovation).
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a key achievement.
    /// </summary>
    public bool IsKeyAchievement { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the review period.
    /// </summary>
    public ReviewPeriod? ReviewPeriod { get; set; }

    /// <summary>
    /// Marks the achievement as a key achievement.
    /// </summary>
    public void MarkAsKey()
    {
        IsKeyAchievement = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
