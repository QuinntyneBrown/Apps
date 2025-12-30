// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.LegacyInstructions;
using DigitalLegacyPlanner.Api.Features.LegacyInstructions.Commands;
using DigitalLegacyPlanner.Api.Features.LegacyInstructions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLegacyPlanner.Api.Controllers;

/// <summary>
/// Controller for managing legacy instructions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LegacyInstructionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LegacyInstructionsController> _logger;

    public LegacyInstructionsController(IMediator mediator, ILogger<LegacyInstructionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all legacy instructions.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="category">Optional category filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of legacy instructions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<LegacyInstructionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LegacyInstructionDto>>> GetLegacyInstructions(
        [FromQuery] Guid? userId,
        [FromQuery] string? category,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting legacy instructions for user {UserId}", userId);
        var query = new GetLegacyInstructionsQuery { UserId = userId, Category = category };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a legacy instruction by ID.
    /// </summary>
    /// <param name="id">Legacy instruction ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The legacy instruction.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LegacyInstructionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LegacyInstructionDto>> GetLegacyInstructionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting legacy instruction {LegacyInstructionId}", id);
        var query = new GetLegacyInstructionByIdQuery { LegacyInstructionId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new legacy instruction.
    /// </summary>
    /// <param name="command">Create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created legacy instruction.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(LegacyInstructionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LegacyInstructionDto>> CreateLegacyInstruction(
        [FromBody] CreateLegacyInstructionCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating legacy instruction for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLegacyInstructionById), new { id = result.LegacyInstructionId }, result);
    }

    /// <summary>
    /// Updates an existing legacy instruction.
    /// </summary>
    /// <param name="id">Legacy instruction ID.</param>
    /// <param name="command">Update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated legacy instruction.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LegacyInstructionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LegacyInstructionDto>> UpdateLegacyInstruction(
        Guid id,
        [FromBody] UpdateLegacyInstructionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.LegacyInstructionId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating legacy instruction {LegacyInstructionId}", id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a legacy instruction.
    /// </summary>
    /// <param name="id">Legacy instruction ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLegacyInstruction(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting legacy instruction {LegacyInstructionId}", id);
        var command = new DeleteLegacyInstructionCommand { LegacyInstructionId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
