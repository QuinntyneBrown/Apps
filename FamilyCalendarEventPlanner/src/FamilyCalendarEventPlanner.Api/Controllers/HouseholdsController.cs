using FamilyCalendarEventPlanner.Api.Features.Households;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HouseholdsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HouseholdsController> _logger;

    public HouseholdsController(IMediator mediator, ILogger<HouseholdsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HouseholdDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HouseholdDto>>> GetHouseholds()
    {
        _logger.LogInformation("Getting all households");

        var result = await _mediator.Send(new GetHouseholdsQuery());

        return Ok(result);
    }

    [HttpGet("{householdId:guid}")]
    [ProducesResponseType(typeof(HouseholdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HouseholdDto>> GetHouseholdById(Guid householdId)
    {
        _logger.LogInformation("Getting household {HouseholdId}", householdId);

        var result = await _mediator.Send(new GetHouseholdByIdQuery { HouseholdId = householdId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(HouseholdDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HouseholdDto>> CreateHousehold([FromBody] CreateHouseholdCommand command)
    {
        _logger.LogInformation("Creating household with name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return Created($"/api/households/{result.HouseholdId}", result);
    }

    [HttpPut("{householdId:guid}")]
    [ProducesResponseType(typeof(HouseholdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HouseholdDto>> UpdateHousehold(Guid householdId, [FromBody] UpdateHouseholdCommand command)
    {
        if (householdId != command.HouseholdId)
        {
            return BadRequest("Household ID mismatch");
        }

        _logger.LogInformation("Updating household {HouseholdId}", householdId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{householdId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHousehold(Guid householdId)
    {
        _logger.LogInformation("Deleting household {HouseholdId}", householdId);

        var result = await _mediator.Send(new DeleteHouseholdCommand { HouseholdId = householdId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
