// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Api.Features.Rewards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChoreAssignmentTracker.Api.Controllers;

/// <summary>
/// Controller for managing rewards.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RewardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RewardsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RewardsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RewardsController(IMediator mediator, ILogger<RewardsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all rewards.
    /// </summary>
    /// <param name="userId">Optional user ID to filter by.</param>
    /// <param name="isAvailable">Optional available status to filter by.</param>
    /// <returns>A list of rewards.</returns>
    [HttpGet]
    public async Task<ActionResult<List<RewardDto>>> GetRewards([FromQuery] Guid? userId, [FromQuery] bool? isAvailable)
    {
        _logger.LogInformation("Getting rewards");

        var result = await _mediator.Send(new GetRewards { UserId = userId, IsAvailable = isAvailable });
        return Ok(result);
    }

    /// <summary>
    /// Gets a reward by ID.
    /// </summary>
    /// <param name="id">The reward ID.</param>
    /// <returns>The reward.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RewardDto>> GetReward(Guid id)
    {
        _logger.LogInformation("Getting reward {RewardId}", id);

        var result = await _mediator.Send(new GetRewardById { RewardId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new reward.
    /// </summary>
    /// <param name="dto">The reward data.</param>
    /// <returns>The created reward.</returns>
    [HttpPost]
    public async Task<ActionResult<RewardDto>> CreateReward(CreateRewardDto dto)
    {
        _logger.LogInformation("Creating reward {Name}", dto.Name);

        var result = await _mediator.Send(new CreateReward { Reward = dto });
        return CreatedAtAction(nameof(GetReward), new { id = result.RewardId }, result);
    }

    /// <summary>
    /// Updates a reward.
    /// </summary>
    /// <param name="id">The reward ID.</param>
    /// <param name="dto">The reward data.</param>
    /// <returns>The updated reward.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RewardDto>> UpdateReward(Guid id, UpdateRewardDto dto)
    {
        _logger.LogInformation("Updating reward {RewardId}", id);

        var result = await _mediator.Send(new UpdateReward { RewardId = id, Reward = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a reward.
    /// </summary>
    /// <param name="id">The reward ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReward(Guid id)
    {
        _logger.LogInformation("Deleting reward {RewardId}", id);

        var result = await _mediator.Send(new DeleteReward { RewardId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Redeems a reward for a family member.
    /// </summary>
    /// <param name="id">The reward ID.</param>
    /// <param name="dto">The redemption data.</param>
    /// <returns>The redeemed reward.</returns>
    [HttpPost("{id}/redeem")]
    public async Task<ActionResult<RewardDto>> RedeemReward(Guid id, RedeemRewardDto dto)
    {
        _logger.LogInformation("Redeeming reward {RewardId} for family member {FamilyMemberId}", id, dto.FamilyMemberId);

        var result = await _mediator.Send(new RedeemReward { RewardId = id, RedemptionData = dto });

        if (result == null)
        {
            return BadRequest("Unable to redeem reward. Check if reward exists, is available, and family member has sufficient points.");
        }

        return Ok(result);
    }
}
