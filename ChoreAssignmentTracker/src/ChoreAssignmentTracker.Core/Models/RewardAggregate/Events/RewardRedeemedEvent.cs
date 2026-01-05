// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Event raised when a reward is redeemed.
/// </summary>
public record RewardRedeemedEvent
{
    /// <summary>
    /// Gets the reward ID.
    /// </summary>
    public Guid RewardId { get; init; }

    /// <summary>
    /// Gets the family member ID who redeemed the reward.
    /// </summary>
    public Guid FamilyMemberId { get; init; }

    /// <summary>
    /// Gets the point cost.
    /// </summary>
    public int PointCost { get; init; }

    /// <summary>
    /// Gets the redemption date.
    /// </summary>
    public DateTime RedeemedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
