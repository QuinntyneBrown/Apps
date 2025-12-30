// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Represents a reward data transfer object.
/// </summary>
public class RewardDto
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
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the family member ID who redeemed this reward.
    /// </summary>
    public Guid? RedeemedByFamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the redemption date.
    /// </summary>
    public DateTime? RedeemedDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Represents a request to create a reward.
/// </summary>
public class CreateRewardDto
{
    /// <summary>
    /// Gets or sets the user ID who owns this reward.
    /// </summary>
    public Guid UserId { get; set; }

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
}

/// <summary>
/// Represents a request to update a reward.
/// </summary>
public class UpdateRewardDto
{
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
    public bool IsAvailable { get; set; }
}

/// <summary>
/// Represents a request to redeem a reward.
/// </summary>
public class RedeemRewardDto
{
    /// <summary>
    /// Gets or sets the family member ID who is redeeming this reward.
    /// </summary>
    public Guid FamilyMemberId { get; set; }
}
