// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Api.Features.FillUps;
using FuelEconomyTracker.Api.Features.FillUps.Commands;
using FuelEconomyTracker.Api.Features.FillUps.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelEconomyTracker.Api.Controllers;

/// <summary>
/// Controller for managing fill-ups.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FillUpsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FillUpsController> _logger;

    public FillUpsController(IMediator mediator, ILogger<FillUpsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all fill-ups.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<FillUpDto>>> GetFillUps([FromQuery] Guid? vehicleId = null)
    {
        var query = new GetFillUps { VehicleId = vehicleId };
        var fillUps = await _mediator.Send(query);
        return Ok(fillUps);
    }

    /// <summary>
    /// Gets a fill-up by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<FillUpDto>> GetFillUp(Guid id)
    {
        var query = new GetFillUpById { FillUpId = id };
        var fillUp = await _mediator.Send(query);

        if (fillUp == null)
        {
            return NotFound();
        }

        return Ok(fillUp);
    }

    /// <summary>
    /// Creates a new fill-up.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<FillUpDto>> CreateFillUp([FromBody] CreateFillUpRequest request)
    {
        var command = new CreateFillUp
        {
            VehicleId = request.VehicleId,
            FillUpDate = request.FillUpDate,
            Odometer = request.Odometer,
            Gallons = request.Gallons,
            PricePerGallon = request.PricePerGallon,
            IsFullTank = request.IsFullTank,
            FuelGrade = request.FuelGrade,
            GasStation = request.GasStation,
            Notes = request.Notes
        };

        var fillUp = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFillUp), new { id = fillUp.FillUpId }, fillUp);
    }

    /// <summary>
    /// Updates an existing fill-up.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<FillUpDto>> UpdateFillUp(Guid id, [FromBody] UpdateFillUpRequest request)
    {
        try
        {
            var command = new UpdateFillUp
            {
                FillUpId = id,
                FillUpDate = request.FillUpDate,
                Odometer = request.Odometer,
                Gallons = request.Gallons,
                PricePerGallon = request.PricePerGallon,
                IsFullTank = request.IsFullTank,
                FuelGrade = request.FuelGrade,
                GasStation = request.GasStation,
                Notes = request.Notes
            };

            var fillUp = await _mediator.Send(command);
            return Ok(fillUp);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a fill-up.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFillUp(Guid id)
    {
        try
        {
            var command = new DeleteFillUp { FillUpId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
