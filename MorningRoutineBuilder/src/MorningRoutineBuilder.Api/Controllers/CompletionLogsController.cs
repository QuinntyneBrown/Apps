// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningRoutineBuilder.Api.Features.CompletionLogs;
using MorningRoutineBuilder.Api.Features.CompletionLogs.Commands;
using MorningRoutineBuilder.Api.Features.CompletionLogs.Queries;

namespace MorningRoutineBuilder.Api.Controllers;

/// <summary>
/// Controller for managing completion logs.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CompletionLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CompletionLogsController> _logger;

    public CompletionLogsController(IMediator mediator, ILogger<CompletionLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all completion logs.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<CompletionLogDto>>> GetCompletionLogs(
        [FromQuery] Guid? routineId = null,
        [FromQuery] Guid? userId = null)
    {
        var query = new GetCompletionLogs { RoutineId = routineId, UserId = userId };
        var logs = await _mediator.Send(query);
        return Ok(logs);
    }

    /// <summary>
    /// Gets a completion log by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CompletionLogDto>> GetCompletionLog(Guid id)
    {
        var query = new GetCompletionLogById { CompletionLogId = id };
        var log = await _mediator.Send(query);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }

    /// <summary>
    /// Creates a new completion log.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CompletionLogDto>> CreateCompletionLog([FromBody] CreateCompletionLogRequest request)
    {
        var command = new CreateCompletionLog
        {
            RoutineId = request.RoutineId,
            UserId = request.UserId,
            CompletionDate = request.CompletionDate,
            ActualStartTime = request.ActualStartTime,
            ActualEndTime = request.ActualEndTime,
            TasksCompleted = request.TasksCompleted,
            TotalTasks = request.TotalTasks,
            Notes = request.Notes,
            MoodRating = request.MoodRating
        };

        var log = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCompletionLog), new { id = log.CompletionLogId }, log);
    }

    /// <summary>
    /// Updates an existing completion log.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CompletionLogDto>> UpdateCompletionLog(Guid id, [FromBody] UpdateCompletionLogRequest request)
    {
        try
        {
            var command = new UpdateCompletionLog
            {
                CompletionLogId = id,
                CompletionDate = request.CompletionDate,
                ActualStartTime = request.ActualStartTime,
                ActualEndTime = request.ActualEndTime,
                TasksCompleted = request.TasksCompleted,
                TotalTasks = request.TotalTasks,
                Notes = request.Notes,
                MoodRating = request.MoodRating
            };

            var log = await _mediator.Send(command);
            return Ok(log);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a completion log.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompletionLog(Guid id)
    {
        try
        {
            var command = new DeleteCompletionLog { CompletionLogId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
