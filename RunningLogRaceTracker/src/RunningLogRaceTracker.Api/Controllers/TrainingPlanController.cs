// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Api.Features.TrainingPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RunningLogRaceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainingPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TrainingPlanDto>>> GetTrainingPlans(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTrainingPlansQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingPlanDto>> GetTrainingPlanById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetTrainingPlanByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<TrainingPlanDto>> CreateTrainingPlan(
        [FromBody] CreateTrainingPlanCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTrainingPlanById), new { id = result.TrainingPlanId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrainingPlanDto>> UpdateTrainingPlan(
        Guid id,
        [FromBody] UpdateTrainingPlanCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TrainingPlanId)
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
    public async Task<IActionResult> DeleteTrainingPlan(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteTrainingPlanCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
