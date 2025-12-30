using BookReadingTrackerLibrary.Api.Features.Reviews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReadingTrackerLibrary.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? bookId)
    {
        _logger.LogInformation("Getting reviews for user {UserId}", userId);

        var result = await _mediator.Send(new GetReviewsQuery
        {
            UserId = userId,
            BookId = bookId,
        });

        return Ok(result);
    }

    [HttpGet("{reviewId:guid}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDto>> GetReviewById(Guid reviewId)
    {
        _logger.LogInformation("Getting review {ReviewId}", reviewId);

        var result = await _mediator.Send(new GetReviewByIdQuery { ReviewId = reviewId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewCommand command)
    {
        _logger.LogInformation("Creating review for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/reviews/{result.ReviewId}", result);
    }

    [HttpPut("{reviewId:guid}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDto>> UpdateReview(Guid reviewId, [FromBody] UpdateReviewCommand command)
    {
        if (reviewId != command.ReviewId)
        {
            return BadRequest("Review ID mismatch");
        }

        _logger.LogInformation("Updating review {ReviewId}", reviewId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{reviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        _logger.LogInformation("Deleting review {ReviewId}", reviewId);

        var result = await _mediator.Send(new DeleteReviewCommand { ReviewId = reviewId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
