using PerformanceReviewPrepTool.Api.Features.Feedbacks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PerformanceReviewPrepTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbacksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FeedbacksController> _logger;

    public FeedbacksController(IMediator mediator, ILogger<FeedbacksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FeedbackDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetFeedbacks(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? reviewPeriodId,
        [FromQuery] bool? isKeyFeedback,
        [FromQuery] string? feedbackType,
        [FromQuery] string? category)
    {
        _logger.LogInformation("Getting feedbacks for user {UserId}", userId);

        var result = await _mediator.Send(new GetFeedbacksQuery
        {
            UserId = userId,
            ReviewPeriodId = reviewPeriodId,
            IsKeyFeedback = isKeyFeedback,
            FeedbackType = feedbackType,
            Category = category,
        });

        return Ok(result);
    }

    [HttpGet("{feedbackId:guid}")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FeedbackDto>> GetFeedbackById(Guid feedbackId)
    {
        _logger.LogInformation("Getting feedback {FeedbackId}", feedbackId);

        var result = await _mediator.Send(new GetFeedbackByIdQuery { FeedbackId = feedbackId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FeedbackDto>> CreateFeedback([FromBody] CreateFeedbackCommand command)
    {
        _logger.LogInformation("Creating feedback for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/feedbacks/{result.FeedbackId}", result);
    }

    [HttpPut("{feedbackId:guid}")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FeedbackDto>> UpdateFeedback(Guid feedbackId, [FromBody] UpdateFeedbackCommand command)
    {
        if (feedbackId != command.FeedbackId)
        {
            return BadRequest("Feedback ID mismatch");
        }

        _logger.LogInformation("Updating feedback {FeedbackId}", feedbackId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{feedbackId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFeedback(Guid feedbackId)
    {
        _logger.LogInformation("Deleting feedback {FeedbackId}", feedbackId);

        var result = await _mediator.Send(new DeleteFeedbackCommand { FeedbackId = feedbackId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
