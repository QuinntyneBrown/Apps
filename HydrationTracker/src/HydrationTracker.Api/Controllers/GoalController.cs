// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Api.Features.Goal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydrationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GoalDto>>> GetGoals(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGoalsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDto>> GetGoalById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetGoalByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<GoalDto>> CreateGoal(
        [FromBody] CreateGoalCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetGoalById), new { id = result.GoalId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GoalDto>> UpdateGoal(
        Guid id,
        [FromBody] UpdateGoalCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.GoalId)
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
    public async Task<IActionResult> DeleteGoal(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteGoalCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
