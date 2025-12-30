// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Notes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceEventManager.Api.Controllers;

/// <summary>
/// Controller for managing notes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NotesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotesController"/> class.
    /// </summary>
    public NotesController(IMediator mediator, ILogger<NotesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all notes, optionally filtered by event ID or user ID.
    /// </summary>
    /// <param name="eventId">Optional event ID to filter notes.</param>
    /// <param name="userId">Optional user ID to filter notes.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of notes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<NoteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<NoteDto>>> GetNotes([FromQuery] Guid? eventId, [FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetNotes.Query { EventId = eventId, UserId = userId };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving notes");
            return StatusCode(500, "An error occurred while retrieving notes");
        }
    }

    /// <summary>
    /// Gets a note by ID.
    /// </summary>
    /// <param name="id">Note ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Note details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteDto>> GetNote(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetNoteById.Query { NoteId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Note with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving note {NoteId}", id);
            return StatusCode(500, "An error occurred while retrieving the note");
        }
    }

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="command">Create note command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created note.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNote.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetNote), new { id = result.NoteId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating note");
            return StatusCode(500, "An error occurred while creating the note");
        }
    }

    /// <summary>
    /// Updates an existing note.
    /// </summary>
    /// <param name="id">Note ID.</param>
    /// <param name="command">Update note command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated note.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteDto>> UpdateNote(Guid id, [FromBody] UpdateNote.Command command, CancellationToken cancellationToken)
    {
        if (id != command.NoteId)
        {
            return BadRequest("Note ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Note with ID {id} not found");
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating note {NoteId}", id);
            return StatusCode(500, "An error occurred while updating the note");
        }
    }

    /// <summary>
    /// Deletes a note.
    /// </summary>
    /// <param name="id">Note ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNote(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteNote.Command { NoteId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Note with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting note {NoteId}", id);
            return StatusCode(500, "An error occurred while deleting the note");
        }
    }
}
