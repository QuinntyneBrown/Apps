using SubscriptionAuditTool.Api.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SubscriptionAuditTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        _logger.LogInformation("Getting all categories");

        var result = await _mediator.Send(new GetCategoriesQuery());

        return Ok(result);
    }

    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid categoryId)
    {
        _logger.LogInformation("Getting category {CategoryId}", categoryId);

        var result = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = categoryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        _logger.LogInformation("Creating category {CategoryName}", command.Name);

        var result = await _mediator.Send(command);

        return Created($"/api/categories/{result.CategoryId}", result);
    }

    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(
        Guid categoryId,
        [FromBody] UpdateCategoryCommand command)
    {
        if (categoryId != command.CategoryId)
        {
            return BadRequest("Category ID mismatch");
        }

        _logger.LogInformation("Updating category {CategoryId}", categoryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        _logger.LogInformation("Deleting category {CategoryId}", categoryId);

        var result = await _mediator.Send(new DeleteCategoryCommand { CategoryId = categoryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
