using HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;
using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceSchedule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceTasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MaintenanceTasksController> _logger;

    public MaintenanceTasksController(IMediator mediator, ILogger<MaintenanceTasksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceTaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaintenanceTaskDto>>> GetMaintenanceTasks(
        [FromQuery] Guid? userId,
        [FromQuery] MaintenanceType? maintenanceType,
        [FromQuery] TaskStatus? status,
        [FromQuery] Guid? contractorId)
    {
        _logger.LogInformation("Getting maintenance tasks for user {UserId}", userId);

        var result = await _mediator.Send(new GetMaintenanceTasksQuery
        {
            UserId = userId,
            MaintenanceType = maintenanceType,
            Status = status,
            ContractorId = contractorId,
        });

        return Ok(result);
    }

    [HttpGet("{maintenanceTaskId:guid}")]
    [ProducesResponseType(typeof(MaintenanceTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceTaskDto>> GetMaintenanceTaskById(Guid maintenanceTaskId)
    {
        _logger.LogInformation("Getting maintenance task {MaintenanceTaskId}", maintenanceTaskId);

        var result = await _mediator.Send(new GetMaintenanceTaskByIdQuery { MaintenanceTaskId = maintenanceTaskId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceTaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceTaskDto>> CreateMaintenanceTask([FromBody] CreateMaintenanceTaskCommand command)
    {
        _logger.LogInformation("Creating maintenance task for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/maintenancetasks/{result.MaintenanceTaskId}", result);
    }

    [HttpPut("{maintenanceTaskId:guid}")]
    [ProducesResponseType(typeof(MaintenanceTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceTaskDto>> UpdateMaintenanceTask(Guid maintenanceTaskId, [FromBody] UpdateMaintenanceTaskCommand command)
    {
        if (maintenanceTaskId != command.MaintenanceTaskId)
        {
            return BadRequest("Maintenance task ID mismatch");
        }

        _logger.LogInformation("Updating maintenance task {MaintenanceTaskId}", maintenanceTaskId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{maintenanceTaskId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaintenanceTask(Guid maintenanceTaskId)
    {
        _logger.LogInformation("Deleting maintenance task {MaintenanceTaskId}", maintenanceTaskId);

        var result = await _mediator.Send(new DeleteMaintenanceTaskCommand { MaintenanceTaskId = maintenanceTaskId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
