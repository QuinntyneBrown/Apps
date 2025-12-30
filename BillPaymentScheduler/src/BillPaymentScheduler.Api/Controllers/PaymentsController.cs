// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Features.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillPaymentScheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IMediator mediator, ILogger<PaymentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all payments.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PaymentDto>>> GetPayments(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all payments");
        var payments = await _mediator.Send(new GetPayments.Query(), cancellationToken);
        return Ok(payments);
    }

    /// <summary>
    /// Gets a payment by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDto>> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting payment with ID: {PaymentId}", id);
        var payment = await _mediator.Send(new GetPaymentById.Query(id), cancellationToken);

        if (payment == null)
        {
            _logger.LogWarning("Payment with ID {PaymentId} not found", id);
            return NotFound();
        }

        return Ok(payment);
    }

    /// <summary>
    /// Creates a new payment.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaymentDto>> CreatePayment(
        [FromBody] CreatePayment.Command command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new payment for bill: {BillId}", command.BillId);
        var payment = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
    }

    /// <summary>
    /// Updates an existing payment.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentDto>> UpdatePayment(
        Guid id,
        [FromBody] UpdatePayment.Command command,
        CancellationToken cancellationToken)
    {
        if (id != command.PaymentId)
        {
            _logger.LogWarning("Payment ID mismatch: URL {UrlId} vs Body {BodyId}", id, command.PaymentId);
            return BadRequest("Payment ID mismatch");
        }

        _logger.LogInformation("Updating payment with ID: {PaymentId}", id);
        var payment = await _mediator.Send(command, cancellationToken);

        if (payment == null)
        {
            _logger.LogWarning("Payment with ID {PaymentId} not found", id);
            return NotFound();
        }

        return Ok(payment);
    }

    /// <summary>
    /// Deletes a payment.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePayment(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting payment with ID: {PaymentId}", id);
        var result = await _mediator.Send(new DeletePayment.Command(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Payment with ID {PaymentId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
