// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// API controller for managing expenses.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExpensesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpensesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ExpensesController(IMediator mediator, ILogger<ExpensesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets an expense by ID.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <returns>The expense.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting expense {ExpenseId}", id);

        var result = await _mediator.Send(new GetExpenseByIdQuery { ExpenseId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets expenses, optionally filtered by budget ID.
    /// </summary>
    /// <param name="budgetId">The budget ID filter.</param>
    /// <returns>The list of expenses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll([FromQuery] Guid? budgetId = null)
    {
        _logger.LogInformation("Getting expenses for budget {BudgetId}", budgetId);

        var result = await _mediator.Send(new GetExpensesQuery { BudgetId = budgetId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created expense.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> Create([FromBody] CreateExpenseCommand command)
    {
        _logger.LogInformation(
            "Creating expense {Description}",
            command.Description);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.ExpenseId },
            result);
    }

    /// <summary>
    /// Updates an existing expense.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated expense.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> Update(Guid id, [FromBody] UpdateExpenseCommand command)
    {
        if (id != command.ExpenseId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating expense {ExpenseId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an expense.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting expense {ExpenseId}", id);

        var result = await _mediator.Send(new DeleteExpenseCommand { ExpenseId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
