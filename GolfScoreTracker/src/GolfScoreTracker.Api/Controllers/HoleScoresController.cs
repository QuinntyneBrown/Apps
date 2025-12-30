// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Api.Features.HoleScores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HoleScoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public HoleScoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HoleScoreDto>>> GetHoleScores([FromQuery] Guid? roundId)
    {
        var query = new GetHoleScoresQuery { RoundId = roundId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HoleScoreDto>> GetHoleScoreById(Guid id)
    {
        var query = new GetHoleScoreByIdQuery { HoleScoreId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<HoleScoreDto>> CreateHoleScore([FromBody] CreateHoleScoreCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetHoleScoreById), new { id = result.HoleScoreId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HoleScoreDto>> UpdateHoleScore(Guid id, [FromBody] UpdateHoleScoreCommand command)
    {
        if (id != command.HoleScoreId)
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
    public async Task<ActionResult> DeleteHoleScore(Guid id)
    {
        var command = new DeleteHoleScoreCommand { HoleScoreId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
