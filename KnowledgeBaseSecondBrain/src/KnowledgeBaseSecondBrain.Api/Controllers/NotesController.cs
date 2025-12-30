using KnowledgeBaseSecondBrain.Api.Features.Notes;
using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseSecondBrain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NotesController> _logger;

    public NotesController(IMediator mediator, ILogger<NotesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes(
        [FromQuery] Guid? userId,
        [FromQuery] NoteType? noteType,
        [FromQuery] bool? isFavorite,
        [FromQuery] bool? isArchived)
    {
        _logger.LogInformation("Getting notes for user {UserId}", userId);

        var result = await _mediator.Send(new GetNotesQuery
        {
            UserId = userId,
            NoteType = noteType,
            IsFavorite = isFavorite,
            IsArchived = isArchived,
        });

        return Ok(result);
    }

    [HttpGet("{noteId:guid}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteDto>> GetNoteById(Guid noteId)
    {
        _logger.LogInformation("Getting note {NoteId}", noteId);

        var result = await _mediator.Send(new GetNoteByIdQuery { NoteId = noteId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteCommand command)
    {
        _logger.LogInformation("Creating note for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/notes/{result.NoteId}", result);
    }

    [HttpPut("{noteId:guid}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteDto>> UpdateNote(Guid noteId, [FromBody] UpdateNoteCommand command)
    {
        if (noteId != command.NoteId)
        {
            return BadRequest("Note ID mismatch");
        }

        _logger.LogInformation("Updating note {NoteId}", noteId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{noteId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNote(Guid noteId)
    {
        _logger.LogInformation("Deleting note {NoteId}", noteId);

        var result = await _mediator.Send(new DeleteNoteCommand { NoteId = noteId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
