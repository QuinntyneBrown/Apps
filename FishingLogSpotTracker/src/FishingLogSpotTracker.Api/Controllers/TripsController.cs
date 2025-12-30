// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Features.Trips;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FishingLogSpotTracker.Api.Controllers;

/// <summary>
/// Controller for managing fishing trips.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TripsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public TripsController(IMediator mediator, ILogger<TripsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all trips.
    /// </summary>
    /// <param name="userId">Optional user ID to filter trips.</param>
    /// <param name="spotId">Optional spot ID to filter trips.</param>
    /// <returns>List of trips.</returns>
    [HttpGet]
    public async Task<ActionResult<List<TripDto>>> GetTrips([FromQuery] Guid? userId, [FromQuery] Guid? spotId)
    {
        _logger.LogInformation("Getting trips for user {UserId}, spot {SpotId}", userId, spotId);
        var query = new GetTripsQuery { UserId = userId, SpotId = spotId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a trip by ID.
    /// </summary>
    /// <param name="id">The trip ID.</param>
    /// <returns>The trip.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> GetTrip(Guid id)
    {
        _logger.LogInformation("Getting trip {TripId}", id);
        var query = new GetTripByIdQuery { TripId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Trip {TripId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new trip.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created trip.</returns>
    [HttpPost]
    public async Task<ActionResult<TripDto>> CreateTrip([FromBody] CreateTripCommand command)
    {
        _logger.LogInformation("Creating trip for user {UserId} at spot {SpotId}", command.UserId, command.SpotId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTrip), new { id = result.TripId }, result);
    }

    /// <summary>
    /// Updates an existing trip.
    /// </summary>
    /// <param name="id">The trip ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated trip.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<TripDto>> UpdateTrip(Guid id, [FromBody] UpdateTripCommand command)
    {
        if (id != command.TripId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating trip {TripId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Trip {TripId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a trip.
    /// </summary>
    /// <param name="id">The trip ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTrip(Guid id)
    {
        _logger.LogInformation("Deleting trip {TripId}", id);
        var command = new DeleteTripCommand { TripId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            _logger.LogWarning("Trip {TripId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
