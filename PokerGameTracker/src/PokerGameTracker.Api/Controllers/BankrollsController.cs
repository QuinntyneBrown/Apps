// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Api.Features.Bankrolls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokerGameTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankrollsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BankrollsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<BankrollDto>>> GetBankrolls()
    {
        var query = new GetBankrollsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BankrollDto>> GetBankrollById(Guid id)
    {
        var query = new GetBankrollByIdQuery { BankrollId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BankrollDto>> CreateBankroll([FromBody] CreateBankrollCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBankrollById), new { id = result.BankrollId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BankrollDto>> UpdateBankroll(Guid id, [FromBody] UpdateBankrollCommand command)
    {
        if (id != command.BankrollId)
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
    public async Task<ActionResult> DeleteBankroll(Guid id)
    {
        var command = new DeleteBankrollCommand { BankrollId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
