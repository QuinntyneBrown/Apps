using Reminders.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Reminders.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetReminders(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all reminders");
        var result = await _mediator.Send(new GetRemindersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> GetReminderById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reminder {ReminderId}", id);
        var result = await _mediator.Send(new GetReminderByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ReminderDto>> CreateReminder(
        [FromBody] CreateReminderCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating reminder for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReminderById), new { id = result.ReminderId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReminderDto>> UpdateReminder(
        Guid id,
        [FromBody] UpdateReminderCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ReminderId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating reminder {ReminderId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReminder(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reminder {ReminderId}", id);
        var result = await _mediator.Send(new DeleteReminderCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
