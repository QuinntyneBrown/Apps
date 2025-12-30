// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Represents a member of a group.
/// </summary>
public class Member
{
    /// <summary>
    /// Gets or sets the unique identifier for the member.
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// Gets or sets the group ID this member belongs to.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the user ID of the member.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the member.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the member.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this member is an admin.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this member is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the date the member joined the group.
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the group this member belongs to.
    /// </summary>
    public Group? Group { get; set; }

    /// <summary>
    /// Gets or sets the collection of RSVPs for this member.
    /// </summary>
    public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();

    /// <summary>
    /// Removes the member from the group.
    /// </summary>
    public void Remove()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Promotes the member to admin.
    /// </summary>
    public void PromoteToAdmin()
    {
        IsAdmin = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
