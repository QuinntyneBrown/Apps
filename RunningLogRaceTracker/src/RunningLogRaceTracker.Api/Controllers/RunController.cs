// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Api.Features.Run;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RunningLogRaceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RunController : ControllerBase
{
    private readonly IMediator _mediator;

    public RunController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RunDto>>> GetRuns(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRunsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RunDto>> GetRunById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetRunByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<RunDto>> CreateRun(
        [FromBody] CreateRunCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRunById), new { id = result.RunId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RunDto>> UpdateRun(
        Guid id,
        [FromBody] UpdateRunCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RunId)
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
    public async Task<IActionResult> DeleteRun(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteRunCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
