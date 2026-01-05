// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents a men's discussion group.
/// </summary>
public class Group
{
    /// <summary>
    /// Gets or sets the unique identifier for the group.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this group.
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the group.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the meeting schedule (e.g., "Weekly on Tuesdays").
    /// </summary>
    public string? MeetingSchedule { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the group is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of meetings for this group.
    /// </summary>
    public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

    /// <summary>
    /// Gets or sets the collection of resources for this group.
    /// </summary>
    public ICollection<Resource> Resources { get; set; } = new List<Resource>();

    /// <summary>
    /// Deactivates the group.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
