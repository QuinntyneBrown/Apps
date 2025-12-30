using MealPrepPlanner.Api.Features.MealPlans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MealPrepPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealPlansController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MealPlansController> _logger;

    public MealPlansController(IMediator mediator, ILogger<MealPlansController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MealPlanDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MealPlanDto>>> GetMealPlans(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting meal plans for user {UserId}", userId);

        var result = await _mediator.Send(new GetMealPlansQuery
        {
            UserId = userId,
            IsActive = isActive,
        });

        return Ok(result);
    }

    [HttpGet("{mealPlanId:guid}")]
    [ProducesResponseType(typeof(MealPlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealPlanDto>> GetMealPlanById(Guid mealPlanId)
    {
        _logger.LogInformation("Getting meal plan {MealPlanId}", mealPlanId);

        var result = await _mediator.Send(new GetMealPlanByIdQuery { MealPlanId = mealPlanId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MealPlanDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MealPlanDto>> CreateMealPlan([FromBody] CreateMealPlanCommand command)
    {
        _logger.LogInformation("Creating meal plan for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/mealplans/{result.MealPlanId}", result);
    }

    [HttpPut("{mealPlanId:guid}")]
    [ProducesResponseType(typeof(MealPlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealPlanDto>> UpdateMealPlan(Guid mealPlanId, [FromBody] UpdateMealPlanCommand command)
    {
        if (mealPlanId != command.MealPlanId)
        {
            return BadRequest("Meal plan ID mismatch");
        }

        _logger.LogInformation("Updating meal plan {MealPlanId}", mealPlanId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{mealPlanId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMealPlan(Guid mealPlanId)
    {
        _logger.LogInformation("Deleting meal plan {MealPlanId}", mealPlanId);

        var result = await _mediator.Send(new DeleteMealPlanCommand { MealPlanId = mealPlanId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
