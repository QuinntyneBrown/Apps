// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanBuilder.Api.Features.ProgressRecords;

namespace WorkoutPlanBuilder.Api.Controllers;

/// <summary>
/// Controller for managing progress records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProgressRecordsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProgressRecordsController> _logger;

    public ProgressRecordsController(IMediator mediator, ILogger<ProgressRecordsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all progress records.
    /// </summary>
    /// <returns>A list of progress records.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ProgressRecordDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProgressRecordDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all progress records");
        var progressRecords = await _mediator.Send(new GetAllProgressRecordsQuery(), cancellationToken);
        return Ok(progressRecords);
    }

    /// <summary>
    /// Gets a progress record by ID.
    /// </summary>
    /// <param name="id">The progress record ID.</param>
    /// <returns>The progress record.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProgressRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgressRecordDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting progress record with ID: {ProgressRecordId}", id);
        var progressRecord = await _mediator.Send(new GetProgressRecordByIdQuery { ProgressRecordId = id }, cancellationToken);

        if (progressRecord == null)
        {
            _logger.LogWarning("Progress record with ID {ProgressRecordId} not found", id);
            return NotFound();
        }

        return Ok(progressRecord);
    }

    /// <summary>
    /// Creates a new progress record.
    /// </summary>
    /// <param name="command">The create progress record command.</param>
    /// <returns>The created progress record.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProgressRecordDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProgressRecordDto>> Create([FromBody] CreateProgressRecordCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new progress record for workout: {WorkoutId}", command.WorkoutId);
        var progressRecord = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = progressRecord.ProgressRecordId }, progressRecord);
    }

    /// <summary>
    /// Updates an existing progress record.
    /// </summary>
    /// <param name="id">The progress record ID.</param>
    /// <param name="command">The update progress record command.</param>
    /// <returns>The updated progress record.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProgressRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgressRecordDto>> Update(Guid id, [FromBody] UpdateProgressRecordCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ProgressRecordId)
        {
            _logger.LogWarning("Route ID {RouteId} does not match command ID {CommandId}", id, command.ProgressRecordId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating progress record with ID: {ProgressRecordId}", id);
        var progressRecord = await _mediator.Send(command, cancellationToken);

        if (progressRecord == null)
        {
            _logger.LogWarning("Progress record with ID {ProgressRecordId} not found", id);
            return NotFound();
        }

        return Ok(progressRecord);
    }

    /// <summary>
    /// Deletes a progress record.
    /// </summary>
    /// <param name="id">The progress record ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting progress record with ID: {ProgressRecordId}", id);
        var result = await _mediator.Send(new DeleteProgressRecordCommand { ProgressRecordId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Progress record with ID {ProgressRecordId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
