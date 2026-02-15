using Tasks.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TasksController> _logger;

    public TasksController(IMediator mediator, ILogger<TasksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all tasks");
        var result = await _mediator.Send(new GetTasksQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTaskById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting task {TaskId}", id);
        var result = await _mediator.Send(new GetTaskByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskDto>> CreateTask(
        [FromBody] CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating task: {Title}", command.Title);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTaskById), new { id = result.PriorityTaskId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> UpdateTask(
        Guid id,
        [FromBody] UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PriorityTaskId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating task {TaskId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> CompleteTask(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Completing task {TaskId}", id);
        var result = await _mediator.Send(new CompleteTaskCommand(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting task {TaskId}", id);
        var result = await _mediator.Send(new DeleteTaskCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
