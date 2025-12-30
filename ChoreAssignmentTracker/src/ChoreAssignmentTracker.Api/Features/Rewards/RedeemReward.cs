// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Command to redeem a reward.
/// </summary>
public class RedeemReward : IRequest<RewardDto?>
{
    /// <summary>
    /// Gets or sets the reward ID.
    /// </summary>
    public Guid RewardId { get; set; }

    /// <summary>
    /// Gets or sets the redemption data.
    /// </summary>
    public RedeemRewardDto RedemptionData { get; set; } = null!;
}

/// <summary>
/// Handler for RedeemReward command.
/// </summary>
public class RedeemRewardHandler : IRequestHandler<RedeemReward, RewardDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedeemRewardHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public RedeemRewardHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the RedeemReward command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The redeemed reward DTO or null if not found or insufficient points.</returns>
    public async Task<RewardDto?> Handle(RedeemReward request, CancellationToken cancellationToken)
    {
        var reward = await _context.Rewards
            .FirstOrDefaultAsync(r => r.RewardId == request.RewardId, cancellationToken);

        if (reward == null || !reward.IsAvailable)
        {
            return null;
        }

        var familyMember = await _context.FamilyMembers
            .FirstOrDefaultAsync(f => f.FamilyMemberId == request.RedemptionData.FamilyMemberId, cancellationToken);

        if (familyMember == null)
        {
            return null;
        }

        // Check if family member has enough points
        if (!familyMember.RedeemPoints(reward.PointCost))
        {
            return null;
        }

        // Redeem the reward
        reward.Redeem(familyMember.FamilyMemberId);

        await _context.SaveChangesAsync(cancellationToken);

        return new RewardDto
        {
            RewardId = reward.RewardId,
            UserId = reward.UserId,
            Name = reward.Name,
            Description = reward.Description,
            PointCost = reward.PointCost,
            Category = reward.Category,
            IsAvailable = reward.IsAvailable,
            RedeemedByFamilyMemberId = reward.RedeemedByFamilyMemberId,
            RedeemedDate = reward.RedeemedDate,
            CreatedAt = reward.CreatedAt
        };
    }
}
