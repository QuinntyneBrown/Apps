using AdminTasks.Api.Features.AdminTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminTasks.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<AdminTaskDto>>> GetAdminTasks([FromQuery] Guid? userId = null)
    {
        var result = await _mediator.Send(new GetAdminTasksQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<AdminTaskDto>> GetAdminTaskById(Guid taskId)
    {
        var result = await _mediator.Send(new GetAdminTaskByIdQuery { AdminTaskId = taskId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AdminTaskDto>> CreateAdminTask([FromBody] CreateAdminTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"/api/admintasks/{result.AdminTaskId}", result);
    }

    [HttpPut("{taskId:guid}")]
    public async Task<ActionResult<AdminTaskDto>> UpdateAdminTask(Guid taskId, [FromBody] UpdateAdminTaskCommand command)
    {
        if (taskId != command.AdminTaskId) return BadRequest("Task ID mismatch");
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteAdminTask(Guid taskId)
    {
        var result = await _mediator.Send(new DeleteAdminTaskCommand { AdminTaskId = taskId });
        if (!result) return NotFound();
        return NoContent();
    }
}
