// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Api.Features.Handicaps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HandicapsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HandicapsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HandicapDto>>> GetHandicaps([FromQuery] Guid? userId)
    {
        var query = new GetHandicapsQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HandicapDto>> GetHandicapById(Guid id)
    {
        var query = new GetHandicapByIdQuery { HandicapId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<HandicapDto>> CreateHandicap([FromBody] CreateHandicapCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetHandicapById), new { id = result.HandicapId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HandicapDto>> UpdateHandicap(Guid id, [FromBody] UpdateHandicapCommand command)
    {
        if (id != command.HandicapId)
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
    public async Task<ActionResult> DeleteHandicap(Guid id)
    {
        var command = new DeleteHandicapCommand { HandicapId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
