// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Data transfer object for creating a Group.
/// </summary>
public class CreateGroupDto
{
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
}
