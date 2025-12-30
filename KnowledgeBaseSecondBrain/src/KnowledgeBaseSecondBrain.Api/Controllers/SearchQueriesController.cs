using KnowledgeBaseSecondBrain.Api.Features.SearchQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseSecondBrain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchQueriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SearchQueriesController> _logger;

    public SearchQueriesController(IMediator mediator, ILogger<SearchQueriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SearchQueryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SearchQueryDto>>> GetSearchQueries(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isSaved)
    {
        _logger.LogInformation("Getting search queries for user {UserId}", userId);

        var result = await _mediator.Send(new GetSearchQueriesQuery
        {
            UserId = userId,
            IsSaved = isSaved,
        });

        return Ok(result);
    }

    [HttpGet("{searchQueryId:guid}")]
    [ProducesResponseType(typeof(SearchQueryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SearchQueryDto>> GetSearchQueryById(Guid searchQueryId)
    {
        _logger.LogInformation("Getting search query {SearchQueryId}", searchQueryId);

        var result = await _mediator.Send(new GetSearchQueryByIdQuery { SearchQueryId = searchQueryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SearchQueryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SearchQueryDto>> CreateSearchQuery([FromBody] CreateSearchQueryCommand command)
    {
        _logger.LogInformation("Creating search query for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/searchqueries/{result.SearchQueryId}", result);
    }

    [HttpPut("{searchQueryId:guid}")]
    [ProducesResponseType(typeof(SearchQueryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SearchQueryDto>> UpdateSearchQuery(Guid searchQueryId, [FromBody] UpdateSearchQueryCommand command)
    {
        if (searchQueryId != command.SearchQueryId)
        {
            return BadRequest("Search query ID mismatch");
        }

        _logger.LogInformation("Updating search query {SearchQueryId}", searchQueryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{searchQueryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSearchQuery(Guid searchQueryId)
    {
        _logger.LogInformation("Deleting search query {SearchQueryId}", searchQueryId);

        var result = await _mediator.Send(new DeleteSearchQueryCommand { SearchQueryId = searchQueryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
