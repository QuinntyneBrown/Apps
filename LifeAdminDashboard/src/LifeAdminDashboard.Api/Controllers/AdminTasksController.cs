using LifeAdminDashboard.Api.Features.AdminTasks;
using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeAdminDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminTasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AdminTasksController> _logger;

    public AdminTasksController(IMediator mediator, ILogger<AdminTasksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AdminTaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AdminTaskDto>>> GetAdminTasks(
        [FromQuery] Guid? userId,
        [FromQuery] TaskCategory? category,
        [FromQuery] TaskPriority? priority,
        [FromQuery] bool? isCompleted,
        [FromQuery] bool? isOverdue)
    {
        _logger.LogInformation("Getting admin tasks for user {UserId}", userId);

        var result = await _mediator.Send(new GetAdminTasksQuery
        {
            UserId = userId,
            Category = category,
            Priority = priority,
            IsCompleted = isCompleted,
            IsOverdue = isOverdue,
        });

        return Ok(result);
    }

    [HttpGet("{adminTaskId:guid}")]
    [ProducesResponseType(typeof(AdminTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminTaskDto>> GetAdminTaskById(Guid adminTaskId)
    {
        _logger.LogInformation("Getting admin task {AdminTaskId}", adminTaskId);

        var result = await _mediator.Send(new GetAdminTaskByIdQuery { AdminTaskId = adminTaskId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AdminTaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdminTaskDto>> CreateAdminTask([FromBody] CreateAdminTaskCommand command)
    {
        _logger.LogInformation("Creating admin task for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/admintasks/{result.AdminTaskId}", result);
    }

    [HttpPut("{adminTaskId:guid}")]
    [ProducesResponseType(typeof(AdminTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminTaskDto>> UpdateAdminTask(Guid adminTaskId, [FromBody] UpdateAdminTaskCommand command)
    {
        if (adminTaskId != command.AdminTaskId)
        {
            return BadRequest("Admin task ID mismatch");
        }

        _logger.LogInformation("Updating admin task {AdminTaskId}", adminTaskId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{adminTaskId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAdminTask(Guid adminTaskId)
    {
        _logger.LogInformation("Deleting admin task {AdminTaskId}", adminTaskId);

        var result = await _mediator.Send(new DeleteAdminTaskCommand { AdminTaskId = adminTaskId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
