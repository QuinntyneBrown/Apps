using Schedules.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Schedules.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SchedulesController> _logger;

    public SchedulesController(IMediator mediator, ILogger<SchedulesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all schedules");
        var result = await _mediator.Send(new GetSchedulesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleDto>> GetScheduleById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting schedule {ScheduleId}", id);
        var result = await _mediator.Send(new GetScheduleByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule(
        [FromBody] CreateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating schedule for activity {ActivityId}", command.ActivityId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetScheduleById), new { id = result.ScheduleId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleDto>> UpdateSchedule(
        Guid id,
        [FromBody] UpdateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ScheduleId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating schedule {ScheduleId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting schedule {ScheduleId}", id);
        var result = await _mediator.Send(new DeleteScheduleCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
