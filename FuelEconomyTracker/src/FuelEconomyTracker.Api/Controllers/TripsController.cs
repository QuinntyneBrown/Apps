// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Api.Features.Trips;
using FuelEconomyTracker.Api.Features.Trips.Commands;
using FuelEconomyTracker.Api.Features.Trips.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelEconomyTracker.Api.Controllers;

/// <summary>
/// Controller for managing trips.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripsController> _logger;

    public TripsController(IMediator mediator, ILogger<TripsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all trips.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TripDto>>> GetTrips([FromQuery] Guid? vehicleId = null)
    {
        var query = new GetTrips { VehicleId = vehicleId };
        var trips = await _mediator.Send(query);
        return Ok(trips);
    }

    /// <summary>
    /// Gets a trip by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> GetTrip(Guid id)
    {
        var query = new GetTripById { TripId = id };
        var trip = await _mediator.Send(query);

        if (trip == null)
        {
            return NotFound();
        }

        return Ok(trip);
    }

    /// <summary>
    /// Creates a new trip.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TripDto>> CreateTrip([FromBody] CreateTripRequest request)
    {
        var command = new CreateTrip
        {
            VehicleId = request.VehicleId,
            StartDate = request.StartDate,
            StartOdometer = request.StartOdometer,
            Purpose = request.Purpose,
            StartLocation = request.StartLocation,
            Notes = request.Notes
        };

        var trip = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTrip), new { id = trip.TripId }, trip);
    }

    /// <summary>
    /// Updates an existing trip.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TripDto>> UpdateTrip(Guid id, [FromBody] UpdateTripRequest request)
    {
        try
        {
            var command = new UpdateTrip
            {
                TripId = id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StartOdometer = request.StartOdometer,
                EndOdometer = request.EndOdometer,
                Purpose = request.Purpose,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation,
                AverageMPG = request.AverageMPG,
                Notes = request.Notes
            };

            var trip = await _mediator.Send(command);
            return Ok(trip);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Completes a trip.
    /// </summary>
    [HttpPost("{id}/complete")]
    public async Task<ActionResult<TripDto>> CompleteTrip(Guid id, [FromBody] CompleteTripRequest request)
    {
        try
        {
            var command = new CompleteTrip
            {
                TripId = id,
                EndDate = request.EndDate,
                EndOdometer = request.EndOdometer,
                EndLocation = request.EndLocation
            };

            var trip = await _mediator.Send(command);
            return Ok(trip);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a trip.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(Guid id)
    {
        try
        {
            var command = new DeleteTrip { TripId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
