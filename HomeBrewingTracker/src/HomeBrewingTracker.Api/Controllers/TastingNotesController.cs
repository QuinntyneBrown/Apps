// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Features.TastingNotes;
using HomeBrewingTracker.Api.Features.TastingNotes.Commands;
using HomeBrewingTracker.Api.Features.TastingNotes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewingTracker.Api.Controllers;

/// <summary>
/// Controller for managing tasting notes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TastingNotesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TastingNotesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TastingNotesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public TastingNotesController(IMediator mediator, ILogger<TastingNotesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all tasting notes.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="batchId">Optional batch ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>List of tasting notes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TastingNoteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TastingNoteDto>>> GetTastingNotes(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? batchId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all tasting notes");
        var query = new GetTastingNotesQuery { UserId = userId, BatchId = batchId };
        var tastingNotes = await _mediator.Send(query, cancellationToken);
        return Ok(tastingNotes);
    }

    /// <summary>
    /// Gets a tasting note by ID.
    /// </summary>
    /// <param name="id">The tasting note ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The tasting note.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TastingNoteDto>> GetTastingNote(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tasting note with ID: {TastingNoteId}", id);
        var query = new GetTastingNoteByIdQuery { TastingNoteId = id };
        var tastingNote = await _mediator.Send(query, cancellationToken);

        if (tastingNote == null)
        {
            return NotFound();
        }

        return Ok(tastingNote);
    }

    /// <summary>
    /// Creates a new tasting note.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created tasting note.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TastingNoteDto>> CreateTastingNote(
        [FromBody] CreateTastingNoteCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new tasting note for batch: {BatchId}", command.BatchId);
        var tastingNote = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTastingNote), new { id = tastingNote.TastingNoteId }, tastingNote);
    }

    /// <summary>
    /// Updates an existing tasting note.
    /// </summary>
    /// <param name="id">The tasting note ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated tasting note.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TastingNoteDto>> UpdateTastingNote(
        Guid id,
        [FromBody] UpdateTastingNoteCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TastingNoteId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating tasting note with ID: {TastingNoteId}", id);

        try
        {
            var tastingNote = await _mediator.Send(command, cancellationToken);
            return Ok(tastingNote);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a tasting note.
    /// </summary>
    /// <param name="id">The tasting note ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTastingNote(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tasting note with ID: {TastingNoteId}", id);

        try
        {
            var command = new DeleteTastingNoteCommand { TastingNoteId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
