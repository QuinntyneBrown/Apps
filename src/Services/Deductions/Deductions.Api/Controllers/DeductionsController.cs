using Deductions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deductions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeductionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeductionsController> _logger;

    public DeductionsController(IMediator mediator, ILogger<DeductionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeductionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeductionDto>>> GetDeductions(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDeductionsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DeductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeductionDto>> GetDeductionById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDeductionByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeductionDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<DeductionDto>> CreateDeduction(
        [FromBody] CreateDeductionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDeductionById), new { id = result.DeductionId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DeductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeductionDto>> UpdateDeduction(
        Guid id,
        [FromBody] UpdateDeductionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.DeductionId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeduction(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteDeductionCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
