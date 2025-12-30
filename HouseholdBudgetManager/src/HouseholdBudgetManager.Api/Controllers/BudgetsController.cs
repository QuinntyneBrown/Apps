// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// API controller for managing budgets.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BudgetsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BudgetsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public BudgetsController(IMediator mediator, ILogger<BudgetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a budget by ID.
    /// </summary>
    /// <param name="id">The budget ID.</param>
    /// <returns>The budget.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BudgetDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting budget {BudgetId}", id);

        var result = await _mediator.Send(new GetBudgetByIdQuery { BudgetId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all budgets.
    /// </summary>
    /// <returns>The list of budgets.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAll()
    {
        _logger.LogInformation("Getting all budgets");

        var result = await _mediator.Send(new GetBudgetsQuery());

        return Ok(result);
    }

    /// <summary>
    /// Creates a new budget.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created budget.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BudgetDto>> Create([FromBody] CreateBudgetCommand command)
    {
        _logger.LogInformation(
            "Creating budget {Name}",
            command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.BudgetId },
            result);
    }

    /// <summary>
    /// Updates an existing budget.
    /// </summary>
    /// <param name="id">The budget ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated budget.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BudgetDto>> Update(Guid id, [FromBody] UpdateBudgetCommand command)
    {
        if (id != command.BudgetId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating budget {BudgetId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a budget.
    /// </summary>
    /// <param name="id">The budget ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting budget {BudgetId}", id);

        var result = await _mediator.Send(new DeleteBudgetCommand { BudgetId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
