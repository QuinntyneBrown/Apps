// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Api.Features.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokerGameTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SessionDto>>> GetSessions()
    {
        var query = new GetSessionsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SessionDto>> GetSessionById(Guid id)
    {
        var query = new GetSessionByIdQuery { SessionId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SessionDto>> CreateSession([FromBody] CreateSessionCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSessionById), new { id = result.SessionId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SessionDto>> UpdateSession(Guid id, [FromBody] UpdateSessionCommand command)
    {
        if (id != command.SessionId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSession(Guid id)
    {
        var command = new DeleteSessionCommand { SessionId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
