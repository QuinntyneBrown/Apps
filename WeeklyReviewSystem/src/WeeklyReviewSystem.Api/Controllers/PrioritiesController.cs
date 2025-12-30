// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeeklyReviewSystem.Api.Features.Priorities;

namespace WeeklyReviewSystem.Api.Controllers;

/// <summary>
/// Controller for managing weekly priorities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PrioritiesController> _logger;

    public PrioritiesController(IMediator mediator, ILogger<PrioritiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all weekly priorities.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<WeeklyPriorityDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all weekly priorities");
        var priorities = await _mediator.Send(new GetAllWeeklyPrioritiesQuery(), cancellationToken);
        return Ok(priorities);
    }

    /// <summary>
    /// Gets a weekly priority by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WeeklyPriorityDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting weekly priority {WeeklyPriorityId}", id);
        var priority = await _mediator.Send(new GetWeeklyPriorityByIdQuery { WeeklyPriorityId = id }, cancellationToken);

        if (priority == null)
        {
            _logger.LogWarning("Weekly priority {WeeklyPriorityId} not found", id);
            return NotFound();
        }

        return Ok(priority);
    }

    /// <summary>
    /// Creates a new weekly priority.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WeeklyPriorityDto>> Create(CreateWeeklyPriorityCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new weekly priority for review {WeeklyReviewId}", command.WeeklyReviewId);
        var priority = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = priority.WeeklyPriorityId }, priority);
    }

    /// <summary>
    /// Updates an existing weekly priority.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<WeeklyPriorityDto>> Update(Guid id, UpdateWeeklyPriorityCommand command, CancellationToken cancellationToken)
    {
        if (id != command.WeeklyPriorityId)
        {
            _logger.LogWarning("ID mismatch: URL {UrlId} != Command {CommandId}", id, command.WeeklyPriorityId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating weekly priority {WeeklyPriorityId}", id);
        var priority = await _mediator.Send(command, cancellationToken);

        if (priority == null)
        {
            _logger.LogWarning("Weekly priority {WeeklyPriorityId} not found", id);
            return NotFound();
        }

        return Ok(priority);
    }

    /// <summary>
    /// Deletes a weekly priority.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting weekly priority {WeeklyPriorityId}", id);
        var result = await _mediator.Send(new DeleteWeeklyPriorityCommand { WeeklyPriorityId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Weekly priority {WeeklyPriorityId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
