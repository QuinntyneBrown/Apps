using PersonalWiki.Api.Features.WikiCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWiki.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WikiCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WikiCategoriesController> _logger;

    public WikiCategoriesController(IMediator mediator, ILogger<WikiCategoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WikiCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WikiCategoryDto>>> GetWikiCategories(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? parentCategoryId,
        [FromQuery] bool? includeRootOnly)
    {
        _logger.LogInformation("Getting wiki categories for user {UserId}", userId);

        var result = await _mediator.Send(new GetWikiCategoriesQuery
        {
            UserId = userId,
            ParentCategoryId = parentCategoryId,
            IncludeRootOnly = includeRootOnly,
        });

        return Ok(result);
    }

    [HttpGet("{wikiCategoryId:guid}")]
    [ProducesResponseType(typeof(WikiCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WikiCategoryDto>> GetWikiCategoryById(Guid wikiCategoryId)
    {
        _logger.LogInformation("Getting wiki category {WikiCategoryId}", wikiCategoryId);

        var result = await _mediator.Send(new GetWikiCategoryByIdQuery { WikiCategoryId = wikiCategoryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WikiCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WikiCategoryDto>> CreateWikiCategory([FromBody] CreateWikiCategoryCommand command)
    {
        _logger.LogInformation("Creating wiki category for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/wikicategories/{result.WikiCategoryId}", result);
    }

    [HttpPut("{wikiCategoryId:guid}")]
    [ProducesResponseType(typeof(WikiCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WikiCategoryDto>> UpdateWikiCategory(Guid wikiCategoryId, [FromBody] UpdateWikiCategoryCommand command)
    {
        if (wikiCategoryId != command.WikiCategoryId)
        {
            return BadRequest("Wiki category ID mismatch");
        }

        _logger.LogInformation("Updating wiki category {WikiCategoryId}", wikiCategoryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{wikiCategoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWikiCategory(Guid wikiCategoryId)
    {
        _logger.LogInformation("Deleting wiki category {WikiCategoryId}", wikiCategoryId);

        var result = await _mediator.Send(new DeleteWikiCategoryCommand { WikiCategoryId = wikiCategoryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
