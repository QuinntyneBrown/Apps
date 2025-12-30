// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

namespace PersonalLoanComparisonTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentScheduleDto>>> GetPaymentSchedules(CancellationToken cancellationToken)
    {
        var paymentSchedules = await _mediator.Send(new GetPaymentSchedulesQuery(), cancellationToken);
        return Ok(paymentSchedules);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentScheduleDto>> GetPaymentScheduleById(Guid id, CancellationToken cancellationToken)
    {
        var paymentSchedule = await _mediator.Send(new GetPaymentScheduleByIdQuery(id), cancellationToken);

        if (paymentSchedule == null)
        {
            return NotFound();
        }

        return Ok(paymentSchedule);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentScheduleDto>> CreatePaymentSchedule([FromBody] CreatePaymentScheduleCommand command, CancellationToken cancellationToken)
    {
        var paymentSchedule = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPaymentScheduleById), new { id = paymentSchedule.PaymentScheduleId }, paymentSchedule);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentScheduleDto>> UpdatePaymentSchedule(Guid id, [FromBody] UpdatePaymentScheduleCommand command, CancellationToken cancellationToken)
    {
        if (id != command.PaymentScheduleId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var paymentSchedule = await _mediator.Send(command, cancellationToken);
            return Ok(paymentSchedule);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePaymentSchedule(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeletePaymentScheduleCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
