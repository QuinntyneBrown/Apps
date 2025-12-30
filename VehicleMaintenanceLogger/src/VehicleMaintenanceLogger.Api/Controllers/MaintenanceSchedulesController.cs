using VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;
using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenanceLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MaintenanceSchedulesController> _logger;

    public MaintenanceSchedulesController(IMediator mediator, ILogger<MaintenanceSchedulesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaintenanceScheduleDto>>> GetMaintenanceSchedules(
        [FromQuery] Guid? vehicleId,
        [FromQuery] ServiceType? serviceType,
        [FromQuery] bool? isActive,
        [FromQuery] bool? isDue,
        [FromQuery] decimal? currentMileage)
    {
        _logger.LogInformation("Getting maintenance schedules for vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new GetMaintenanceSchedulesQuery
        {
            VehicleId = vehicleId,
            ServiceType = serviceType,
            IsActive = isActive,
            IsDue = isDue,
            CurrentMileage = currentMileage,
        });

        return Ok(result);
    }

    [HttpGet("{maintenanceScheduleId:guid}")]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceScheduleDto>> GetMaintenanceScheduleById(Guid maintenanceScheduleId)
    {
        _logger.LogInformation("Getting maintenance schedule {MaintenanceScheduleId}", maintenanceScheduleId);

        var result = await _mediator.Send(new GetMaintenanceScheduleByIdQuery { MaintenanceScheduleId = maintenanceScheduleId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceScheduleDto>> CreateMaintenanceSchedule([FromBody] CreateMaintenanceScheduleCommand command)
    {
        _logger.LogInformation("Creating maintenance schedule for vehicle {VehicleId}", command.VehicleId);

        var result = await _mediator.Send(command);

        return Created($"/api/maintenanceschedules/{result.MaintenanceScheduleId}", result);
    }

    [HttpPut("{maintenanceScheduleId:guid}")]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceScheduleDto>> UpdateMaintenanceSchedule(Guid maintenanceScheduleId, [FromBody] UpdateMaintenanceScheduleCommand command)
    {
        if (maintenanceScheduleId != command.MaintenanceScheduleId)
        {
            return BadRequest("Maintenance schedule ID mismatch");
        }

        _logger.LogInformation("Updating maintenance schedule {MaintenanceScheduleId}", maintenanceScheduleId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{maintenanceScheduleId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaintenanceSchedule(Guid maintenanceScheduleId)
    {
        _logger.LogInformation("Deleting maintenance schedule {MaintenanceScheduleId}", maintenanceScheduleId);

        var result = await _mediator.Send(new DeleteMaintenanceScheduleCommand { MaintenanceScheduleId = maintenanceScheduleId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
