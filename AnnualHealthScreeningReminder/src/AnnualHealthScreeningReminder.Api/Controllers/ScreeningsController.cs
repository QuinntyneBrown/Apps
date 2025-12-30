// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;
using AnnualHealthScreeningReminder.Api.Features.Screenings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnnualHealthScreeningReminder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreeningsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScreeningsController> _logger;

    public ScreeningsController(IMediator mediator, ILogger<ScreeningsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all screenings, optionally filtered by user ID.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllScreenings.Query(userId), cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all screenings");
            return StatusCode(500, "An error occurred while retrieving screenings.");
        }
    }

    /// <summary>
    /// Gets a screening by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetScreeningById.Query(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Screening not found: {ScreeningId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting screening by ID: {ScreeningId}", id);
            return StatusCode(500, "An error occurred while retrieving the screening.");
        }
    }

    /// <summary>
    /// Creates a new screening.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScreening.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.ScreeningId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error creating screening");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating screening");
            return StatusCode(500, "An error occurred while creating the screening.");
        }
    }

    /// <summary>
    /// Updates an existing screening.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScreening.Command command, CancellationToken cancellationToken)
    {
        if (id != command.ScreeningId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Screening not found: {ScreeningId}", id);
            return NotFound(ex.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error updating screening");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating screening: {ScreeningId}", id);
            return StatusCode(500, "An error occurred while updating the screening.");
        }
    }

    /// <summary>
    /// Deletes a screening.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteScreening.Command(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Screening not found: {ScreeningId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting screening: {ScreeningId}", id);
            return StatusCode(500, "An error occurred while deleting the screening.");
        }
    }
}
