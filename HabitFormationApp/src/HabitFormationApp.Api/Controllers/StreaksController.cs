using HabitFormationApp.Api.Features.Streaks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HabitFormationApp.Api.Controllers;

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

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StreakDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StreakDto>>> GetStreaks()
    {
        _logger.LogInformation("Getting all streaks");

        var result = await _mediator.Send(new GetStreaksQuery());

        return Ok(result);
    }

    [HttpGet("habit/{habitId:guid}")]
    [ProducesResponseType(typeof(StreakDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StreakDto>> GetStreakByHabitId(Guid habitId)
    {
        _logger.LogInformation("Getting streak for habit {HabitId}", habitId);

        var result = await _mediator.Send(new GetStreakByHabitIdQuery { HabitId = habitId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("habit/{habitId:guid}/increment")]
    [ProducesResponseType(typeof(StreakDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StreakDto>> IncrementStreak(Guid habitId)
    {
        _logger.LogInformation("Incrementing streak for habit {HabitId}", habitId);

        var result = await _mediator.Send(new IncrementStreakCommand { HabitId = habitId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
