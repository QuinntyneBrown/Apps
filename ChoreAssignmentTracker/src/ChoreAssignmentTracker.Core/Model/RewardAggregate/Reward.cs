// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents a reward that can be earned by completing chores.
/// </summary>
public class Reward
{
    /// <summary>
    /// Gets or sets the unique identifier for the reward.
    /// </summary>
    public Guid RewardId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this reward.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the reward.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the reward.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the point cost to redeem the reward.
    /// </summary>
    public int PointCost { get; set; }

    /// <summary>
    /// Gets or sets the category of the reward.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the reward is currently available.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the family member ID who redeemed this reward.
    /// </summary>
    public Guid? RedeemedByFamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the family member who redeemed this reward.
    /// </summary>
    public FamilyMember? RedeemedByFamilyMember { get; set; }

    /// <summary>
    /// Gets or sets the redemption date.
    /// </summary>
    public DateTime? RedeemedDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Redeems the reward for a family member.
    /// </summary>
    /// <param name="familyMemberId">The family member ID.</param>
    public void Redeem(Guid familyMemberId)
    {
        RedeemedByFamilyMemberId = familyMemberId;
        RedeemedDate = DateTime.UtcNow;
        IsAvailable = false;
    }
}
