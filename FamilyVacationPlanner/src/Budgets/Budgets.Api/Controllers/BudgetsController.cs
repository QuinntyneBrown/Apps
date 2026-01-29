using Budgets.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgets.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BudgetsController> _logger;

    public BudgetsController(IMediator mediator, ILogger<BudgetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgets(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBudgetsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BudgetDto>> GetBudgetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBudgetByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<BudgetDto>> CreateBudget(
        [FromBody] CreateBudgetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBudgetById), new { id = result.VacationBudgetId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BudgetDto>> UpdateBudget(
        Guid id,
        [FromBody] UpdateBudgetCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.VacationBudgetId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteBudgetCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
