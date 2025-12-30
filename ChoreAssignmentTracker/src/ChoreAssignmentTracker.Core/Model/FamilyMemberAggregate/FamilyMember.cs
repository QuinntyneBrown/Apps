// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents a family member who can be assigned chores.
/// </summary>
public class FamilyMember
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

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
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of assignments for this family member.
    /// </summary>
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    /// <summary>
    /// Gets or sets the collection of rewards redeemed by this family member.
    /// </summary>
    public ICollection<Reward> RedeemedRewards { get; set; } = new List<Reward>();

    /// <summary>
    /// Awards points to the family member.
    /// </summary>
    /// <param name="points">The points to award.</param>
    public void AwardPoints(int points)
    {
        TotalPoints += points;
        AvailablePoints += points;
    }

    /// <summary>
    /// Redeems points for a reward.
    /// </summary>
    /// <param name="points">The points to redeem.</param>
    /// <returns>True if successful; otherwise, false.</returns>
    public bool RedeemPoints(int points)
    {
        if (AvailablePoints >= points)
        {
            AvailablePoints -= points;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the completion rate for assigned chores.
    /// </summary>
    /// <returns>The completion rate as a percentage.</returns>
    public double GetCompletionRate()
    {
        if (!Assignments.Any())
        {
            return 0;
        }

        var completed = Assignments.Count(a => a.IsCompleted);
        return (double)completed / Assignments.Count * 100;
    }
}
