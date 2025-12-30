using PersonalLibraryLessonsLearned.Api.Features.LessonReminder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalLibraryLessonsLearned.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonRemindersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LessonRemindersController> _logger;

    public LessonRemindersController(IMediator mediator, ILogger<LessonRemindersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LessonReminderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LessonReminderDto>>> GetLessonReminders(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? lessonId,
        [FromQuery] bool? isSent,
        [FromQuery] bool? isDismissed)
    {
        _logger.LogInformation("Getting reminders for user {UserId}", userId);

        var result = await _mediator.Send(new GetLessonRemindersQuery
        {
            UserId = userId,
            LessonId = lessonId,
            IsSent = isSent,
            IsDismissed = isDismissed,
        });

        return Ok(result);
    }

    [HttpGet("{lessonReminderId:guid}")]
    [ProducesResponseType(typeof(LessonReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonReminderDto>> GetLessonReminderById(Guid lessonReminderId)
    {
        _logger.LogInformation("Getting reminder {LessonReminderId}", lessonReminderId);

        var result = await _mediator.Send(new GetLessonReminderByIdQuery { LessonReminderId = lessonReminderId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LessonReminderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LessonReminderDto>> CreateLessonReminder([FromBody] CreateLessonReminderCommand command)
    {
        _logger.LogInformation("Creating reminder for lesson {LessonId}", command.LessonId);

        var result = await _mediator.Send(command);

        return Created($"/api/lessonreminders/{result.LessonReminderId}", result);
    }

    [HttpPut("{lessonReminderId:guid}")]
    [ProducesResponseType(typeof(LessonReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonReminderDto>> UpdateLessonReminder(Guid lessonReminderId, [FromBody] UpdateLessonReminderCommand command)
    {
        if (lessonReminderId != command.LessonReminderId)
        {
            return BadRequest("Reminder ID mismatch");
        }

        _logger.LogInformation("Updating reminder {LessonReminderId}", lessonReminderId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{lessonReminderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLessonReminder(Guid lessonReminderId)
    {
        _logger.LogInformation("Deleting reminder {LessonReminderId}", lessonReminderId);

        var result = await _mediator.Send(new DeleteLessonReminderCommand { LessonReminderId = lessonReminderId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
