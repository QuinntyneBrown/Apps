// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalLoanComparisonTool.Api.Features.Loan;

namespace PersonalLoanComparisonTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanDto>>> GetLoans(CancellationToken cancellationToken)
    {
        var loans = await _mediator.Send(new GetLoansQuery(), cancellationToken);
        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanDto>> GetLoanById(Guid id, CancellationToken cancellationToken)
    {
        var loan = await _mediator.Send(new GetLoanByIdQuery(id), cancellationToken);

        if (loan == null)
        {
            return NotFound();
        }

        return Ok(loan);
    }

    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] CreateLoanCommand command, CancellationToken cancellationToken)
    {
        var loan = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLoanById), new { id = loan.LoanId }, loan);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LoanDto>> UpdateLoan(Guid id, [FromBody] UpdateLoanCommand command, CancellationToken cancellationToken)
    {
        if (id != command.LoanId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var loan = await _mediator.Send(command, cancellationToken);
            return Ok(loan);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLoan(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteLoanCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
