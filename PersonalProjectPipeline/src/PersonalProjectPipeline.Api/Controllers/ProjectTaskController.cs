// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectPipeline.Api.Features.ProjectTask;

namespace PersonalProjectPipeline.Api.Controllers;

/// <summary>
/// Controller for managing project tasks.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectTaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectTaskController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Gets all project tasks.
    /// </summary>
    /// <param name="projectId">Optional project ID to filter tasks.</param>
    /// <param name="milestoneId">Optional milestone ID to filter tasks.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of tasks.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ProjectTaskDto>>> GetTasks(
        [FromQuery] Guid? projectId,
        [FromQuery] Guid? milestoneId,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectTasksQuery
        {
            ProjectId = projectId,
            MilestoneId = milestoneId
        };
        var tasks = await _mediator.Send(query, cancellationToken);
        return Ok(tasks);
    }

    /// <summary>
    /// Gets a project task by ID.
    /// </summary>
    /// <param name="id">The task ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The task.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskDto>> GetTask(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectTaskByIdQuery { ProjectTaskId = id };
        var task = await _mediator.Send(query, cancellationToken);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    /// <summary>
    /// Creates a new project task.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created task.</returns>
    [HttpPost]
    public async Task<ActionResult<ProjectTaskDto>> CreateTask(
        CreateProjectTaskCommand command,
        CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = task.ProjectTaskId }, task);
    }

    /// <summary>
    /// Updates an existing project task.
    /// </summary>
    /// <param name="id">The task ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated task.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectTaskDto>> UpdateTask(
        Guid id,
        UpdateProjectTaskCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectTaskId)
        {
            return BadRequest("Task ID mismatch.");
        }

        try
        {
            var task = await _mediator.Send(command, cancellationToken);
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a project task.
    /// </summary>
    /// <param name="id">The task ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteProjectTaskCommand { ProjectTaskId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
