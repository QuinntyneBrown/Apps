// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalLoanComparisonTool.Api.Features.Offer;

namespace PersonalLoanComparisonTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OffersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<OfferDto>>> GetOffers(CancellationToken cancellationToken)
    {
        var offers = await _mediator.Send(new GetOffersQuery(), cancellationToken);
        return Ok(offers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OfferDto>> GetOfferById(Guid id, CancellationToken cancellationToken)
    {
        var offer = await _mediator.Send(new GetOfferByIdQuery(id), cancellationToken);

        if (offer == null)
        {
            return NotFound();
        }

        return Ok(offer);
    }

    [HttpPost]
    public async Task<ActionResult<OfferDto>> CreateOffer([FromBody] CreateOfferCommand command, CancellationToken cancellationToken)
    {
        var offer = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOfferById), new { id = offer.OfferId }, offer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OfferDto>> UpdateOffer(Guid id, [FromBody] UpdateOfferCommand command, CancellationToken cancellationToken)
    {
        if (id != command.OfferId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var offer = await _mediator.Send(command, cancellationToken);
            return Ok(offer);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOffer(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteOfferCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
