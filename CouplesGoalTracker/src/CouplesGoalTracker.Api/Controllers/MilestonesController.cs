// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Api.Features.Milestones;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouplesGoalTracker.Api.Controllers;

/// <summary>
/// Controller for managing milestones.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MilestonesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MilestonesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MilestonesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public MilestonesController(IMediator mediator, ILogger<MilestonesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all milestones for a goal.
    /// </summary>
    /// <param name="goalId">The goal ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of milestones.</returns>
    [HttpGet("by-goal/{goalId}")]
    public async Task<ActionResult<List<MilestoneDto>>> GetByGoal(Guid goalId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestones for goal {GoalId}", goalId);
        var query = new GetMilestonesByGoalQuery { GoalId = goalId };
        var milestones = await _mediator.Send(query, cancellationToken);
        return Ok(milestones);
    }

    /// <summary>
    /// Gets a milestone by ID.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The milestone.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<MilestoneDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestone {MilestoneId}", id);
        var query = new GetMilestoneByIdQuery { MilestoneId = id };
        var milestone = await _mediator.Send(query, cancellationToken);

        if (milestone == null)
        {
            return NotFound();
        }

        return Ok(milestone);
    }

    /// <summary>
    /// Creates a new milestone.
    /// </summary>
    /// <param name="command">The create milestone command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created milestone.</returns>
    [HttpPost]
    public async Task<ActionResult<MilestoneDto>> Create([FromBody] CreateMilestoneCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating milestone for goal {GoalId}", command.GoalId);
        var milestone = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = milestone.MilestoneId }, milestone);
    }

    /// <summary>
    /// Updates an existing milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="command">The update milestone command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated milestone.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<MilestoneDto>> Update(Guid id, [FromBody] UpdateMilestoneCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating milestone {MilestoneId}", id);
        command.MilestoneId = id;
        var milestone = await _mediator.Send(command, cancellationToken);

        if (milestone == null)
        {
            return NotFound();
        }

        return Ok(milestone);
    }

    /// <summary>
    /// Deletes a milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting milestone {MilestoneId}", id);
        var command = new DeleteMilestoneCommand { MilestoneId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
