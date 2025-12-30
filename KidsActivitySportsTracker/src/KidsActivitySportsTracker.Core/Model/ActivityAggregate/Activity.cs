// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Represents a kids activity or sport.
/// </summary>
public class Activity
{
    /// <summary>
    /// Gets or sets the unique identifier for the activity.
    /// </summary>
    public Guid ActivityId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this activity.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the child's name.
    /// </summary>
    public string ChildName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the activity.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of activity.
    /// </summary>
    public ActivityType ActivityType { get; set; }

    /// <summary>
    /// Gets or sets the organization or team name.
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Gets or sets the coach or instructor name.
    /// </summary>
    public string? CoachName { get; set; }

    /// <summary>
    /// Gets or sets the coach contact information.
    /// </summary>
    public string? CoachContact { get; set; }

    /// <summary>
    /// Gets or sets the season or session.
    /// </summary>
    public string? Season { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the activity.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of schedules for this activity.
    /// </summary>
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
