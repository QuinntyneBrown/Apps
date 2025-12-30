// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Features.CryptoHoldings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoHoldingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CryptoHoldingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CryptoHoldingDto>>> GetCryptoHoldings([FromQuery] Guid? walletId = null)
    {
        var holdings = await _mediator.Send(new GetCryptoHoldingsQuery { WalletId = walletId });
        return Ok(holdings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CryptoHoldingDto>> GetCryptoHolding(Guid id)
    {
        var holding = await _mediator.Send(new GetCryptoHoldingByIdQuery { CryptoHoldingId = id });

        if (holding == null)
            return NotFound();

        return Ok(holding);
    }

    [HttpPost]
    public async Task<ActionResult<CryptoHoldingDto>> CreateCryptoHolding([FromBody] CreateCryptoHoldingCommand command)
    {
        var holding = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCryptoHolding), new { id = holding.CryptoHoldingId }, holding);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CryptoHoldingDto>> UpdateCryptoHolding(Guid id, [FromBody] UpdateCryptoHoldingCommand command)
    {
        command.CryptoHoldingId = id;
        var holding = await _mediator.Send(command);

        if (holding == null)
            return NotFound();

        return Ok(holding);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCryptoHolding(Guid id)
    {
        var result = await _mediator.Send(new DeleteCryptoHoldingCommand { CryptoHoldingId = id });

        if (!result)
            return NotFound();

        return NoContent();
    }
}
