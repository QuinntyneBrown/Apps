// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// API controller for managing injuries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InjuriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InjuriesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="InjuriesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public InjuriesController(IMediator mediator, ILogger<InjuriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets an injury by ID.
    /// </summary>
    /// <param name="id">The injury ID.</param>
    /// <returns>The injury.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InjuryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InjuryDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting injury {InjuryId}", id);

        var result = await _mediator.Send(new GetInjuryByIdQuery { InjuryId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all injuries for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of injuries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InjuryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InjuryDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting injuries for user {UserId}", userId);

        var result = await _mediator.Send(new GetInjuriesQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new injury.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created injury.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(InjuryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InjuryDto>> Create([FromBody] CreateInjuryCommand command)
    {
        _logger.LogInformation(
            "Creating injury for user {UserId}",
            command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.InjuryId },
            result);
    }

    /// <summary>
    /// Updates an existing injury.
    /// </summary>
    /// <param name="id">The injury ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated injury.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InjuryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InjuryDto>> Update(Guid id, [FromBody] UpdateInjuryCommand command)
    {
        if (id != command.InjuryId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating injury {InjuryId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an injury.
    /// </summary>
    /// <param name="id">The injury ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting injury {InjuryId}", id);

        var result = await _mediator.Send(new DeleteInjuryCommand { InjuryId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
