// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanBuilder.Api.Features.Exercises;

namespace WorkoutPlanBuilder.Api.Controllers;

/// <summary>
/// Controller for managing exercises.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExercisesController> _logger;

    public ExercisesController(IMediator mediator, ILogger<ExercisesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all exercises.
    /// </summary>
    /// <returns>A list of exercises.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ExerciseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ExerciseDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all exercises");
        var exercises = await _mediator.Send(new GetAllExercisesQuery(), cancellationToken);
        return Ok(exercises);
    }

    /// <summary>
    /// Gets an exercise by ID.
    /// </summary>
    /// <param name="id">The exercise ID.</param>
    /// <returns>The exercise.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExerciseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting exercise with ID: {ExerciseId}", id);
        var exercise = await _mediator.Send(new GetExerciseByIdQuery { ExerciseId = id }, cancellationToken);

        if (exercise == null)
        {
            _logger.LogWarning("Exercise with ID {ExerciseId} not found", id);
            return NotFound();
        }

        return Ok(exercise);
    }

    /// <summary>
    /// Creates a new exercise.
    /// </summary>
    /// <param name="command">The create exercise command.</param>
    /// <returns>The created exercise.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExerciseDto>> Create([FromBody] CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new exercise: {ExerciseName}", command.Name);
        var exercise = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
    }

    /// <summary>
    /// Updates an existing exercise.
    /// </summary>
    /// <param name="id">The exercise ID.</param>
    /// <param name="command">The update exercise command.</param>
    /// <returns>The updated exercise.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExerciseDto>> Update(Guid id, [FromBody] UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ExerciseId)
        {
            _logger.LogWarning("Route ID {RouteId} does not match command ID {CommandId}", id, command.ExerciseId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating exercise with ID: {ExerciseId}", id);
        var exercise = await _mediator.Send(command, cancellationToken);

        if (exercise == null)
        {
            _logger.LogWarning("Exercise with ID {ExerciseId} not found", id);
            return NotFound();
        }

        return Ok(exercise);
    }

    /// <summary>
    /// Deletes an exercise.
    /// </summary>
    /// <param name="id">The exercise ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting exercise with ID: {ExerciseId}", id);
        var result = await _mediator.Send(new DeleteExerciseCommand { ExerciseId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Exercise with ID {ExerciseId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
