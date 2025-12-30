// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Api.Features.Vehicles;
using FuelEconomyTracker.Api.Features.Vehicles.Commands;
using FuelEconomyTracker.Api.Features.Vehicles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelEconomyTracker.Api.Controllers;

/// <summary>
/// Controller for managing vehicles.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IMediator mediator, ILogger<VehiclesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all vehicles.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> GetVehicles([FromQuery] bool? isActive = null)
    {
        var query = new GetVehicles { IsActive = isActive };
        var vehicles = await _mediator.Send(query);
        return Ok(vehicles);
    }

    /// <summary>
    /// Gets a vehicle by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> GetVehicle(Guid id)
    {
        var query = new GetVehicleById { VehicleId = id };
        var vehicle = await _mediator.Send(query);

        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<VehicleDto>> CreateVehicle([FromBody] CreateVehicleRequest request)
    {
        var command = new CreateVehicle
        {
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            VIN = request.VIN,
            LicensePlate = request.LicensePlate,
            TankCapacity = request.TankCapacity,
            EPACityMPG = request.EPACityMPG,
            EPAHighwayMPG = request.EPAHighwayMPG
        };

        var vehicle = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleId }, vehicle);
    }

    /// <summary>
    /// Updates an existing vehicle.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleDto>> UpdateVehicle(Guid id, [FromBody] UpdateVehicleRequest request)
    {
        try
        {
            var command = new UpdateVehicle
            {
                VehicleId = id,
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                VIN = request.VIN,
                LicensePlate = request.LicensePlate,
                TankCapacity = request.TankCapacity,
                EPACityMPG = request.EPACityMPG,
                EPAHighwayMPG = request.EPAHighwayMPG,
                IsActive = request.IsActive
            };

            var vehicle = await _mediator.Send(command);
            return Ok(vehicle);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a vehicle.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        try
        {
            var command = new DeleteVehicle { VehicleId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
