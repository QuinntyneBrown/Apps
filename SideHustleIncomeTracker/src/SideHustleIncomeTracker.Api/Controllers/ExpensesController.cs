using SideHustleIncomeTracker.Api.Features.Expenses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SideHustleIncomeTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExpensesController> _logger;

    public ExpensesController(IMediator mediator, ILogger<ExpensesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses(
        [FromQuery] Guid? businessId,
        [FromQuery] bool? isTaxDeductible,
        [FromQuery] string? category)
    {
        _logger.LogInformation("Getting expenses");

        var result = await _mediator.Send(new GetExpensesQuery
        {
            BusinessId = businessId,
            IsTaxDeductible = isTaxDeductible,
            Category = category,
        });

        return Ok(result);
    }

    [HttpGet("{expenseId:guid}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> GetExpenseById(Guid expenseId)
    {
        _logger.LogInformation("Getting expense {ExpenseId}", expenseId);

        var result = await _mediator.Send(new GetExpenseByIdQuery { ExpenseId = expenseId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseCommand command)
    {
        _logger.LogInformation("Creating expense for business {BusinessId}", command.BusinessId);

        var result = await _mediator.Send(command);

        return Created($"/api/expenses/{result.ExpenseId}", result);
    }

    [HttpPut("{expenseId:guid}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> UpdateExpense(Guid expenseId, [FromBody] UpdateExpenseCommand command)
    {
        if (expenseId != command.ExpenseId)
        {
            return BadRequest("Expense ID mismatch");
        }

        _logger.LogInformation("Updating expense {ExpenseId}", expenseId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{expenseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid expenseId)
    {
        _logger.LogInformation("Deleting expense {ExpenseId}", expenseId);

        var result = await _mediator.Send(new DeleteExpenseCommand { ExpenseId = expenseId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
