using PersonalLibraryLessonsLearned.Api.Features.Lesson;
using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalLibraryLessonsLearned.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LessonsController> _logger;

    public LessonsController(IMediator mediator, ILogger<LessonsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LessonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? sourceId,
        [FromQuery] LessonCategory? category,
        [FromQuery] bool? isApplied)
    {
        _logger.LogInformation("Getting lessons for user {UserId}", userId);

        var result = await _mediator.Send(new GetLessonsQuery
        {
            UserId = userId,
            SourceId = sourceId,
            Category = category,
            IsApplied = isApplied,
        });

        return Ok(result);
    }

    [HttpGet("{lessonId:guid}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonDto>> GetLessonById(Guid lessonId)
    {
        _logger.LogInformation("Getting lesson {LessonId}", lessonId);

        var result = await _mediator.Send(new GetLessonByIdQuery { LessonId = lessonId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LessonDto>> CreateLesson([FromBody] CreateLessonCommand command)
    {
        _logger.LogInformation("Creating lesson for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/lessons/{result.LessonId}", result);
    }

    [HttpPut("{lessonId:guid}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonDto>> UpdateLesson(Guid lessonId, [FromBody] UpdateLessonCommand command)
    {
        if (lessonId != command.LessonId)
        {
            return BadRequest("Lesson ID mismatch");
        }

        _logger.LogInformation("Updating lesson {LessonId}", lessonId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{lessonId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLesson(Guid lessonId)
    {
        _logger.LogInformation("Deleting lesson {LessonId}", lessonId);

        var result = await _mediator.Send(new DeleteLessonCommand { LessonId = lessonId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
