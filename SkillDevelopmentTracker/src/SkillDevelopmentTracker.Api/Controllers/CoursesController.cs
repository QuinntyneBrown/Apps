using SkillDevelopmentTracker.Api.Features.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SkillDevelopmentTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(IMediator mediator, ILogger<CoursesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(
        [FromQuery] Guid? userId,
        [FromQuery] string? provider,
        [FromQuery] bool? isCompleted)
    {
        _logger.LogInformation("Getting courses for user {UserId}", userId);

        var result = await _mediator.Send(new GetCoursesQuery
        {
            UserId = userId,
            Provider = provider,
            IsCompleted = isCompleted,
        });

        return Ok(result);
    }

    [HttpGet("{courseId:guid}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseDto>> GetCourseById(Guid courseId)
    {
        _logger.LogInformation("Getting course {CourseId}", courseId);

        var result = await _mediator.Send(new GetCourseByIdQuery { CourseId = courseId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseCommand command)
    {
        _logger.LogInformation("Creating course for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/courses/{result.CourseId}", result);
    }

    [HttpPut("{courseId:guid}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseDto>> UpdateCourse(Guid courseId, [FromBody] UpdateCourseCommand command)
    {
        if (courseId != command.CourseId)
        {
            return BadRequest("Course ID mismatch");
        }

        _logger.LogInformation("Updating course {CourseId}", courseId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{courseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        _logger.LogInformation("Deleting course {CourseId}", courseId);

        var result = await _mediator.Send(new DeleteCourseCommand { CourseId = courseId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
