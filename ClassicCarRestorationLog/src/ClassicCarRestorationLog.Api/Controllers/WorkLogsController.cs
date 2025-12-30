// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Features.WorkLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassicCarRestorationLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WorkLogsController> _logger;

    public WorkLogsController(IMediator mediator, ILogger<WorkLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkLogDto>>> GetWorkLogs([FromQuery] Guid? projectId, [FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting work logs for project {ProjectId}, user {UserId}", projectId, userId);
        var result = await _mediator.Send(new GetWorkLogs.Query { ProjectId = projectId, UserId = userId });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkLogDto>> GetWorkLog(Guid id)
    {
        _logger.LogInformation("Getting work log {WorkLogId}", id);
        var result = await _mediator.Send(new GetWorkLogById.Query { WorkLogId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<WorkLogDto>> CreateWorkLog(CreateWorkLog.Command command)
    {
        _logger.LogInformation("Creating work log for project {ProjectId}", command.ProjectId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWorkLog), new { id = result.WorkLogId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkLogDto>> UpdateWorkLog(Guid id, UpdateWorkLog.Command command)
    {
        if (id != command.WorkLogId)
        {
            return BadRequest("Work log ID mismatch");
        }

        _logger.LogInformation("Updating work log {WorkLogId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteWorkLog(Guid id)
    {
        _logger.LogInformation("Deleting work log {WorkLogId}", id);
        var result = await _mediator.Send(new DeleteWorkLog.Command { WorkLogId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
