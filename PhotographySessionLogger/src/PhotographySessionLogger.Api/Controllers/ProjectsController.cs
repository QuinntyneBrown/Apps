using PhotographySessionLogger.Api.Features.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PhotographySessionLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IMediator mediator, ILogger<ProjectsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isCompleted)
    {
        _logger.LogInformation("Getting projects for user {UserId}", userId);

        var result = await _mediator.Send(new GetProjectsQuery
        {
            UserId = userId,
            IsCompleted = isCompleted,
        });

        return Ok(result);
    }

    [HttpGet("{projectId:guid}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProjectById(Guid projectId)
    {
        _logger.LogInformation("Getting project {ProjectId}", projectId);

        var result = await _mediator.Send(new GetProjectByIdQuery { ProjectId = projectId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectCommand command)
    {
        _logger.LogInformation("Creating project for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/projects/{result.ProjectId}", result);
    }

    [HttpPut("{projectId:guid}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid projectId, [FromBody] UpdateProjectCommand command)
    {
        if (projectId != command.ProjectId)
        {
            return BadRequest("Project ID mismatch");
        }

        _logger.LogInformation("Updating project {ProjectId}", projectId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{projectId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(Guid projectId)
    {
        _logger.LogInformation("Deleting project {ProjectId}", projectId);

        var result = await _mediator.Send(new DeleteProjectCommand { ProjectId = projectId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
