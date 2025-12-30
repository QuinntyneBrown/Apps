// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Api.Features.Rounds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoundsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoundsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoundDto>>> GetRounds([FromQuery] Guid? userId, [FromQuery] Guid? courseId)
    {
        var query = new GetRoundsQuery { UserId = userId, CourseId = courseId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoundDto>> GetRoundById(Guid id)
    {
        var query = new GetRoundByIdQuery { RoundId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoundDto>> CreateRound([FromBody] CreateRoundCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoundById), new { id = result.RoundId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoundDto>> UpdateRound(Guid id, [FromBody] UpdateRoundCommand command)
    {
        if (id != command.RoundId)
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
    public async Task<ActionResult> DeleteRound(Guid id)
    {
        var command = new DeleteRoundCommand { RoundId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
