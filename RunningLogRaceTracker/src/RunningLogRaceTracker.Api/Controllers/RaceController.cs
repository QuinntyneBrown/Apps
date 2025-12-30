// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Api.Features.Race;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RunningLogRaceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public RaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RaceDto>>> GetRaces(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRacesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RaceDto>> GetRaceById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetRaceByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<RaceDto>> CreateRace(
        [FromBody] CreateRaceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRaceById), new { id = result.RaceId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RaceDto>> UpdateRace(
        Guid id,
        [FromBody] UpdateRaceCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RaceId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRace(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteRaceCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
