// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Command to update a reward.
/// </summary>
public class UpdateReward : IRequest<RewardDto?>
{
    /// <summary>
    /// Gets or sets the reward ID.
    /// </summary>
    public Guid RewardId { get; set; }

    /// <summary>
    /// Gets or sets the reward data.
    /// </summary>
    public UpdateRewardDto Reward { get; set; } = null!;
}

/// <summary>
/// Handler for UpdateReward command.
/// </summary>
public class UpdateRewardHandler : IRequestHandler<UpdateReward, RewardDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRewardHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateRewardHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the UpdateReward command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated reward DTO or null if not found.</returns>
    public async Task<RewardDto?> Handle(UpdateReward request, CancellationToken cancellationToken)
    {
        var reward = await _context.Rewards
            .FirstOrDefaultAsync(r => r.RewardId == request.RewardId, cancellationToken);

        if (reward == null)
        {
            return null;
        }

        reward.Name = request.Reward.Name;
        reward.Description = request.Reward.Description;
        reward.PointCost = request.Reward.PointCost;
        reward.Category = request.Reward.Category;
        reward.IsAvailable = request.Reward.IsAvailable;

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
