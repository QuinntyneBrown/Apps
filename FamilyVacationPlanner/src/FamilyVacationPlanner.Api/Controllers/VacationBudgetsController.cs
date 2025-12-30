using FamilyVacationPlanner.Api.Features.VacationBudgets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyVacationPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacationBudgetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VacationBudgetsController> _logger;

    public VacationBudgetsController(IMediator mediator, ILogger<VacationBudgetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VacationBudgetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VacationBudgetDto>>> GetVacationBudgets([FromQuery] Guid? tripId)
    {
        _logger.LogInformation("Getting vacation budgets for trip {TripId}", tripId);

        var result = await _mediator.Send(new GetVacationBudgetsQuery { TripId = tripId });

        return Ok(result);
    }

    [HttpGet("{vacationBudgetId:guid}")]
    [ProducesResponseType(typeof(VacationBudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VacationBudgetDto>> GetVacationBudgetById(Guid vacationBudgetId)
    {
        _logger.LogInformation("Getting vacation budget {VacationBudgetId}", vacationBudgetId);

        var result = await _mediator.Send(new GetVacationBudgetByIdQuery { VacationBudgetId = vacationBudgetId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VacationBudgetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VacationBudgetDto>> CreateVacationBudget([FromBody] CreateVacationBudgetCommand command)
    {
        _logger.LogInformation("Creating vacation budget for trip {TripId}", command.TripId);

        var result = await _mediator.Send(command);

        return Created($"/api/vacationbudgets/{result.VacationBudgetId}", result);
    }

    [HttpPut("{vacationBudgetId:guid}")]
    [ProducesResponseType(typeof(VacationBudgetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VacationBudgetDto>> UpdateVacationBudget(Guid vacationBudgetId, [FromBody] UpdateVacationBudgetCommand command)
    {
        if (vacationBudgetId != command.VacationBudgetId)
        {
            return BadRequest("Vacation Budget ID mismatch");
        }

        _logger.LogInformation("Updating vacation budget {VacationBudgetId}", vacationBudgetId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{vacationBudgetId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVacationBudget(Guid vacationBudgetId)
    {
        _logger.LogInformation("Deleting vacation budget {VacationBudgetId}", vacationBudgetId);

        var result = await _mediator.Send(new DeleteVacationBudgetCommand { VacationBudgetId = vacationBudgetId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
