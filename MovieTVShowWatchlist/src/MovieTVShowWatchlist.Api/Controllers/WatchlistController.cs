using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTVShowWatchlist.Api;

[ApiController]
[Route("api/[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WatchlistController> _logger;

    public WatchlistController(IMediator mediator, ILogger<WatchlistController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<WatchlistItemDto>>> GetWatchlist(
        [FromQuery] Guid userId,
        [FromQuery] string? sortBy,
        [FromQuery] string? filterByGenre,
        [FromQuery] string? filterByPriority,
        [FromQuery] string? filterByMood,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetWatchlist.Query(
            userId, sortBy, filterByGenre, filterByPriority, filterByMood, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<WatchlistItemDto>> GetWatchlistItem(
        [FromQuery] Guid userId,
        Guid itemId)
    {
        var result = await _mediator.Send(new GetWatchlistItem.Query(userId, itemId));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost("movies")]
    public async Task<ActionResult<WatchlistItemDto>> AddMovieToWatchlist(
        [FromQuery] Guid userId,
        [FromBody] AddMovieToWatchlistRequest request)
    {
        try
        {
            var result = await _mediator.Send(new AddMovieToWatchlist.Command(
                userId,
                request.MovieId,
                request.Title,
                request.ReleaseYear,
                request.Genres,
                request.Director,
                request.Runtime,
                request.PriorityLevel,
                request.RecommendationSource,
                request.Availability));
            return CreatedAtAction(nameof(GetWatchlistItem), new { itemId = result.WatchlistItemId, userId }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("tvshows")]
    public async Task<ActionResult<WatchlistItemDto>> AddTVShowToWatchlist(
        [FromQuery] Guid userId,
        [FromBody] AddTVShowToWatchlistRequest request)
    {
        try
        {
            var result = await _mediator.Send(new AddTVShowToWatchlist.Command(
                userId,
                request.ShowId,
                request.Title,
                request.PremiereYear,
                request.Genres,
                request.NumberOfSeasons,
                request.Status,
                request.Priority,
                request.StreamingPlatform));
            return CreatedAtAction(nameof(GetWatchlistItem), new { itemId = result.WatchlistItemId, userId }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> RemoveFromWatchlist(
        [FromQuery] Guid userId,
        Guid itemId,
        [FromBody] RemoveWatchlistItemRequest request)
    {
        var result = await _mediator.Send(new RemoveFromWatchlist.Command(
            userId, itemId, request.RemovalReason, request.AlternativeAdded));
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("prioritize")]
    public async Task<ActionResult<List<WatchlistItemDto>>> PrioritizeWatchlist(
        [FromQuery] Guid userId,
        [FromBody] PrioritizeWatchlistRequest request)
    {
        var result = await _mediator.Send(new PrioritizeWatchlist.Command(
            userId, request.ItemRankings, request.SortingCriteria, request.MoodBasedCategories));
        return Ok(result);
    }
}
