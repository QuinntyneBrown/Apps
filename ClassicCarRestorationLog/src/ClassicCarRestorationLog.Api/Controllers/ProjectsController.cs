// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Features.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassicCarRestorationLog.Api.Controllers;

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
    public async Task<ActionResult<List<ProjectDto>>> GetProjects([FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting projects for user {UserId}", userId);
        var result = await _mediator.Send(new GetProjects.Query { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        _logger.LogInformation("Getting project {ProjectId}", id);
        var result = await _mediator.Send(new GetProjectById.Query { ProjectId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProject.Command command)
    {
        _logger.LogInformation("Creating project for user {UserId}", command.UserId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProject), new { id = result.ProjectId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid id, UpdateProject.Command command)
    {
        if (id != command.ProjectId)
        {
            return BadRequest("Project ID mismatch");
        }

        _logger.LogInformation("Updating project {ProjectId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        _logger.LogInformation("Deleting project {ProjectId}", id);
        var result = await _mediator.Send(new DeleteProject.Command { ProjectId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
