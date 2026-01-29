using Lessons.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lessons.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all lessons");
        var result = await _mediator.Send(new GetLessonsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonDto>> GetLessonById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting lesson {LessonId}", id);
        var result = await _mediator.Send(new GetLessonByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<LessonDto>> CreateLesson(
        [FromBody] CreateLessonCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating lesson {Title}", command.Title);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLessonById), new { id = result.LessonId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LessonDto>> UpdateLesson(
        Guid id,
        [FromBody] UpdateLessonCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.LessonId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating lesson {LessonId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLesson(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting lesson {LessonId}", id);
        var result = await _mediator.Send(new DeleteLessonCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
