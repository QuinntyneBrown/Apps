using BookReadingTrackerLibrary.Api.Features.ReadingLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReadingTrackerLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReadingLogsController> _logger;

    public ReadingLogsController(IMediator mediator, ILogger<ReadingLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadingLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReadingLogDto>>> GetReadingLogs(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? bookId)
    {
        _logger.LogInformation("Getting reading logs for user {UserId}", userId);

        var result = await _mediator.Send(new GetReadingLogsQuery
        {
            UserId = userId,
            BookId = bookId,
        });

        return Ok(result);
    }

    [HttpGet("{readingLogId:guid}")]
    [ProducesResponseType(typeof(ReadingLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadingLogDto>> GetReadingLogById(Guid readingLogId)
    {
        _logger.LogInformation("Getting reading log {ReadingLogId}", readingLogId);

        var result = await _mediator.Send(new GetReadingLogByIdQuery { ReadingLogId = readingLogId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReadingLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadingLogDto>> CreateReadingLog([FromBody] CreateReadingLogCommand command)
    {
        _logger.LogInformation("Creating reading log for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/readinglogs/{result.ReadingLogId}", result);
    }

    [HttpPut("{readingLogId:guid}")]
    [ProducesResponseType(typeof(ReadingLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadingLogDto>> UpdateReadingLog(Guid readingLogId, [FromBody] UpdateReadingLogCommand command)
    {
        if (readingLogId != command.ReadingLogId)
        {
            return BadRequest("Reading log ID mismatch");
        }

        _logger.LogInformation("Updating reading log {ReadingLogId}", readingLogId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{readingLogId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReadingLog(Guid readingLogId)
    {
        _logger.LogInformation("Deleting reading log {ReadingLogId}", readingLogId);

        var result = await _mediator.Send(new DeleteReadingLogCommand { ReadingLogId = readingLogId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
