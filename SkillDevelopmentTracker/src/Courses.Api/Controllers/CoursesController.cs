using Courses.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CoursesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromQuery] Guid tenantId, [FromQuery] Guid userId) =>
        Ok(await _mediator.Send(new GetCoursesQuery(tenantId, userId)));

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id, [FromQuery] Guid tenantId)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery(id, tenantId));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCourse), new { id = result.CourseId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(Guid id, [FromBody] UpdateCourseCommand command)
    {
        if (id != command.CourseId) return BadRequest();
        var result = await _mediator.Send(command);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id, [FromQuery] Guid tenantId) =>
        await _mediator.Send(new DeleteCourseCommand(id, tenantId)) ? NoContent() : NotFound();
}
