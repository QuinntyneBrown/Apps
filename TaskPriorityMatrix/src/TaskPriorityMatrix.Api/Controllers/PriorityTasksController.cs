// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using TaskPriorityMatrix.Api.Features.Tasks;
using TaskPriorityMatrix.Core;

namespace TaskPriorityMatrix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriorityTasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PriorityTaskDto>> GetPriorityTasks(
        [FromQuery] Urgency? urgency = null,
        [FromQuery] Importance? importance = null,
        [FromQuery] TaskStatus? status = null,
        [FromQuery] Guid? categoryId = null)
    {
        // Placeholder: Return empty list for now
        return Ok(new List<PriorityTaskDto>());
    }

    [HttpGet("{id}")]
    public ActionResult<PriorityTaskDto> GetPriorityTask(Guid id)
    {
        // Placeholder: Return null for now
        return NotFound();
    }

    [HttpPost]
    public ActionResult<PriorityTaskDto> CreatePriorityTask([FromBody] CreatePriorityTaskCommand command)
    {
        // Placeholder: Return created task
        var task = new PriorityTaskDto
        {
            PriorityTaskId = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Urgency = command.Urgency,
            Importance = command.Importance,
            Status = TaskStatus.NotStarted,
            DueDate = command.DueDate,
            CategoryId = command.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
        return CreatedAtAction(nameof(GetPriorityTask), new { id = task.PriorityTaskId }, task);
    }

    [HttpPut("{id}")]
    public ActionResult<PriorityTaskDto> UpdatePriorityTask(Guid id, [FromBody] UpdatePriorityTaskCommand command)
    {
        // Placeholder: Return updated task
        var task = new PriorityTaskDto
        {
            PriorityTaskId = command.PriorityTaskId,
            Title = command.Title,
            Description = command.Description,
            Urgency = command.Urgency,
            Importance = command.Importance,
            Status = command.Status,
            DueDate = command.DueDate,
            CategoryId = command.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePriorityTask(Guid id)
    {
        // Placeholder: Return success
        return NoContent();
    }
}
