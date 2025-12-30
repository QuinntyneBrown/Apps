// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.Distraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FocusSessionTracker.Api.Controllers;

/// <summary>
/// Controller for managing distractions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DistractionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DistractionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DistractionsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public DistractionsController(IMediator mediator, ILogger<DistractionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets distractions with optional filters.
    /// </summary>
    /// <param name="focusSessionId">Optional session ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of distractions.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DistractionDto>>> GetDistractions(
        [FromQuery] Guid? focusSessionId,
        CancellationToken cancellationToken)
    {
        var query = new GetDistractionsQuery { FocusSessionId = focusSessionId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new distraction.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created distraction.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistractionDto>> CreateDistraction(
        [FromBody] CreateDistractionCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetDistractions), new { focusSessionId = result.FocusSessionId }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating distraction");
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a distraction.
    /// </summary>
    /// <param name="id">The distraction ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDistraction(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteDistractionCommand { DistractionId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
