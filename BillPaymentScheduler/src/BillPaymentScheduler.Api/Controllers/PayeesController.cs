// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Features.Payees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillPaymentScheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PayeesController> _logger;

    public PayeesController(IMediator mediator, ILogger<PayeesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all payees.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PayeeDto>>> GetPayees(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all payees");
        var payees = await _mediator.Send(new GetPayees.Query(), cancellationToken);
        return Ok(payees);
    }

    /// <summary>
    /// Gets a payee by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PayeeDto>> GetPayeeById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting payee with ID: {PayeeId}", id);
        var payee = await _mediator.Send(new GetPayeeById.Query(id), cancellationToken);

        if (payee == null)
        {
            _logger.LogWarning("Payee with ID {PayeeId} not found", id);
            return NotFound();
        }

        return Ok(payee);
    }

    /// <summary>
    /// Creates a new payee.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PayeeDto>> CreatePayee(
        [FromBody] CreatePayee.Command command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new payee: {PayeeName}", command.Name);
        var payee = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPayeeById), new { id = payee.PayeeId }, payee);
    }

    /// <summary>
    /// Updates an existing payee.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PayeeDto>> UpdatePayee(
        Guid id,
        [FromBody] UpdatePayee.Command command,
        CancellationToken cancellationToken)
    {
        if (id != command.PayeeId)
        {
            _logger.LogWarning("Payee ID mismatch: URL {UrlId} vs Body {BodyId}", id, command.PayeeId);
            return BadRequest("Payee ID mismatch");
        }

        _logger.LogInformation("Updating payee with ID: {PayeeId}", id);
        var payee = await _mediator.Send(command, cancellationToken);

        if (payee == null)
        {
            _logger.LogWarning("Payee with ID {PayeeId} not found", id);
            return NotFound();
        }

        return Ok(payee);
    }

    /// <summary>
    /// Deletes a payee.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePayee(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting payee with ID: {PayeeId}", id);
        var result = await _mediator.Send(new DeletePayee.Command(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Payee with ID {PayeeId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
