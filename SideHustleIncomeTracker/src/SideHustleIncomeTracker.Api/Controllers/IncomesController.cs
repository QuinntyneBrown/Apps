using SideHustleIncomeTracker.Api.Features.Incomes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SideHustleIncomeTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IncomesController> _logger;

    public IncomesController(IMediator mediator, ILogger<IncomesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IncomeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes(
        [FromQuery] Guid? businessId,
        [FromQuery] bool? isPaid)
    {
        _logger.LogInformation("Getting incomes");

        var result = await _mediator.Send(new GetIncomesQuery
        {
            BusinessId = businessId,
            IsPaid = isPaid,
        });

        return Ok(result);
    }

    [HttpGet("{incomeId:guid}")]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncomeDto>> GetIncomeById(Guid incomeId)
    {
        _logger.LogInformation("Getting income {IncomeId}", incomeId);

        var result = await _mediator.Send(new GetIncomeByIdQuery { IncomeId = incomeId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeDto>> CreateIncome([FromBody] CreateIncomeCommand command)
    {
        _logger.LogInformation("Creating income for business {BusinessId}", command.BusinessId);

        var result = await _mediator.Send(command);

        return Created($"/api/incomes/{result.IncomeId}", result);
    }

    [HttpPut("{incomeId:guid}")]
    [ProducesResponseType(typeof(IncomeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncomeDto>> UpdateIncome(Guid incomeId, [FromBody] UpdateIncomeCommand command)
    {
        if (incomeId != command.IncomeId)
        {
            return BadRequest("Income ID mismatch");
        }

        _logger.LogInformation("Updating income {IncomeId}", incomeId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{incomeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIncome(Guid incomeId)
    {
        _logger.LogInformation("Deleting income {IncomeId}", incomeId);

        var result = await _mediator.Send(new DeleteIncomeCommand { IncomeId = incomeId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
