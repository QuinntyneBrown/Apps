// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Api.Features.Liability;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalNetWorthDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LiabilityController : ControllerBase
{
    private readonly IMediator _mediator;

    public LiabilityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LiabilityDto>>> GetLiabilities(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLiabilitiesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LiabilityDto>> GetLiabilityById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetLiabilityByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<LiabilityDto>> CreateLiability(
        [FromBody] CreateLiabilityCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLiabilityById), new { id = result.LiabilityId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LiabilityDto>> UpdateLiability(
        Guid id,
        [FromBody] UpdateLiabilityCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.LiabilityId)
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
    public async Task<IActionResult> DeleteLiability(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteLiabilityCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
