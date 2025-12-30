// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningRoutineBuilder.Api.Features.RoutineTasks;
using MorningRoutineBuilder.Api.Features.RoutineTasks.Commands;
using MorningRoutineBuilder.Api.Features.RoutineTasks.Queries;

namespace MorningRoutineBuilder.Api.Controllers;

/// <summary>
/// Controller for managing routine tasks.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoutineTasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoutineTasksController> _logger;

    public RoutineTasksController(IMediator mediator, ILogger<RoutineTasksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all routine tasks.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<RoutineTaskDto>>> GetRoutineTasks([FromQuery] Guid? routineId = null)
    {
        var query = new GetRoutineTasks { RoutineId = routineId };
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }

    /// <summary>
    /// Gets a routine task by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoutineTaskDto>> GetRoutineTask(Guid id)
    {
        var query = new GetRoutineTaskById { RoutineTaskId = id };
        var task = await _mediator.Send(query);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    /// <summary>
    /// Creates a new routine task.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RoutineTaskDto>> CreateRoutineTask([FromBody] CreateRoutineTaskRequest request)
    {
        var command = new CreateRoutineTask
        {
            RoutineId = request.RoutineId,
            Name = request.Name,
            TaskType = request.TaskType,
            Description = request.Description,
            EstimatedDurationMinutes = request.EstimatedDurationMinutes,
            SortOrder = request.SortOrder,
            IsOptional = request.IsOptional
        };

        var task = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoutineTask), new { id = task.RoutineTaskId }, task);
    }

    /// <summary>
    /// Updates an existing routine task.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<RoutineTaskDto>> UpdateRoutineTask(Guid id, [FromBody] UpdateRoutineTaskRequest request)
    {
        try
        {
            var command = new UpdateRoutineTask
            {
                RoutineTaskId = id,
                Name = request.Name,
                TaskType = request.TaskType,
                Description = request.Description,
                EstimatedDurationMinutes = request.EstimatedDurationMinutes,
                SortOrder = request.SortOrder,
                IsOptional = request.IsOptional
            };

            var task = await _mediator.Send(command);
            return Ok(task);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a routine task.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoutineTask(Guid id)
    {
        try
        {
            var command = new DeleteRoutineTask { RoutineTaskId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
