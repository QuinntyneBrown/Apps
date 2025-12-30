using ProfessionalReadingList.Api.Features.ReadingProgress;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalReadingList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingProgressController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReadingProgressController> _logger;

    public ReadingProgressController(IMediator mediator, ILogger<ReadingProgressController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadingProgressDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReadingProgressDto>>> GetReadingProgress(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? resourceId,
        [FromQuery] string? status)
    {
        _logger.LogInformation("Getting reading progress for user {UserId}", userId);

        var result = await _mediator.Send(new GetReadingProgressQuery
        {
            UserId = userId,
            ResourceId = resourceId,
            Status = status,
        });

        return Ok(result);
    }

    [HttpGet("{readingProgressId:guid}")]
    [ProducesResponseType(typeof(ReadingProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadingProgressDto>> GetReadingProgressById(Guid readingProgressId)
    {
        _logger.LogInformation("Getting reading progress {ReadingProgressId}", readingProgressId);

        var result = await _mediator.Send(new GetReadingProgressByIdQuery { ReadingProgressId = readingProgressId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReadingProgressDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadingProgressDto>> CreateReadingProgress([FromBody] CreateReadingProgressCommand command)
    {
        _logger.LogInformation("Creating reading progress for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/readingprogress/{result.ReadingProgressId}", result);
    }

    [HttpPut("{readingProgressId:guid}")]
    [ProducesResponseType(typeof(ReadingProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadingProgressDto>> UpdateReadingProgress(Guid readingProgressId, [FromBody] UpdateReadingProgressCommand command)
    {
        if (readingProgressId != command.ReadingProgressId)
        {
            return BadRequest("Reading progress ID mismatch");
        }

        _logger.LogInformation("Updating reading progress {ReadingProgressId}", readingProgressId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{readingProgressId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReadingProgress(Guid readingProgressId)
    {
        _logger.LogInformation("Deleting reading progress {ReadingProgressId}", readingProgressId);

        var result = await _mediator.Send(new DeleteReadingProgressCommand { ReadingProgressId = readingProgressId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
