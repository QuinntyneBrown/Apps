using KnowledgeBaseSecondBrain.Api.Features.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseSecondBrain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TagsController> _logger;

    public TagsController(IMediator mediator, ILogger<TagsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TagDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags([FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting tags for user {UserId}", userId);

        var result = await _mediator.Send(new GetTagsQuery { UserId = userId });

        return Ok(result);
    }

    [HttpGet("{tagId:guid}")]
    [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDto>> GetTagById(Guid tagId)
    {
        _logger.LogInformation("Getting tag {TagId}", tagId);

        var result = await _mediator.Send(new GetTagByIdQuery { TagId = tagId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TagDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TagDto>> CreateTag([FromBody] CreateTagCommand command)
    {
        _logger.LogInformation("Creating tag for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/tags/{result.TagId}", result);
    }

    [HttpPut("{tagId:guid}")]
    [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDto>> UpdateTag(Guid tagId, [FromBody] UpdateTagCommand command)
    {
        if (tagId != command.TagId)
        {
            return BadRequest("Tag ID mismatch");
        }

        _logger.LogInformation("Updating tag {TagId}", tagId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{tagId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTag(Guid tagId)
    {
        _logger.LogInformation("Deleting tag {TagId}", tagId);

        var result = await _mediator.Send(new DeleteTagCommand { TagId = tagId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
