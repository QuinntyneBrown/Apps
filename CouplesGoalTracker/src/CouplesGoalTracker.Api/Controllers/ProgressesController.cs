// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Api.Features.Progresses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouplesGoalTracker.Api.Controllers;

/// <summary>
/// Controller for managing progress entries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProgressesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProgressesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ProgressesController(IMediator mediator, ILogger<ProgressesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all progress entries for a goal.
    /// </summary>
    /// <param name="goalId">The goal ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of progress entries.</returns>
    [HttpGet("by-goal/{goalId}")]
    public async Task<ActionResult<List<ProgressDto>>> GetByGoal(Guid goalId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting progress entries for goal {GoalId}", goalId);
        var query = new GetProgressesByGoalQuery { GoalId = goalId };
        var progresses = await _mediator.Send(query, cancellationToken);
        return Ok(progresses);
    }

    /// <summary>
    /// Gets a progress entry by ID.
    /// </summary>
    /// <param name="id">The progress ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The progress entry.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgressDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting progress entry {ProgressId}", id);
        var query = new GetProgressByIdQuery { ProgressId = id };
        var progress = await _mediator.Send(query, cancellationToken);

        if (progress == null)
        {
            return NotFound();
        }

        return Ok(progress);
    }

    /// <summary>
    /// Creates a new progress entry.
    /// </summary>
    /// <param name="command">The create progress command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created progress entry.</returns>
    [HttpPost]
    public async Task<ActionResult<ProgressDto>> Create([FromBody] CreateProgressCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating progress entry for goal {GoalId}", command.GoalId);
        var progress = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = progress.ProgressId }, progress);
    }

    /// <summary>
    /// Updates an existing progress entry.
    /// </summary>
    /// <param name="id">The progress ID.</param>
    /// <param name="command">The update progress command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated progress entry.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProgressDto>> Update(Guid id, [FromBody] UpdateProgressCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating progress entry {ProgressId}", id);
        command.ProgressId = id;
        var progress = await _mediator.Send(command, cancellationToken);

        if (progress == null)
        {
            return NotFound();
        }

        return Ok(progress);
    }

    /// <summary>
    /// Deletes a progress entry.
    /// </summary>
    /// <param name="id">The progress ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting progress entry {ProgressId}", id);
        var command = new DeleteProgressCommand { ProgressId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
