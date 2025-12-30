// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Api.Features.Meeting;
using Microsoft.AspNetCore.Mvc;

namespace MeetingNotesActionItemTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<MeetingDto>>> GetMeetings()
    {
        var meetings = await _mediator.Send(new GetMeetingsQuery());
        return Ok(meetings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeetingDto>> GetMeetingById(Guid id)
    {
        var meeting = await _mediator.Send(new GetMeetingByIdQuery(id));

        if (meeting == null)
        {
            return NotFound();
        }

        return Ok(meeting);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingDto>> CreateMeeting([FromBody] CreateMeetingCommand command)
    {
        var meeting = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.MeetingId }, meeting);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MeetingDto>> UpdateMeeting(Guid id, [FromBody] UpdateMeetingCommand command)
    {
        if (id != command.MeetingId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var meeting = await _mediator.Send(command);
            return Ok(meeting);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMeeting(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteMeetingCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
