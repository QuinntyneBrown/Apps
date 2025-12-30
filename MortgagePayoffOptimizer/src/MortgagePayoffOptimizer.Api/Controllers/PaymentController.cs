// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MortgagePayoffOptimizer.Api.Features.Payment;

namespace MortgagePayoffOptimizer.Api.Controllers;

/// <summary>
/// Controller for managing payments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all payments.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PaymentDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetPaymentsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Gets a payment by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPaymentByIdQuery { PaymentId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new payment.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaymentDto>> Create([FromBody] CreatePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.PaymentId }, result);
    }

    /// <summary>
    /// Updates an existing payment.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentDto>> Update(Guid id, [FromBody] UpdatePaymentCommand command)
    {
        if (id != command.PaymentId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a payment.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePaymentCommand { PaymentId = id });
        return NoContent();
    }
}
