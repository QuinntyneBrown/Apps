using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Features.Purchases;

namespace WarrantyReturnPeriodTracker.Api.Controllers;

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
    public async Task<ActionResult<List<PurchaseDto>>> GetPurchases(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all purchases");
        var purchases = await _mediator.Send(new GetPurchasesQuery(), cancellationToken);
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting purchase with ID: {PurchaseId}", id);
        var purchase = await _mediator.Send(new GetPurchaseByIdQuery { PurchaseId = id }, cancellationToken);

        if (purchase == null)
        {
            _logger.LogWarning("Purchase with ID {PurchaseId} not found", id);
            return NotFound();
        }

        return Ok(purchase);
    }

    [HttpPost]
    public async Task<ActionResult<PurchaseDto>> CreatePurchase(
        CreatePurchaseCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new purchase for product: {ProductName}", command.ProductName);
        var purchase = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.PurchaseId }, purchase);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PurchaseDto>> UpdatePurchase(
        Guid id,
        UpdatePurchaseCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PurchaseId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating purchase with ID: {PurchaseId}", id);

        try
        {
            var purchase = await _mediator.Send(command, cancellationToken);
            return Ok(purchase);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to update purchase with ID {PurchaseId}", id);
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePurchase(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting purchase with ID: {PurchaseId}", id);

        try
        {
            await _mediator.Send(new DeletePurchaseCommand { PurchaseId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to delete purchase with ID {PurchaseId}", id);
            return NotFound(ex.Message);
        }
    }
}
