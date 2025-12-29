using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTVShowWatchlist.Api;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReviewsController> _logger;

    public ReviewsController(IMediator mediator, ILogger<ReviewsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ReviewDto>>> GetReviews(
        [FromQuery] Guid userId,
        [FromQuery] string? contentType,
        [FromQuery] bool? hasSpoilers,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetReviews.Query(
            userId, contentType, hasSpoilers, page, pageSize));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview(
        [FromQuery] Guid userId,
        [FromBody] CreateReviewRequest request)
    {
        try
        {
            var result = await _mediator.Send(new CreateReview.Command(
                userId,
                request.ContentId,
                request.ContentType,
                request.ReviewText,
                request.HasSpoilers,
                request.ThemesDiscussed,
                request.WouldRecommend,
                request.TargetAudience));
            return CreatedAtAction(nameof(GetReviews), new { userId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
