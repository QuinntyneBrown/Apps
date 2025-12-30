// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Api.Features.Hands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokerGameTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HandsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HandDto>>> GetHands()
    {
        var query = new GetHandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HandDto>> GetHandById(Guid id)
    {
        var query = new GetHandByIdQuery { HandId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<HandDto>> CreateHand([FromBody] CreateHandCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetHandById), new { id = result.HandId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HandDto>> UpdateHand(Guid id, [FromBody] UpdateHandCommand command)
    {
        if (id != command.HandId)
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
    public async Task<ActionResult> DeleteHand(Guid id)
    {
        var command = new DeleteHandCommand { HandId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
