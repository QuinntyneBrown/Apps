using PersonalWiki.Api.Features.PageLinks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWiki.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PageLinksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PageLinksController> _logger;

    public PageLinksController(IMediator mediator, ILogger<PageLinksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PageLinkDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PageLinkDto>>> GetPageLinks(
        [FromQuery] Guid? sourcePageId,
        [FromQuery] Guid? targetPageId)
    {
        _logger.LogInformation("Getting page links");

        var result = await _mediator.Send(new GetPageLinksQuery
        {
            SourcePageId = sourcePageId,
            TargetPageId = targetPageId,
        });

        return Ok(result);
    }

    [HttpGet("{pageLinkId:guid}")]
    [ProducesResponseType(typeof(PageLinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageLinkDto>> GetPageLinkById(Guid pageLinkId)
    {
        _logger.LogInformation("Getting page link {PageLinkId}", pageLinkId);

        var result = await _mediator.Send(new GetPageLinkByIdQuery { PageLinkId = pageLinkId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PageLinkDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PageLinkDto>> CreatePageLink([FromBody] CreatePageLinkCommand command)
    {
        _logger.LogInformation("Creating page link from {SourcePageId} to {TargetPageId}", command.SourcePageId, command.TargetPageId);

        var result = await _mediator.Send(command);

        return Created($"/api/pagelinks/{result.PageLinkId}", result);
    }

    [HttpPut("{pageLinkId:guid}")]
    [ProducesResponseType(typeof(PageLinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageLinkDto>> UpdatePageLink(Guid pageLinkId, [FromBody] UpdatePageLinkCommand command)
    {
        if (pageLinkId != command.PageLinkId)
        {
            return BadRequest("Page link ID mismatch");
        }

        _logger.LogInformation("Updating page link {PageLinkId}", pageLinkId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{pageLinkId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePageLink(Guid pageLinkId)
    {
        _logger.LogInformation("Deleting page link {PageLinkId}", pageLinkId);

        var result = await _mediator.Send(new DeletePageLinkCommand { PageLinkId = pageLinkId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
