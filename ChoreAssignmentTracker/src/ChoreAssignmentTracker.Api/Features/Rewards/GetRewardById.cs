// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Query to get a reward by ID.
/// </summary>
public class GetRewardById : IRequest<RewardDto?>
{
    /// <summary>
    /// Gets or sets the reward ID.
    /// </summary>
    public Guid RewardId { get; set; }
}

/// <summary>
/// Handler for GetRewardById query.
/// </summary>
public class GetRewardByIdHandler : IRequestHandler<GetRewardById, RewardDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRewardByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRewardByIdHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetRewardById query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The reward DTO or null if not found.</returns>
    public async Task<RewardDto?> Handle(GetRewardById request, CancellationToken cancellationToken)
    {
        var reward = await _context.Rewards
            .FirstOrDefaultAsync(r => r.RewardId == request.RewardId, cancellationToken);

        if (reward == null)
        {
            return null;
        }

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
