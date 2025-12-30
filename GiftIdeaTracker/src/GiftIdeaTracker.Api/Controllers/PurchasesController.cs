using GiftIdeaTracker.Api.Features.Purchases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GiftIdeaTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PurchasesController> _logger;

    public PurchasesController(IMediator mediator, ILogger<PurchasesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PurchaseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases(
        [FromQuery] Guid? giftIdeaId)
    {
        _logger.LogInformation("Getting purchases");

        var result = await _mediator.Send(new GetPurchasesQuery
        {
            GiftIdeaId = giftIdeaId,
        });

        return Ok(result);
    }

    [HttpGet("{purchaseId:guid}")]
    [ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById(Guid purchaseId)
    {
        _logger.LogInformation("Getting purchase {PurchaseId}", purchaseId);

        var result = await _mediator.Send(new GetPurchaseByIdQuery { PurchaseId = purchaseId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PurchaseDto>> CreatePurchase([FromBody] CreatePurchaseCommand command)
    {
        _logger.LogInformation("Creating purchase for gift idea {GiftIdeaId}", command.GiftIdeaId);

        var result = await _mediator.Send(command);

        return Created($"/api/purchases/{result.PurchaseId}", result);
    }

    [HttpPut("{purchaseId:guid}")]
    [ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PurchaseDto>> UpdatePurchase(Guid purchaseId, [FromBody] UpdatePurchaseCommand command)
    {
        if (purchaseId != command.PurchaseId)
        {
            return BadRequest("Purchase ID mismatch");
        }

        _logger.LogInformation("Updating purchase {PurchaseId}", purchaseId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{purchaseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePurchase(Guid purchaseId)
    {
        _logger.LogInformation("Deleting purchase {PurchaseId}", purchaseId);

        var result = await _mediator.Send(new DeletePurchaseCommand { PurchaseId = purchaseId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
