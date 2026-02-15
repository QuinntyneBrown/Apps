using Receipts.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Receipts.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<ReceiptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetReceipts(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetReceiptsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReceiptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReceiptDto>> GetReceiptById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetReceiptByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReceiptDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ReceiptDto>> CreateReceipt(
        [FromBody] CreateReceiptCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReceiptById), new { id = result.ReceiptId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReceipt(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteReceiptCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
