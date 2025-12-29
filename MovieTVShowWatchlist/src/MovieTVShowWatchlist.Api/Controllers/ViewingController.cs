using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTVShowWatchlist.Api;

[ApiController]
[Route("api/[controller]")]
public class ViewingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ViewingController> _logger;

    public ViewingController(IMediator mediator, ILogger<ViewingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("movies/{movieId}/watched")]
    public async Task<ActionResult<ViewingRecordDto>> MarkMovieWatched(
        [FromQuery] Guid userId,
        Guid movieId,
        [FromBody] MarkMovieWatchedRequest request)
    {
        var result = await _mediator.Send(new MarkMovieWatched.Command(
            userId,
            movieId,
            request.WatchDate,
            request.ViewingLocation,
            request.ViewingPlatform,
            request.WatchedWith,
            request.ViewingContext,
            request.IsRewatch));
        return CreatedAtAction(nameof(GetViewingHistory), new { userId }, result);
    }

    [HttpPost("episodes/{episodeId}/watched")]
    public async Task<ActionResult<EpisodeViewingRecordDto>> MarkEpisodeWatched(
        [FromQuery] Guid userId,
        Guid episodeId,
        [FromBody] MarkEpisodeWatchedRequest request)
    {
        try
        {
            var result = await _mediator.Send(new MarkEpisodeWatched.Command(
                userId,
                episodeId,
                request.WatchDate,
                request.Platform,
                request.BingeSessionId,
                request.ViewingDurationMinutes));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<PaginatedResult<ViewingRecordDto>>> GetViewingHistory(
        [FromQuery] Guid userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? contentType,
        [FromQuery] string? platform,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetViewingHistory.Query(
            userId, startDate, endDate, contentType, platform, page, pageSize));
        return Ok(result);
    }

    [HttpGet("progress/{showId}")]
    public async Task<ActionResult<ShowProgressDto>> GetShowProgress(
        [FromQuery] Guid userId,
        Guid showId)
    {
        var result = await _mediator.Send(new GetShowProgress.Query(userId, showId));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
