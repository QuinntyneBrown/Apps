using MusicCollectionOrganizer.Api.Features.ListeningLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicCollectionOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ListeningLogsController> _logger;

    public ListeningLogsController(IMediator mediator, ILogger<ListeningLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListeningLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ListeningLogDto>>> GetListeningLogs(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? albumId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int? minRating)
    {
        _logger.LogInformation("Getting listening logs for user {UserId}", userId);

        var result = await _mediator.Send(new GetListeningLogsQuery
        {
            UserId = userId,
            AlbumId = albumId,
            StartDate = startDate,
            EndDate = endDate,
            MinRating = minRating,
        });

        return Ok(result);
    }

    [HttpGet("{listeningLogId:guid}")]
    [ProducesResponseType(typeof(ListeningLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ListeningLogDto>> GetListeningLogById(Guid listeningLogId)
    {
        _logger.LogInformation("Getting listening log {ListeningLogId}", listeningLogId);

        var result = await _mediator.Send(new GetListeningLogByIdQuery { ListeningLogId = listeningLogId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ListeningLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListeningLogDto>> CreateListeningLog([FromBody] CreateListeningLogCommand command)
    {
        _logger.LogInformation("Creating listening log for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/listeninglogs/{result.ListeningLogId}", result);
    }

    [HttpPut("{listeningLogId:guid}")]
    [ProducesResponseType(typeof(ListeningLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ListeningLogDto>> UpdateListeningLog(Guid listeningLogId, [FromBody] UpdateListeningLogCommand command)
    {
        if (listeningLogId != command.ListeningLogId)
        {
            return BadRequest("Listening log ID mismatch");
        }

        _logger.LogInformation("Updating listening log {ListeningLogId}", listeningLogId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{listeningLogId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteListeningLog(Guid listeningLogId)
    {
        _logger.LogInformation("Deleting listening log {ListeningLogId}", listeningLogId);

        var result = await _mediator.Send(new DeleteListeningLogCommand { ListeningLogId = listeningLogId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
