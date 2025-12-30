using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Features.Receipts;

namespace WarrantyReturnPeriodTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceiptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReceiptsController> _logger;

    public ReceiptsController(IMediator mediator, ILogger<ReceiptsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReceiptDto>>> GetReceipts(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all receipts");
        var receipts = await _mediator.Send(new GetReceiptsQuery(), cancellationToken);
        return Ok(receipts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReceiptDto>> GetReceiptById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting receipt with ID: {ReceiptId}", id);
        var receipt = await _mediator.Send(new GetReceiptByIdQuery { ReceiptId = id }, cancellationToken);

        if (receipt == null)
        {
            _logger.LogWarning("Receipt with ID {ReceiptId} not found", id);
            return NotFound();
        }

        return Ok(receipt);
    }

    [HttpPost]
    public async Task<ActionResult<ReceiptDto>> CreateReceipt(
        CreateReceiptCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new receipt for purchase: {PurchaseId}", command.PurchaseId);
        var receipt = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReceiptById), new { id = receipt.ReceiptId }, receipt);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReceiptDto>> UpdateReceipt(
        Guid id,
        UpdateReceiptCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ReceiptId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating receipt with ID: {ReceiptId}", id);

        try
        {
            var receipt = await _mediator.Send(command, cancellationToken);
            return Ok(receipt);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to update receipt with ID {ReceiptId}", id);
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReceipt(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting receipt with ID: {ReceiptId}", id);

        try
        {
            await _mediator.Send(new DeleteReceiptCommand { ReceiptId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to delete receipt with ID {ReceiptId}", id);
            return NotFound(ex.Message);
        }
    }
}
