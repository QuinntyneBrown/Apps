using VehicleValueTracker.Api.Features.Vehicles;
using VehicleValueTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VehicleValueTracker.Api.Controllers;

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

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles(
        [FromQuery] string? make,
        [FromQuery] string? model,
        [FromQuery] int? year,
        [FromQuery] bool? isCurrentlyOwned)
    {
        _logger.LogInformation("Getting vehicles");

        var result = await _mediator.Send(new GetVehiclesQuery
        {
            Make = make,
            Model = model,
            Year = year,
            IsCurrentlyOwned = isCurrentlyOwned,
        });

        return Ok(result);
    }

    [HttpGet("{vehicleId:guid}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleDto>> GetVehicleById(Guid vehicleId)
    {
        _logger.LogInformation("Getting vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new GetVehicleByIdQuery { VehicleId = vehicleId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleDto>> CreateVehicle([FromBody] CreateVehicleCommand command)
    {
        _logger.LogInformation("Creating vehicle: {Year} {Make} {Model}", command.Year, command.Make, command.Model);

        var result = await _mediator.Send(command);

        return Created($"/api/vehicles/{result.VehicleId}", result);
    }

    [HttpPut("{vehicleId:guid}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleDto>> UpdateVehicle(Guid vehicleId, [FromBody] UpdateVehicleCommand command)
    {
        if (vehicleId != command.VehicleId)
        {
            return BadRequest("Vehicle ID mismatch");
        }

        _logger.LogInformation("Updating vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{vehicleId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
    {
        _logger.LogInformation("Deleting vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new DeleteVehicleCommand { VehicleId = vehicleId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
