// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// API controller for managing reminders.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RemindersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemindersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RemindersController(IMediator mediator, ILogger<RemindersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Schedules a new reminder.
    /// </summary>
    /// <param name="command">The schedule command.</param>
    /// <returns>The created reminder.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> Schedule([FromBody] ScheduleReminderCommand command)
    {
        _logger.LogInformation(
            "Scheduling reminder for important date {ImportantDateId}",
            command.ImportantDateId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound("Important date not found");
        }

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Dismisses a reminder.
    /// </summary>
    /// <param name="id">The reminder ID.</param>
    /// <returns>The dismissed reminder.</returns>
    [HttpPost("{id:guid}/dismiss")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> Dismiss(Guid id)
    {
        _logger.LogInformation("Dismissing reminder {ReminderId}", id);

        var result = await _mediator.Send(new DismissReminderCommand { ReminderId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Snoozes a reminder.
    /// </summary>
    /// <param name="id">The reminder ID.</param>
    /// <param name="command">The snooze command.</param>
    /// <returns>The snoozed reminder.</returns>
    [HttpPost("{id:guid}/snooze")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReminderDto>> Snooze(Guid id, [FromBody] SnoozeReminderCommand command)
    {
        if (id != command.ReminderId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation(
            "Snoozing reminder {ReminderId} for {SnoozeMinutes} minutes",
            id,
            command.SnoozeMinutes);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
