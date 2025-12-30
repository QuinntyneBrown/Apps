// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Data transfer object for updating a Member.
/// </summary>
public class UpdateMemberDto
{
    /// <summary>
    /// Gets or sets the name of the member.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the member.
    /// </summary>
    public string? Email { get; set; }
}
