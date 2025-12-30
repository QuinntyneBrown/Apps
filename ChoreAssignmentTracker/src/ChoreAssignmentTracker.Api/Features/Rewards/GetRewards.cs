// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Query to get all rewards.
/// </summary>
public class GetRewards : IRequest<List<RewardDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter by.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include only available rewards.
    /// </summary>
    public bool? IsAvailable { get; set; }
}

/// <summary>
/// Handler for GetRewards query.
/// </summary>
public class GetRewardsHandler : IRequestHandler<GetRewards, List<RewardDto>>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRewardsHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRewardsHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetRewards query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of reward DTOs.</returns>
    public async Task<List<RewardDto>> Handle(GetRewards request, CancellationToken cancellationToken)
    {
        var query = _context.Rewards.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.IsAvailable.HasValue)
        {
            query = query.Where(r => r.IsAvailable == request.IsAvailable.Value);
        }

        var rewards = await query.ToListAsync(cancellationToken);

        return rewards.Select(r => new RewardDto
        {
            RewardId = r.RewardId,
            UserId = r.UserId,
            Name = r.Name,
            Description = r.Description,
            PointCost = r.PointCost,
            Category = r.Category,
            IsAvailable = r.IsAvailable,
            RedeemedByFamilyMemberId = r.RedeemedByFamilyMemberId,
            RedeemedDate = r.RedeemedDate,
            CreatedAt = r.CreatedAt
        }).ToList();
    }
}
