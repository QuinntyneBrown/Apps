using MensGroupDiscussionTracker.Api.Features.Topics;
using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MensGroupDiscussionTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TopicsController> _logger;

    public TopicsController(IMediator mediator, ILogger<TopicsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TopicDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopics(
        [FromQuery] Guid? meetingId,
        [FromQuery] Guid? userId,
        [FromQuery] TopicCategory? category)
    {
        _logger.LogInformation("Getting topics for user {UserId}", userId);

        var result = await _mediator.Send(new GetTopicsQuery
        {
            MeetingId = meetingId,
            UserId = userId,
            Category = category,
        });

        return Ok(result);
    }

    [HttpGet("{topicId:guid}")]
    [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TopicDto>> GetTopicById(Guid topicId)
    {
        _logger.LogInformation("Getting topic {TopicId}", topicId);

        var result = await _mediator.Send(new GetTopicByIdQuery { TopicId = topicId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TopicDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TopicDto>> CreateTopic([FromBody] CreateTopicCommand command)
    {
        _logger.LogInformation("Creating topic for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/topics/{result.TopicId}", result);
    }

    [HttpPut("{topicId:guid}")]
    [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TopicDto>> UpdateTopic(Guid topicId, [FromBody] UpdateTopicCommand command)
    {
        if (topicId != command.TopicId)
        {
            return BadRequest("Topic ID mismatch");
        }

        _logger.LogInformation("Updating topic {TopicId}", topicId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{topicId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTopic(Guid topicId)
    {
        _logger.LogInformation("Deleting topic {TopicId}", topicId);

        var result = await _mediator.Send(new DeleteTopicCommand { TopicId = topicId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
