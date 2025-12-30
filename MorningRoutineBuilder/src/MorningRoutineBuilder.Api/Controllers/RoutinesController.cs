// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningRoutineBuilder.Api.Features.Routines;
using MorningRoutineBuilder.Api.Features.Routines.Commands;
using MorningRoutineBuilder.Api.Features.Routines.Queries;

namespace MorningRoutineBuilder.Api.Controllers;

/// <summary>
/// Controller for managing routines.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoutinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoutinesController> _logger;

    public RoutinesController(IMediator mediator, ILogger<RoutinesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all routines.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<RoutineDto>>> GetRoutines([FromQuery] Guid? userId = null)
    {
        var query = new GetRoutines { UserId = userId };
        var routines = await _mediator.Send(query);
        return Ok(routines);
    }

    /// <summary>
    /// Gets a routine by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoutineDto>> GetRoutine(Guid id)
    {
        var query = new GetRoutineById { RoutineId = id };
        var routine = await _mediator.Send(query);

        if (routine == null)
        {
            return NotFound();
        }

        return Ok(routine);
    }

    /// <summary>
    /// Creates a new routine.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RoutineDto>> CreateRoutine([FromBody] CreateRoutineRequest request)
    {
        var command = new CreateRoutine
        {
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            TargetStartTime = request.TargetStartTime,
            EstimatedDurationMinutes = request.EstimatedDurationMinutes
        };

        var routine = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoutine), new { id = routine.RoutineId }, routine);
    }

    /// <summary>
    /// Updates an existing routine.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<RoutineDto>> UpdateRoutine(Guid id, [FromBody] UpdateRoutineRequest request)
    {
        try
        {
            var command = new UpdateRoutine
            {
                RoutineId = id,
                Name = request.Name,
                Description = request.Description,
                TargetStartTime = request.TargetStartTime,
                EstimatedDurationMinutes = request.EstimatedDurationMinutes,
                IsActive = request.IsActive
            };

            var routine = await _mediator.Send(command);
            return Ok(routine);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a routine.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoutine(Guid id)
    {
        try
        {
            var command = new DeleteRoutine { RoutineId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
