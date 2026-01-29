using Projects.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Projects.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all projects");
        var result = await _mediator.Send(new GetProjectsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting project {ProjectId}", id);
        var result = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ProjectDto>> CreateProject(
        [FromBody] CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating project for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProjectById), new { id = result.ProjectId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> UpdateProject(
        Guid id,
        [FromBody] UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating project {ProjectId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting project {ProjectId}", id);
        var result = await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
