// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Features.PhotoLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassicCarRestorationLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PhotoLogsController> _logger;

    public PhotoLogsController(IMediator mediator, ILogger<PhotoLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotoLogDto>>> GetPhotoLogs([FromQuery] Guid? projectId, [FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting photo logs for project {ProjectId}, user {UserId}", projectId, userId);
        var result = await _mediator.Send(new GetPhotoLogs.Query { ProjectId = projectId, UserId = userId });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoLogDto>> GetPhotoLog(Guid id)
    {
        _logger.LogInformation("Getting photo log {PhotoLogId}", id);
        var result = await _mediator.Send(new GetPhotoLogById.Query { PhotoLogId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PhotoLogDto>> CreatePhotoLog(CreatePhotoLog.Command command)
    {
        _logger.LogInformation("Creating photo log for project {ProjectId}", command.ProjectId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPhotoLog), new { id = result.PhotoLogId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PhotoLogDto>> UpdatePhotoLog(Guid id, UpdatePhotoLog.Command command)
    {
        if (id != command.PhotoLogId)
        {
            return BadRequest("Photo log ID mismatch");
        }

        _logger.LogInformation("Updating photo log {PhotoLogId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhotoLog(Guid id)
    {
        _logger.LogInformation("Deleting photo log {PhotoLogId}", id);
        var result = await _mediator.Send(new DeletePhotoLog.Command { PhotoLogId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
