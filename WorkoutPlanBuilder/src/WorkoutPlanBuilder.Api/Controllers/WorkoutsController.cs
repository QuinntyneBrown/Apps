// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanBuilder.Api.Features.Workouts;

namespace WorkoutPlanBuilder.Api.Controllers;

/// <summary>
/// Controller for managing workouts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WorkoutsController> _logger;

    public WorkoutsController(IMediator mediator, ILogger<WorkoutsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all workouts.
    /// </summary>
    /// <returns>A list of workouts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<WorkoutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WorkoutDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all workouts");
        var workouts = await _mediator.Send(new GetAllWorkoutsQuery(), cancellationToken);
        return Ok(workouts);
    }

    /// <summary>
    /// Gets a workout by ID.
    /// </summary>
    /// <param name="id">The workout ID.</param>
    /// <returns>The workout.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting workout with ID: {WorkoutId}", id);
        var workout = await _mediator.Send(new GetWorkoutByIdQuery { WorkoutId = id }, cancellationToken);

        if (workout == null)
        {
            _logger.LogWarning("Workout with ID {WorkoutId} not found", id);
            return NotFound();
        }

        return Ok(workout);
    }

    /// <summary>
    /// Creates a new workout.
    /// </summary>
    /// <param name="command">The create workout command.</param>
    /// <returns>The created workout.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkoutDto>> Create([FromBody] CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new workout: {WorkoutName}", command.Name);
        var workout = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = workout.WorkoutId }, workout);
    }

    /// <summary>
    /// Updates an existing workout.
    /// </summary>
    /// <param name="id">The workout ID.</param>
    /// <param name="command">The update workout command.</param>
    /// <returns>The updated workout.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDto>> Update(Guid id, [FromBody] UpdateWorkoutCommand command, CancellationToken cancellationToken)
    {
        if (id != command.WorkoutId)
        {
            _logger.LogWarning("Route ID {RouteId} does not match command ID {CommandId}", id, command.WorkoutId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating workout with ID: {WorkoutId}", id);
        var workout = await _mediator.Send(command, cancellationToken);

        if (workout == null)
        {
            _logger.LogWarning("Workout with ID {WorkoutId} not found", id);
            return NotFound();
        }

        return Ok(workout);
    }

    /// <summary>
    /// Deletes a workout.
    /// </summary>
    /// <param name="id">The workout ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting workout with ID: {WorkoutId}", id);
        var result = await _mediator.Send(new DeleteWorkoutCommand { WorkoutId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Workout with ID {WorkoutId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
