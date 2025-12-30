// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Api.Features.Schedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsActivitySportsTracker.Api.Controllers;

/// <summary>
/// Controller for managing schedules.
/// </summary>
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

    /// <summary>
    /// Gets all schedules.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ScheduleDto>>> GetSchedules([FromQuery] Guid? activityId)
    {
        try
        {
            var query = new GetSchedulesQuery { ActivityId = activityId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting schedules");
            return StatusCode(500, "An error occurred while retrieving schedules");
        }
    }

    /// <summary>
    /// Gets a schedule by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleDto>> GetScheduleById(Guid id)
    {
        try
        {
            var query = new GetScheduleByIdQuery { ScheduleId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Schedule with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting schedule by ID {ScheduleId}", id);
            return StatusCode(500, "An error occurred while retrieving the schedule");
        }
    }

    /// <summary>
    /// Creates a new schedule.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetScheduleById), new { id = result.ScheduleId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating schedule");
            return StatusCode(500, "An error occurred while creating the schedule");
        }
    }

    /// <summary>
    /// Updates an existing schedule.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleDto>> UpdateSchedule(Guid id, [FromBody] UpdateScheduleCommand command)
    {
        try
        {
            if (id != command.ScheduleId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Schedule not found for update: {ScheduleId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating schedule {ScheduleId}", id);
            return StatusCode(500, "An error occurred while updating the schedule");
        }
    }

    /// <summary>
    /// Deletes a schedule.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSchedule(Guid id)
    {
        try
        {
            var command = new DeleteScheduleCommand { ScheduleId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Schedule not found for deletion: {ScheduleId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting schedule {ScheduleId}", id);
            return StatusCode(500, "An error occurred while deleting the schedule");
        }
    }
}
