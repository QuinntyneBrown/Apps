using Incomes.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Incomes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IncomesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes([FromQuery] Guid tenantId, [FromQuery] Guid businessId)
    {
        var query = new GetIncomesQuery(tenantId, businessId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeDto>> GetIncome(Guid id, [FromQuery] Guid tenantId)
    {
        var query = new GetIncomeByIdQuery(id, tenantId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeDto>> CreateIncome([FromBody] CreateIncomeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetIncome), new { id = result.IncomeId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IncomeDto>> UpdateIncome(Guid id, [FromBody] UpdateIncomeCommand command)
    {
        if (id != command.IncomeId) return BadRequest();
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncome(Guid id, [FromQuery] Guid tenantId)
    {
        var command = new DeleteIncomeCommand(id, tenantId);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
