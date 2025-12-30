// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Api.Features.Note;
using Microsoft.AspNetCore.Mvc;

namespace MeetingNotesActionItemTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<NoteDto>>> GetNotes()
    {
        var notes = await _mediator.Send(new GetNotesQuery());
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNoteById(Guid id)
    {
        var note = await _mediator.Send(new GetNoteByIdQuery(id));

        if (note == null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteCommand command)
    {
        var note = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetNoteById), new { id = note.NoteId }, note);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NoteDto>> UpdateNote(Guid id, [FromBody] UpdateNoteCommand command)
    {
        if (id != command.NoteId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var note = await _mediator.Send(command);
            return Ok(note);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteNoteCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
