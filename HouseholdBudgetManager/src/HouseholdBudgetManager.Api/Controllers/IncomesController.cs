// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// API controller for managing incomes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IncomesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncomesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public IncomesController(IMediator mediator, ILogger<IncomesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets an income by ID.
    /// </summary>
    /// <param name="id">The income ID.</param>
    /// <returns>The income.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncomeDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting income {IncomeId}", id);

        var result = await _mediator.Send(new GetIncomeByIdQuery { IncomeId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets incomes, optionally filtered by budget ID.
    /// </summary>
    /// <param name="budgetId">The budget ID filter.</param>
    /// <returns>The list of incomes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IncomeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetAll([FromQuery] Guid? budgetId = null)
    {
        _logger.LogInformation("Getting incomes for budget {BudgetId}", budgetId);

        var result = await _mediator.Send(new GetIncomesQuery { BudgetId = budgetId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new income.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created income.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeDto>> Create([FromBody] CreateIncomeCommand command)
    {
        _logger.LogInformation(
            "Creating income {Description}",
            command.Description);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IncomeId },
            result);
    }

    /// <summary>
    /// Updates an existing income.
    /// </summary>
    /// <param name="id">The income ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated income.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeDto>> Update(Guid id, [FromBody] UpdateIncomeCommand command)
    {
        if (id != command.IncomeId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating income {IncomeId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an income.
    /// </summary>
    /// <param name="id">The income ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting income {IncomeId}", id);

        var result = await _mediator.Send(new DeleteIncomeCommand { IncomeId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
