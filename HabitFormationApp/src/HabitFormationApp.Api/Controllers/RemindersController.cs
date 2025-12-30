using HabitFormationApp.Api.Features.Reminders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HabitFormationApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RemindersController> _logger;

    public RemindersController(IMediator mediator, ILogger<RemindersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReminderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetReminders(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? habitId,
        [FromQuery] bool? isEnabled)
    {
        _logger.LogInformation("Getting reminders for user {UserId}", userId);

        var result = await _mediator.Send(new GetRemindersQuery
        {
            UserId = userId,
            HabitId = habitId,
            IsEnabled = isEnabled,
        });

        return Ok(result);
    }

    [HttpGet("{reminderId:guid}")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> GetReminderById(Guid reminderId)
    {
        _logger.LogInformation("Getting reminder {ReminderId}", reminderId);

        var result = await _mediator.Send(new GetReminderByIdQuery { ReminderId = reminderId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReminderDto>> CreateReminder([FromBody] CreateReminderCommand command)
    {
        _logger.LogInformation("Creating reminder for habit {HabitId}", command.HabitId);

        var result = await _mediator.Send(command);

        return Created($"/api/reminders/{result.ReminderId}", result);
    }

    [HttpPut("{reminderId:guid}")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> UpdateReminder(Guid reminderId, [FromBody] UpdateReminderCommand command)
    {
        if (reminderId != command.ReminderId)
        {
            return BadRequest("Reminder ID mismatch");
        }

        _logger.LogInformation("Updating reminder {ReminderId}", reminderId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{reminderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReminder(Guid reminderId)
    {
        _logger.LogInformation("Deleting reminder {ReminderId}", reminderId);

        var result = await _mediator.Send(new DeleteReminderCommand { ReminderId = reminderId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{reminderId:guid}/toggle")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> ToggleReminder(Guid reminderId)
    {
        _logger.LogInformation("Toggling reminder {ReminderId}", reminderId);

        var result = await _mediator.Send(new ToggleReminderCommand { ReminderId = reminderId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
