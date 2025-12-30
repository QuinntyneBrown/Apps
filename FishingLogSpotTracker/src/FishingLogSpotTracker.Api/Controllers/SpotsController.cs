// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Features.Spots;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FishingLogSpotTracker.Api.Controllers;

/// <summary>
/// Controller for managing fishing spots.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SpotsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SpotsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpotsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public SpotsController(IMediator mediator, ILogger<SpotsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all spots.
    /// </summary>
    /// <param name="userId">Optional user ID to filter spots.</param>
    /// <param name="favoritesOnly">Optional flag to get only favorites.</param>
    /// <returns>List of spots.</returns>
    [HttpGet]
    public async Task<ActionResult<List<SpotDto>>> GetSpots([FromQuery] Guid? userId, [FromQuery] bool? favoritesOnly)
    {
        _logger.LogInformation("Getting spots for user {UserId}, favoritesOnly: {FavoritesOnly}", userId, favoritesOnly);
        var query = new GetSpotsQuery { UserId = userId, FavoritesOnly = favoritesOnly };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a spot by ID.
    /// </summary>
    /// <param name="id">The spot ID.</param>
    /// <returns>The spot.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<SpotDto>> GetSpot(Guid id)
    {
        _logger.LogInformation("Getting spot {SpotId}", id);
        var query = new GetSpotByIdQuery { SpotId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Spot {SpotId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new spot.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created spot.</returns>
    [HttpPost]
    public async Task<ActionResult<SpotDto>> CreateSpot([FromBody] CreateSpotCommand command)
    {
        _logger.LogInformation("Creating spot {SpotName} for user {UserId}", command.Name, command.UserId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSpot), new { id = result.SpotId }, result);
    }

    /// <summary>
    /// Updates an existing spot.
    /// </summary>
    /// <param name="id">The spot ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated spot.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<SpotDto>> UpdateSpot(Guid id, [FromBody] UpdateSpotCommand command)
    {
        if (id != command.SpotId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating spot {SpotId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Spot {SpotId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a spot.
    /// </summary>
    /// <param name="id">The spot ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSpot(Guid id)
    {
        _logger.LogInformation("Deleting spot {SpotId}", id);
        var command = new DeleteSpotCommand { SpotId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            _logger.LogWarning("Spot {SpotId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
