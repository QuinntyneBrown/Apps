// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// API controller for managing recovery exercises.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RecoveryExercisesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecoveryExercisesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecoveryExercisesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RecoveryExercisesController(IMediator mediator, ILogger<RecoveryExercisesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a recovery exercise by ID.
    /// </summary>
    /// <param name="id">The recovery exercise ID.</param>
    /// <returns>The recovery exercise.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RecoveryExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecoveryExerciseDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting recovery exercise {RecoveryExerciseId}", id);

        var result = await _mediator.Send(new GetRecoveryExerciseByIdQuery { RecoveryExerciseId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all recovery exercises for an injury.
    /// </summary>
    /// <param name="injuryId">The injury ID.</param>
    /// <returns>The list of recovery exercises.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecoveryExerciseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecoveryExerciseDto>>> GetByInjuryId([FromQuery] Guid injuryId)
    {
        _logger.LogInformation("Getting recovery exercises for injury {InjuryId}", injuryId);

        var result = await _mediator.Send(new GetRecoveryExercisesQuery { InjuryId = injuryId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new recovery exercise.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created recovery exercise.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecoveryExerciseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecoveryExerciseDto>> Create([FromBody] CreateRecoveryExerciseCommand command)
    {
        _logger.LogInformation(
            "Creating recovery exercise for injury {InjuryId}",
            command.InjuryId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.RecoveryExerciseId },
            result);
    }

    /// <summary>
    /// Updates an existing recovery exercise.
    /// </summary>
    /// <param name="id">The recovery exercise ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated recovery exercise.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RecoveryExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecoveryExerciseDto>> Update(Guid id, [FromBody] UpdateRecoveryExerciseCommand command)
    {
        if (id != command.RecoveryExerciseId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating recovery exercise {RecoveryExerciseId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a recovery exercise.
    /// </summary>
    /// <param name="id">The recovery exercise ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting recovery exercise {RecoveryExerciseId}", id);

        var result = await _mediator.Send(new DeleteRecoveryExerciseCommand { RecoveryExerciseId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
