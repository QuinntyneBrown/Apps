using TaxYears.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaxYears.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxYearsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaxYearsController> _logger;

    public TaxYearsController(IMediator mediator, ILogger<TaxYearsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaxYearDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaxYearDto>>> GetTaxYears(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTaxYearsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaxYearDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxYearDto>> GetTaxYearById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTaxYearByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TaxYearDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaxYearDto>> CreateTaxYear(
        [FromBody] CreateTaxYearCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTaxYearById), new { id = result.TaxYearId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TaxYearDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxYearDto>> UpdateTaxYear(
        Guid id,
        [FromBody] UpdateTaxYearCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TaxYearId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTaxYear(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTaxYearCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
