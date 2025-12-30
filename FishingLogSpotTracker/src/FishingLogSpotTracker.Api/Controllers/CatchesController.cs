// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Features.Catches;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FishingLogSpotTracker.Api.Controllers;

/// <summary>
/// Controller for managing fish catches.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CatchesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatchesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CatchesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public CatchesController(IMediator mediator, ILogger<CatchesController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all catches.
    /// </summary>
    /// <param name="userId">Optional user ID to filter catches.</param>
    /// <param name="tripId">Optional trip ID to filter catches.</param>
    /// <param name="species">Optional species to filter catches.</param>
    /// <returns>List of catches.</returns>
    [HttpGet]
    public async Task<ActionResult<List<CatchDto>>> GetCatches(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? tripId,
        [FromQuery] FishSpecies? species)
    {
        _logger.LogInformation("Getting catches for user {UserId}, trip {TripId}, species {Species}", userId, tripId, species);
        var query = new GetCatchesQuery { UserId = userId, TripId = tripId, Species = species };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a catch by ID.
    /// </summary>
    /// <param name="id">The catch ID.</param>
    /// <returns>The catch.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CatchDto>> GetCatch(Guid id)
    {
        _logger.LogInformation("Getting catch {CatchId}", id);
        var query = new GetCatchByIdQuery { CatchId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Catch {CatchId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new catch.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created catch.</returns>
    [HttpPost]
    public async Task<ActionResult<CatchDto>> CreateCatch([FromBody] CreateCatchCommand command)
    {
        _logger.LogInformation("Creating catch for user {UserId}, trip {TripId}, species {Species}",
            command.UserId, command.TripId, command.Species);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCatch), new { id = result.CatchId }, result);
    }

    /// <summary>
    /// Updates an existing catch.
    /// </summary>
    /// <param name="id">The catch ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated catch.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CatchDto>> UpdateCatch(Guid id, [FromBody] UpdateCatchCommand command)
    {
        if (id != command.CatchId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating catch {CatchId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Catch {CatchId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a catch.
    /// </summary>
    /// <param name="id">The catch ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCatch(Guid id)
    {
        _logger.LogInformation("Deleting catch {CatchId}", id);
        var command = new DeleteCatchCommand { CatchId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            _logger.LogWarning("Catch {CatchId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
