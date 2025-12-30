// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Api.Features.Reminder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydrationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReminderController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReminderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReminderDto>>> GetReminders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRemindersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReminderDto>> GetReminderById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetReminderByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<ReminderDto>> CreateReminder(
        [FromBody] CreateReminderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReminderById), new { id = result.ReminderId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReminderDto>> UpdateReminder(
        Guid id,
        [FromBody] UpdateReminderCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ReminderId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReminder(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteReminderCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
