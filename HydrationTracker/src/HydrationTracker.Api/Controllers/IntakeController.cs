// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Api.Features.Intake;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydrationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntakeController : ControllerBase
{
    private readonly IMediator _mediator;

    public IntakeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<IntakeDto>>> GetIntakes(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetIntakesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IntakeDto>> GetIntakeById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetIntakeByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<IntakeDto>> CreateIntake(
        [FromBody] CreateIntakeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetIntakeById), new { id = result.IntakeId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IntakeDto>> UpdateIntake(
        Guid id,
        [FromBody] UpdateIntakeCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.IntakeId)
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
    public async Task<IActionResult> DeleteIntake(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteIntakeCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
