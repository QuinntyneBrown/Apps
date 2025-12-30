using PersonalWiki.Api.Features.WikiPages;
using PersonalWiki.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWiki.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WikiPagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WikiPagesController> _logger;

    public WikiPagesController(IMediator mediator, ILogger<WikiPagesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WikiPageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WikiPageDto>>> GetWikiPages(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? categoryId,
        [FromQuery] PageStatus? status,
        [FromQuery] bool? isFeatured,
        [FromQuery] string? searchTerm)
    {
        _logger.LogInformation("Getting wiki pages for user {UserId}", userId);

        var result = await _mediator.Send(new GetWikiPagesQuery
        {
            UserId = userId,
            CategoryId = categoryId,
            Status = status,
            IsFeatured = isFeatured,
            SearchTerm = searchTerm,
        });

        return Ok(result);
    }

    [HttpGet("{wikiPageId:guid}")]
    [ProducesResponseType(typeof(WikiPageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WikiPageDto>> GetWikiPageById(Guid wikiPageId)
    {
        _logger.LogInformation("Getting wiki page {WikiPageId}", wikiPageId);

        var result = await _mediator.Send(new GetWikiPageByIdQuery { WikiPageId = wikiPageId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WikiPageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WikiPageDto>> CreateWikiPage([FromBody] CreateWikiPageCommand command)
    {
        _logger.LogInformation("Creating wiki page for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/wikipages/{result.WikiPageId}", result);
    }

    [HttpPut("{wikiPageId:guid}")]
    [ProducesResponseType(typeof(WikiPageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WikiPageDto>> UpdateWikiPage(Guid wikiPageId, [FromBody] UpdateWikiPageCommand command)
    {
        if (wikiPageId != command.WikiPageId)
        {
            return BadRequest("Wiki page ID mismatch");
        }

        _logger.LogInformation("Updating wiki page {WikiPageId}", wikiPageId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{wikiPageId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWikiPage(Guid wikiPageId)
    {
        _logger.LogInformation("Deleting wiki page {WikiPageId}", wikiPageId);

        var result = await _mediator.Send(new DeleteWikiPageCommand { WikiPageId = wikiPageId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
