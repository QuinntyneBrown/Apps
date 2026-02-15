using TaxEstimates.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaxEstimates.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxEstimatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaxEstimatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaxEstimateDto>>> GetTaxEstimates([FromQuery] Guid tenantId, [FromQuery] Guid userId, [FromQuery] int? year)
    {
        var query = new GetTaxEstimatesQuery(tenantId, userId, year);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaxEstimateDto>> GetTaxEstimate(Guid id, [FromQuery] Guid tenantId)
    {
        var query = new GetTaxEstimateByIdQuery(id, tenantId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaxEstimateDto>> CreateTaxEstimate([FromBody] CreateTaxEstimateCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTaxEstimate), new { id = result.TaxEstimateId, tenantId = command.TenantId }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaxEstimate(Guid id, [FromQuery] Guid tenantId)
    {
        var command = new DeleteTaxEstimateCommand(id, tenantId);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
