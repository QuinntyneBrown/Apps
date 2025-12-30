using StressMoodTracker.Api.Features.MoodEntries;
using StressMoodTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StressMoodTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoodEntriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoodEntriesController> _logger;

    public MoodEntriesController(IMediator mediator, ILogger<MoodEntriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MoodEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MoodEntryDto>>> GetMoodEntries(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] MoodLevel? moodLevel,
        [FromQuery] StressLevel? stressLevel)
    {
        _logger.LogInformation("Getting mood entries for user {UserId}", userId);

        var result = await _mediator.Send(new GetMoodEntriesQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            MoodLevel = moodLevel,
            StressLevel = stressLevel,
        });

        return Ok(result);
    }

    [HttpGet("{moodEntryId:guid}")]
    [ProducesResponseType(typeof(MoodEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MoodEntryDto>> GetMoodEntryById(Guid moodEntryId)
    {
        _logger.LogInformation("Getting mood entry {MoodEntryId}", moodEntryId);

        var result = await _mediator.Send(new GetMoodEntryByIdQuery { MoodEntryId = moodEntryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MoodEntryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MoodEntryDto>> CreateMoodEntry([FromBody] CreateMoodEntryCommand command)
    {
        _logger.LogInformation("Creating mood entry for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/moodentries/{result.MoodEntryId}", result);
    }

    [HttpPut("{moodEntryId:guid}")]
    [ProducesResponseType(typeof(MoodEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MoodEntryDto>> UpdateMoodEntry(Guid moodEntryId, [FromBody] UpdateMoodEntryCommand command)
    {
        if (moodEntryId != command.MoodEntryId)
        {
            return BadRequest("Mood entry ID mismatch");
        }

        _logger.LogInformation("Updating mood entry {MoodEntryId}", moodEntryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{moodEntryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMoodEntry(Guid moodEntryId)
    {
        _logger.LogInformation("Deleting mood entry {MoodEntryId}", moodEntryId);

        var result = await _mediator.Send(new DeleteMoodEntryCommand { MoodEntryId = moodEntryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
