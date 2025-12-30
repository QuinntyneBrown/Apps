// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Api.Features.Activity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsActivitySportsTracker.Api.Controllers;

/// <summary>
/// Controller for managing activities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(IMediator mediator, ILogger<ActivitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all activities.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetActivities([FromQuery] Guid? userId)
    {
        try
        {
            var query = new GetActivitiesQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting activities");
            return StatusCode(500, "An error occurred while retrieving activities");
        }
    }

    /// <summary>
    /// Gets an activity by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivityById(Guid id)
    {
        try
        {
            var query = new GetActivityByIdQuery { ActivityId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Activity with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting activity by ID {ActivityId}", id);
            return StatusCode(500, "An error occurred while retrieving the activity");
        }
    }

    /// <summary>
    /// Creates a new activity.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ActivityDto>> CreateActivity([FromBody] CreateActivityCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetActivityById), new { id = result.ActivityId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating activity");
            return StatusCode(500, "An error occurred while creating the activity");
        }
    }

    /// <summary>
    /// Updates an existing activity.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ActivityDto>> UpdateActivity(Guid id, [FromBody] UpdateActivityCommand command)
    {
        try
        {
            if (id != command.ActivityId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Activity not found for update: {ActivityId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating activity {ActivityId}", id);
            return StatusCode(500, "An error occurred while updating the activity");
        }
    }

    /// <summary>
    /// Deletes an activity.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(Guid id)
    {
        try
        {
            var command = new DeleteActivityCommand { ActivityId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Activity not found for deletion: {ActivityId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting activity {ActivityId}", id);
            return StatusCode(500, "An error occurred while deleting the activity");
        }
    }
}
