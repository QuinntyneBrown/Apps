// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Data transfer object for creating a Member.
/// </summary>
public class CreateMemberDto
{
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
}
