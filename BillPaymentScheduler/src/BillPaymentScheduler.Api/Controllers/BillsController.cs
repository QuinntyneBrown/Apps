// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Features.Bills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillPaymentScheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BillsController> _logger;

    public BillsController(IMediator mediator, ILogger<BillsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all bills.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<BillDto>>> GetBills(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all bills");
        var bills = await _mediator.Send(new GetBills.Query(), cancellationToken);
        return Ok(bills);
    }

    /// <summary>
    /// Gets a bill by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BillDto>> GetBillById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting bill with ID: {BillId}", id);
        var bill = await _mediator.Send(new GetBillById.Query(id), cancellationToken);

        if (bill == null)
        {
            _logger.LogWarning("Bill with ID {BillId} not found", id);
            return NotFound();
        }

        return Ok(bill);
    }

    /// <summary>
    /// Creates a new bill.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BillDto>> CreateBill(
        [FromBody] CreateBill.Command command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new bill: {BillName}", command.Name);
        var bill = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBillById), new { id = bill.BillId }, bill);
    }

    /// <summary>
    /// Updates an existing bill.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<BillDto>> UpdateBill(
        Guid id,
        [FromBody] UpdateBill.Command command,
        CancellationToken cancellationToken)
    {
        if (id != command.BillId)
        {
            _logger.LogWarning("Bill ID mismatch: URL {UrlId} vs Body {BodyId}", id, command.BillId);
            return BadRequest("Bill ID mismatch");
        }

        _logger.LogInformation("Updating bill with ID: {BillId}", id);
        var bill = await _mediator.Send(command, cancellationToken);

        if (bill == null)
        {
            _logger.LogWarning("Bill with ID {BillId} not found", id);
            return NotFound();
        }

        return Ok(bill);
    }

    /// <summary>
    /// Deletes a bill.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBill(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting bill with ID: {BillId}", id);
        var result = await _mediator.Send(new DeleteBill.Command(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Bill with ID {BillId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
