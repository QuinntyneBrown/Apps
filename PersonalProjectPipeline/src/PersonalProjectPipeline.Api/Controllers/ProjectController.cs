// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectPipeline.Api.Features.Project;

namespace PersonalProjectPipeline.Api.Controllers;

/// <summary>
/// Controller for managing projects.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Gets all projects.
    /// </summary>
    /// <param name="userId">Optional user ID to filter projects.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of projects.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects(
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectsQuery { UserId = userId };
        var projects = await _mediator.Send(query, cancellationToken);
        return Ok(projects);
    }

    /// <summary>
    /// Gets a project by ID.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectByIdQuery { ProjectId = id };
        var project = await _mediator.Send(query, cancellationToken);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created project.</returns>
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated project.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(
        Guid id,
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectId)
        {
            return BadRequest("Project ID mismatch.");
        }

        try
        {
            var project = await _mediator.Send(command, cancellationToken);
            return Ok(project);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a project.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
