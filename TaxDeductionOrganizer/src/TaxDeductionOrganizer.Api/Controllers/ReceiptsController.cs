// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxDeductionOrganizer.Api.Features.Receipts;

namespace TaxDeductionOrganizer.Api.Controllers;

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

    /// <summary>
    /// Gets all receipts, optionally filtered by deduction.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ReceiptDto>>> GetAll([FromQuery] Guid? deductionId = null)
    {
        _logger.LogInformation("Getting all receipts for deduction: {DeductionId}", deductionId);
        var result = await _mediator.Send(new GetAllReceipts.Query { DeductionId = deductionId });
        return Ok(result);
    }

    /// <summary>
    /// Gets a receipt by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReceiptDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting receipt with ID: {ReceiptId}", id);
        try
        {
            var result = await _mediator.Send(new GetReceiptById.Query { ReceiptId = id });
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Receipt with ID {ReceiptId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new receipt.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ReceiptDto>> Create([FromBody] CreateReceipt.Command command)
    {
        _logger.LogInformation("Creating new receipt for deduction {DeductionId}", command.DeductionId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.ReceiptId }, result);
    }

    /// <summary>
    /// Updates an existing receipt.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ReceiptDto>> Update(Guid id, [FromBody] UpdateReceipt.Command command)
    {
        if (id != command.ReceiptId)
        {
            _logger.LogWarning("Receipt ID mismatch: route={RouteId}, body={BodyId}", id, command.ReceiptId);
            return BadRequest(new { message = "Receipt ID in route does not match ID in request body" });
        }

        _logger.LogInformation("Updating receipt with ID: {ReceiptId}", id);
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Receipt with ID {ReceiptId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a receipt.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting receipt with ID: {ReceiptId}", id);
        try
        {
            await _mediator.Send(new DeleteReceipt.Command { ReceiptId = id });
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Receipt with ID {ReceiptId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }
}
