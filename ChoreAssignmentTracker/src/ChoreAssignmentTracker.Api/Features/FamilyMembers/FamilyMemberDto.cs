// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Represents a family member data transfer object.
/// </summary>
public class FamilyMemberDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the family member.
    /// </summary>
    public Guid FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this family member.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the family member.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the age of the family member.
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the avatar or icon identifier.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets the total points earned.
    /// </summary>
    public int TotalPoints { get; set; }

    /// <summary>
    /// Gets or sets the available points (not yet redeemed).
    /// </summary>
    public int AvailablePoints { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the family member is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the completion rate.
    /// </summary>
    public double CompletionRate { get; set; }
}

/// <summary>
/// Represents a request to create a family member.
/// </summary>
public class CreateFamilyMemberDto
{
    /// <summary>
    /// Gets or sets the user ID who owns this family member.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the family member.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the age of the family member.
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the avatar or icon identifier.
    /// </summary>
    public string? Avatar { get; set; }
}

/// <summary>
/// Represents a request to update a family member.
/// </summary>
public class UpdateFamilyMemberDto
{
    /// <summary>
    /// Gets or sets the name of the family member.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the age of the family member.
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the avatar or icon identifier.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the family member is active.
    /// </summary>
    public bool IsActive { get; set; }
}
