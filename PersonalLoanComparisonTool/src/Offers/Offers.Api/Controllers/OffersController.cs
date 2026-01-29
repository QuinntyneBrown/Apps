using Offers.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Offers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OffersController> _logger;

    public OffersController(IMediator mediator, ILogger<OffersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OfferDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetOffers(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOffersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OfferDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OfferDto>> GetOfferById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOfferByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OfferDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<OfferDto>> CreateOffer([FromBody] CreateOfferCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOfferById), new { id = result.OfferId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(OfferDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OfferDto>> UpdateOffer(Guid id, [FromBody] UpdateOfferCommand command, CancellationToken cancellationToken)
    {
        if (id != command.OfferId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffer(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteOfferCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
