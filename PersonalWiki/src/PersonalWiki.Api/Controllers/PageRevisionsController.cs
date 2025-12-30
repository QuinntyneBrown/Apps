using PersonalWiki.Api.Features.PageRevisions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWiki.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PageRevisionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PageRevisionsController> _logger;

    public PageRevisionsController(IMediator mediator, ILogger<PageRevisionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PageRevisionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PageRevisionDto>>> GetPageRevisions(
        [FromQuery] Guid? wikiPageId)
    {
        _logger.LogInformation("Getting page revisions for page {WikiPageId}", wikiPageId);

        var result = await _mediator.Send(new GetPageRevisionsQuery
        {
            WikiPageId = wikiPageId,
        });

        return Ok(result);
    }

    [HttpGet("{pageRevisionId:guid}")]
    [ProducesResponseType(typeof(PageRevisionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageRevisionDto>> GetPageRevisionById(Guid pageRevisionId)
    {
        _logger.LogInformation("Getting page revision {PageRevisionId}", pageRevisionId);

        var result = await _mediator.Send(new GetPageRevisionByIdQuery { PageRevisionId = pageRevisionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PageRevisionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PageRevisionDto>> CreatePageRevision([FromBody] CreatePageRevisionCommand command)
    {
        _logger.LogInformation("Creating page revision for page {WikiPageId}", command.WikiPageId);

        var result = await _mediator.Send(command);

        return Created($"/api/pagerevisions/{result.PageRevisionId}", result);
    }

    [HttpPut("{pageRevisionId:guid}")]
    [ProducesResponseType(typeof(PageRevisionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageRevisionDto>> UpdatePageRevision(Guid pageRevisionId, [FromBody] UpdatePageRevisionCommand command)
    {
        if (pageRevisionId != command.PageRevisionId)
        {
            return BadRequest("Page revision ID mismatch");
        }

        _logger.LogInformation("Updating page revision {PageRevisionId}", pageRevisionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{pageRevisionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePageRevision(Guid pageRevisionId)
    {
        _logger.LogInformation("Deleting page revision {PageRevisionId}", pageRevisionId);

        var result = await _mediator.Send(new DeletePageRevisionCommand { PageRevisionId = pageRevisionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
