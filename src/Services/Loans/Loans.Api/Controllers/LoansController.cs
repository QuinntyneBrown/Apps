using Loans.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LoansController> _logger;

    public LoansController(IMediator mediator, ILogger<LoansController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LoanDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetLoans(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLoansQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LoanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoanDto>> GetLoanById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLoanByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LoanDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] CreateLoanCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLoanById), new { id = result.LoanId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LoanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoanDto>> UpdateLoan(Guid id, [FromBody] UpdateLoanCommand command, CancellationToken cancellationToken)
    {
        if (id != command.LoanId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLoan(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteLoanCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
