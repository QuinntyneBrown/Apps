using MealPrepPlanner.Api.Features.Nutritions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MealPrepPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NutritionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NutritionsController> _logger;

    public NutritionsController(IMediator mediator, ILogger<NutritionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NutritionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NutritionDto>>> GetNutritions(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? recipeId)
    {
        _logger.LogInformation("Getting nutritions for user {UserId}", userId);

        var result = await _mediator.Send(new GetNutritionsQuery
        {
            UserId = userId,
            RecipeId = recipeId,
        });

        return Ok(result);
    }

    [HttpGet("{nutritionId:guid}")]
    [ProducesResponseType(typeof(NutritionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NutritionDto>> GetNutritionById(Guid nutritionId)
    {
        _logger.LogInformation("Getting nutrition {NutritionId}", nutritionId);

        var result = await _mediator.Send(new GetNutritionByIdQuery { NutritionId = nutritionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NutritionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NutritionDto>> CreateNutrition([FromBody] CreateNutritionCommand command)
    {
        _logger.LogInformation("Creating nutrition for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/nutritions/{result.NutritionId}", result);
    }

    [HttpPut("{nutritionId:guid}")]
    [ProducesResponseType(typeof(NutritionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NutritionDto>> UpdateNutrition(Guid nutritionId, [FromBody] UpdateNutritionCommand command)
    {
        if (nutritionId != command.NutritionId)
        {
            return BadRequest("Nutrition ID mismatch");
        }

        _logger.LogInformation("Updating nutrition {NutritionId}", nutritionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{nutritionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNutrition(Guid nutritionId)
    {
        _logger.LogInformation("Deleting nutrition {NutritionId}", nutritionId);

        var result = await _mediator.Send(new DeleteNutritionCommand { NutritionId = nutritionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
