// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Command to create a reward.
/// </summary>
public class CreateReward : IRequest<RewardDto>
{
    /// <summary>
    /// Gets or sets the reward data.
    /// </summary>
    public CreateRewardDto Reward { get; set; } = null!;
}

/// <summary>
/// Handler for CreateReward command.
/// </summary>
public class CreateRewardHandler : IRequestHandler<CreateReward, RewardDto>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRewardHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateRewardHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the CreateReward command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created reward DTO.</returns>
    public async Task<RewardDto> Handle(CreateReward request, CancellationToken cancellationToken)
    {
        var reward = new Reward
        {
            RewardId = Guid.NewGuid(),
            UserId = request.Reward.UserId,
            Name = request.Reward.Name,
            Description = request.Reward.Description,
            PointCost = request.Reward.PointCost,
            Category = request.Reward.Category,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rewards.Add(reward);
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
