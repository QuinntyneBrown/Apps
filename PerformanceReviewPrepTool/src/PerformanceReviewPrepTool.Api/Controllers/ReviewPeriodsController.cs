using PerformanceReviewPrepTool.Api.Features.ReviewPeriods;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PerformanceReviewPrepTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewPeriodsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReviewPeriodsController> _logger;

    public ReviewPeriodsController(IMediator mediator, ILogger<ReviewPeriodsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReviewPeriodDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReviewPeriodDto>>> GetReviewPeriods(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isCompleted,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting review periods for user {UserId}", userId);

        var result = await _mediator.Send(new GetReviewPeriodsQuery
        {
            UserId = userId,
            IsCompleted = isCompleted,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{reviewPeriodId:guid}")]
    [ProducesResponseType(typeof(ReviewPeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewPeriodDto>> GetReviewPeriodById(Guid reviewPeriodId)
    {
        _logger.LogInformation("Getting review period {ReviewPeriodId}", reviewPeriodId);

        var result = await _mediator.Send(new GetReviewPeriodByIdQuery { ReviewPeriodId = reviewPeriodId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReviewPeriodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReviewPeriodDto>> CreateReviewPeriod([FromBody] CreateReviewPeriodCommand command)
    {
        _logger.LogInformation("Creating review period for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/reviewperiods/{result.ReviewPeriodId}", result);
    }

    [HttpPut("{reviewPeriodId:guid}")]
    [ProducesResponseType(typeof(ReviewPeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewPeriodDto>> UpdateReviewPeriod(Guid reviewPeriodId, [FromBody] UpdateReviewPeriodCommand command)
    {
        if (reviewPeriodId != command.ReviewPeriodId)
        {
            return BadRequest("Review period ID mismatch");
        }

        _logger.LogInformation("Updating review period {ReviewPeriodId}", reviewPeriodId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{reviewPeriodId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReviewPeriod(Guid reviewPeriodId)
    {
        _logger.LogInformation("Deleting review period {ReviewPeriodId}", reviewPeriodId);

        var result = await _mediator.Send(new DeleteReviewPeriodCommand { ReviewPeriodId = reviewPeriodId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
