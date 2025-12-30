// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningRoutineBuilder.Api.Features.Streaks;
using MorningRoutineBuilder.Api.Features.Streaks.Commands;
using MorningRoutineBuilder.Api.Features.Streaks.Queries;

namespace MorningRoutineBuilder.Api.Controllers;

/// <summary>
/// Controller for managing streaks.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StreaksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StreaksController> _logger;

    public StreaksController(IMediator mediator, ILogger<StreaksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all streaks.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<StreakDto>>> GetStreaks(
        [FromQuery] Guid? routineId = null,
        [FromQuery] Guid? userId = null)
    {
        var query = new GetStreaks { RoutineId = routineId, UserId = userId };
        var streaks = await _mediator.Send(query);
        return Ok(streaks);
    }

    /// <summary>
    /// Gets a streak by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<StreakDto>> GetStreak(Guid id)
    {
        var query = new GetStreakById { StreakId = id };
        var streak = await _mediator.Send(query);

        if (streak == null)
        {
            return NotFound();
        }

        return Ok(streak);
    }

    /// <summary>
    /// Creates a new streak.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<StreakDto>> CreateStreak([FromBody] CreateStreakRequest request)
    {
        var command = new CreateStreak
        {
            RoutineId = request.RoutineId,
            UserId = request.UserId
        };

        var streak = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStreak), new { id = streak.StreakId }, streak);
    }

    /// <summary>
    /// Updates an existing streak.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<StreakDto>> UpdateStreak(Guid id, [FromBody] UpdateStreakRequest request)
    {
        try
        {
            var command = new UpdateStreak
            {
                StreakId = id,
                CurrentStreak = request.CurrentStreak,
                LongestStreak = request.LongestStreak,
                LastCompletionDate = request.LastCompletionDate,
                StreakStartDate = request.StreakStartDate,
                IsActive = request.IsActive
            };

            var streak = await _mediator.Send(command);
            return Ok(streak);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a streak.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStreak(Guid id)
    {
        try
        {
            var command = new DeleteStreak { StreakId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
