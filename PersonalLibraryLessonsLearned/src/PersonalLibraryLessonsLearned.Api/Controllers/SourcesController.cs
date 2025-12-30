using PersonalLibraryLessonsLearned.Api.Features.Source;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalLibraryLessonsLearned.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SourcesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SourcesController> _logger;

    public SourcesController(IMediator mediator, ILogger<SourcesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SourceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SourceDto>>> GetSources(
        [FromQuery] Guid? userId,
        [FromQuery] string? sourceType)
    {
        _logger.LogInformation("Getting sources for user {UserId}", userId);

        var result = await _mediator.Send(new GetSourcesQuery
        {
            UserId = userId,
            SourceType = sourceType,
        });

        return Ok(result);
    }

    [HttpGet("{sourceId:guid}")]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SourceDto>> GetSourceById(Guid sourceId)
    {
        _logger.LogInformation("Getting source {SourceId}", sourceId);

        var result = await _mediator.Send(new GetSourceByIdQuery { SourceId = sourceId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SourceDto>> CreateSource([FromBody] CreateSourceCommand command)
    {
        _logger.LogInformation("Creating source for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/sources/{result.SourceId}", result);
    }

    [HttpPut("{sourceId:guid}")]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SourceDto>> UpdateSource(Guid sourceId, [FromBody] UpdateSourceCommand command)
    {
        if (sourceId != command.SourceId)
        {
            return BadRequest("Source ID mismatch");
        }

        _logger.LogInformation("Updating source {SourceId}", sourceId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{sourceId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSource(Guid sourceId)
    {
        _logger.LogInformation("Deleting source {SourceId}", sourceId);

        var result = await _mediator.Send(new DeleteSourceCommand { SourceId = sourceId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
