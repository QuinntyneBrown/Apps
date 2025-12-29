using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTVShowWatchlist.Api;

[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RatingsController> _logger;

    public RatingsController(IMediator mediator, ILogger<RatingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<RatingDto>>> GetRatings(
        [FromQuery] Guid userId,
        [FromQuery] string? contentType,
        [FromQuery] decimal? minRating,
        [FromQuery] decimal? maxRating,
        [FromQuery] string? sortBy,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetRatings.Query(
            userId, contentType, minRating, maxRating, sortBy, page, pageSize));
        return Ok(result);
    }

    [HttpPost("movies/{movieId}")]
    public async Task<ActionResult<RatingDto>> RateMovie(
        [FromQuery] Guid userId,
        Guid movieId,
        [FromBody] CreateRatingRequest request)
    {
        try
        {
            var result = await _mediator.Send(new CreateRating.Command(
                userId,
                movieId,
                "Movie",
                request.RatingValue,
                request.RatingScale,
                request.RatingDate,
                request.ViewingDate,
                request.IsRewatchRating,
                request.Mood));
            return CreatedAtAction(nameof(GetRatings), new { userId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("shows/{showId}")]
    public async Task<ActionResult<RatingDto>> RateTVShow(
        [FromQuery] Guid userId,
        Guid showId,
        [FromBody] CreateRatingRequest request)
    {
        try
        {
            var result = await _mediator.Send(new CreateRating.Command(
                userId,
                showId,
                "TVShow",
                request.RatingValue,
                request.RatingScale,
                request.RatingDate,
                request.ViewingDate,
                request.IsRewatchRating,
                request.Mood));
            return CreatedAtAction(nameof(GetRatings), new { userId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
