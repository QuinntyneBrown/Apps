// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Api.Features.Goals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouplesGoalTracker.Api.Controllers;

/// <summary>
/// Controller for managing goals.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GoalsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GoalsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public GoalsController(IMediator mediator, ILogger<GoalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all goals.
    /// </summary>
    /// <param name="userId">Optional user ID to filter by.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of goals.</returns>
    [HttpGet]
    public async Task<ActionResult<List<GoalDto>>> GetAll([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all goals for user {UserId}", userId);
        var query = new GetAllGoalsQuery { UserId = userId };
        var goals = await _mediator.Send(query, cancellationToken);
        return Ok(goals);
    }

    /// <summary>
    /// Gets a goal by ID.
    /// </summary>
    /// <param name="id">The goal ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The goal.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting goal {GoalId}", id);
        var query = new GetGoalByIdQuery { GoalId = id };
        var goal = await _mediator.Send(query, cancellationToken);

        if (goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    /// <summary>
    /// Creates a new goal.
    /// </summary>
    /// <param name="command">The create goal command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created goal.</returns>
    [HttpPost]
    public async Task<ActionResult<GoalDto>> Create([FromBody] CreateGoalCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating goal for user {UserId}", command.UserId);
        var goal = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = goal.GoalId }, goal);
    }

    /// <summary>
    /// Updates an existing goal.
    /// </summary>
    /// <param name="id">The goal ID.</param>
    /// <param name="command">The update goal command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated goal.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<GoalDto>> Update(Guid id, [FromBody] UpdateGoalCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating goal {GoalId}", id);
        command.GoalId = id;
        var goal = await _mediator.Send(command, cancellationToken);

        if (goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    /// <summary>
    /// Deletes a goal.
    /// </summary>
    /// <param name="id">The goal ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting goal {GoalId}", id);
        var command = new DeleteGoalCommand { GoalId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
