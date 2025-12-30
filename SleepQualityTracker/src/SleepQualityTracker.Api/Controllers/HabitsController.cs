using SleepQualityTracker.Api.Features.Habits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SleepQualityTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HabitsController> _logger;

    public HabitsController(IMediator mediator, ILogger<HabitsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HabitDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetHabits(
        [FromQuery] Guid? userId,
        [FromQuery] string? habitType,
        [FromQuery] bool? isPositive,
        [FromQuery] bool? isActive,
        [FromQuery] bool? isHighImpact)
    {
        _logger.LogInformation("Getting habits for user {UserId}", userId);

        var result = await _mediator.Send(new GetHabitsQuery
        {
            UserId = userId,
            HabitType = habitType,
            IsPositive = isPositive,
            IsActive = isActive,
            IsHighImpact = isHighImpact,
        });

        return Ok(result);
    }

    [HttpGet("{habitId:guid}")]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitDto>> GetHabitById(Guid habitId)
    {
        _logger.LogInformation("Getting habit {HabitId}", habitId);

        var result = await _mediator.Send(new GetHabitByIdQuery { HabitId = habitId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HabitDto>> CreateHabit([FromBody] CreateHabitCommand command)
    {
        _logger.LogInformation("Creating habit for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/habits/{result.HabitId}", result);
    }

    [HttpPut("{habitId:guid}")]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitDto>> UpdateHabit(Guid habitId, [FromBody] UpdateHabitCommand command)
    {
        if (habitId != command.HabitId)
        {
            return BadRequest("Habit ID mismatch");
        }

        _logger.LogInformation("Updating habit {HabitId}", habitId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{habitId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHabit(Guid habitId)
    {
        _logger.LogInformation("Deleting habit {HabitId}", habitId);

        var result = await _mediator.Send(new DeleteHabitCommand { HabitId = habitId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
