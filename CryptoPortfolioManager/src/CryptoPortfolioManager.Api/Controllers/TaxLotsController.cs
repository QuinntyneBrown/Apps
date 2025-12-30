// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Features.TaxLots;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxLotsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaxLotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaxLotDto>>> GetTaxLots([FromQuery] Guid? cryptoHoldingId = null)
    {
        var taxLots = await _mediator.Send(new GetTaxLotsQuery { CryptoHoldingId = cryptoHoldingId });
        return Ok(taxLots);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaxLotDto>> GetTaxLot(Guid id)
    {
        var taxLot = await _mediator.Send(new GetTaxLotByIdQuery { TaxLotId = id });

        if (taxLot == null)
            return NotFound();

        return Ok(taxLot);
    }

    [HttpPost]
    public async Task<ActionResult<TaxLotDto>> CreateTaxLot([FromBody] CreateTaxLotCommand command)
    {
        var taxLot = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTaxLot), new { id = taxLot.TaxLotId }, taxLot);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaxLotDto>> UpdateTaxLot(Guid id, [FromBody] UpdateTaxLotCommand command)
    {
        command.TaxLotId = id;
        var taxLot = await _mediator.Send(command);

        if (taxLot == null)
            return NotFound();

        return Ok(taxLot);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaxLot(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaxLotCommand { TaxLotId = id });

        if (!result)
            return NotFound();

        return NoContent();
    }
}
