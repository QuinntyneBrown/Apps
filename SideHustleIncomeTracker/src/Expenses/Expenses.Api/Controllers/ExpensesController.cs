using Expenses.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses([FromQuery] Guid tenantId, [FromQuery] Guid businessId)
    {
        var query = new GetExpensesQuery(tenantId, businessId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetExpense(Guid id, [FromQuery] Guid tenantId)
    {
        var query = new GetExpenseByIdQuery(id, tenantId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExpense), new { id = result.ExpenseId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> UpdateExpense(Guid id, [FromBody] UpdateExpenseCommand command)
    {
        if (id != command.ExpenseId) return BadRequest();
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id, [FromQuery] Guid tenantId)
    {
        var command = new DeleteExpenseCommand(id, tenantId);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
