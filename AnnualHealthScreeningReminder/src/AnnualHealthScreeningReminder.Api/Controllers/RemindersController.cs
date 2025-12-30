// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;
using AnnualHealthScreeningReminder.Api.Features.Reminders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnnualHealthScreeningReminder.Api.Controllers;

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

    /// <summary>
    /// Gets all reminders, optionally filtered by user ID, screening ID, or sent status.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? userId, [FromQuery] Guid? screeningId, [FromQuery] bool? isSent, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllReminders.Query(userId, screeningId, isSent), cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all reminders");
            return StatusCode(500, "An error occurred while retrieving reminders.");
        }
    }

    /// <summary>
    /// Gets a reminder by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetReminderById.Query(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Reminder not found: {ReminderId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting reminder by ID: {ReminderId}", id);
            return StatusCode(500, "An error occurred while retrieving the reminder.");
        }
    }

    /// <summary>
    /// Creates a new reminder.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReminder.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.ReminderId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error creating reminder");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating reminder");
            return StatusCode(500, "An error occurred while creating the reminder.");
        }
    }

    /// <summary>
    /// Updates an existing reminder.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReminder.Command command, CancellationToken cancellationToken)
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
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Reminder not found: {ReminderId}", id);
            return NotFound(ex.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error updating reminder");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating reminder: {ReminderId}", id);
            return StatusCode(500, "An error occurred while updating the reminder.");
        }
    }

    /// <summary>
    /// Marks a reminder as sent.
    /// </summary>
    [HttpPatch("{id}/mark-sent")]
    public async Task<IActionResult> MarkAsSent(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new MarkReminderAsSent.Command(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Reminder not found: {ReminderId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking reminder as sent: {ReminderId}", id);
            return StatusCode(500, "An error occurred while marking the reminder as sent.");
        }
    }

    /// <summary>
    /// Deletes a reminder.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteReminder.Command(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Reminder not found: {ReminderId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting reminder: {ReminderId}", id);
            return StatusCode(500, "An error occurred while deleting the reminder.");
        }
    }
}
