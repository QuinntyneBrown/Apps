using DeliverySchedules.Api.Features.DeliverySchedules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySchedules.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliverySchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeliverySchedulesController> _logger;

    public DeliverySchedulesController(IMediator mediator, ILogger<DeliverySchedulesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeliveryScheduleDto>>> GetDeliverySchedules([FromQuery] Guid? letterId = null)
    {
        _logger.LogInformation("Getting delivery schedules");
        var result = await _mediator.Send(new GetDeliverySchedulesQuery { LetterId = letterId });
        return Ok(result);
    }

    [HttpGet("{scheduleId:guid}")]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryScheduleDto>> GetDeliveryScheduleById(Guid scheduleId)
    {
        _logger.LogInformation("Getting delivery schedule {ScheduleId}", scheduleId);
        var result = await _mediator.Send(new GetDeliveryScheduleByIdQuery { DeliveryScheduleId = scheduleId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeliveryScheduleDto>> CreateDeliverySchedule([FromBody] CreateDeliveryScheduleCommand command)
    {
        _logger.LogInformation("Creating delivery schedule for letter: {LetterId}", command.LetterId);
        var result = await _mediator.Send(command);
        return Created($"/api/deliveryschedules/{result.DeliveryScheduleId}", result);
    }

    [HttpPut("{scheduleId:guid}")]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryScheduleDto>> UpdateDeliverySchedule(Guid scheduleId, [FromBody] UpdateDeliveryScheduleCommand command)
    {
        if (scheduleId != command.DeliveryScheduleId) return BadRequest("Schedule ID mismatch");
        _logger.LogInformation("Updating delivery schedule {ScheduleId}", scheduleId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{scheduleId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeliverySchedule(Guid scheduleId)
    {
        _logger.LogInformation("Deleting delivery schedule {ScheduleId}", scheduleId);
        var result = await _mediator.Send(new DeleteDeliveryScheduleCommand { DeliveryScheduleId = scheduleId });
        if (!result) return NotFound();
        return NoContent();
    }
}
