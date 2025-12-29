using FamilyCalendarEventPlanner.Api.Features.Reminders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<EventReminderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EventReminderDto>>> GetReminders(
        [FromQuery] Guid? eventId,
        [FromQuery] Guid? recipientId)
    {
        _logger.LogInformation(
            "Getting reminders for event {EventId}, recipient {RecipientId}",
            eventId,
            recipientId);

        var result = await _mediator.Send(new GetRemindersQuery
        {
            EventId = eventId,
            RecipientId = recipientId,
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EventReminderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventReminderDto>> CreateReminder([FromBody] CreateReminderCommand command)
    {
        _logger.LogInformation(
            "Creating reminder for event {EventId}, recipient {RecipientId}",
            command.EventId,
            command.RecipientId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    [HttpPut("{reminderId:guid}/reschedule")]
    [ProducesResponseType(typeof(EventReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventReminderDto>> RescheduleReminder(Guid reminderId, [FromBody] RescheduleReminderCommand command)
    {
        if (reminderId != command.ReminderId)
        {
            return BadRequest("Reminder ID mismatch");
        }

        _logger.LogInformation(
            "Rescheduling reminder {ReminderId} to {ReminderTime}",
            reminderId,
            command.ReminderTime);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{reminderId:guid}/send")]
    [ProducesResponseType(typeof(EventReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventReminderDto>> SendReminder(Guid reminderId)
    {
        _logger.LogInformation("Marking reminder {ReminderId} as sent", reminderId);

        var result = await _mediator.Send(new SendReminderCommand { ReminderId = reminderId });

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
}
