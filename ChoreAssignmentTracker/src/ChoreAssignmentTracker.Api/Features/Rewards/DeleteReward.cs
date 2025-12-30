// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Rewards;

/// <summary>
/// Command to delete a reward.
/// </summary>
public class DeleteReward : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the reward ID.
    /// </summary>
    public Guid RewardId { get; set; }
}

/// <summary>
/// Handler for DeleteReward command.
/// </summary>
public class DeleteRewardHandler : IRequestHandler<DeleteReward, bool>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRewardHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteRewardHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the DeleteReward command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteReward request, CancellationToken cancellationToken)
    {
        var reward = await _context.Rewards
            .FirstOrDefaultAsync(r => r.RewardId == request.RewardId, cancellationToken);

        if (reward == null)
        {
            return false;
        }

        _context.Rewards.Remove(reward);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
