using DailyJournalingApp.Api.Features.JournalEntries;
using DailyJournalingApp.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyJournalingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalEntriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<JournalEntriesController> _logger;

    public JournalEntriesController(IMediator mediator, ILogger<JournalEntriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JournalEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<JournalEntryDto>>> GetJournalEntries(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] Mood? mood,
        [FromQuery] bool? favoritesOnly)
    {
        _logger.LogInformation("Getting journal entries for user {UserId}", userId);

        var result = await _mediator.Send(new GetJournalEntriesQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            Mood = mood,
            FavoritesOnly = favoritesOnly,
        });

        return Ok(result);
    }

    [HttpGet("{journalEntryId:guid}")]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalEntryDto>> GetJournalEntryById(Guid journalEntryId)
    {
        _logger.LogInformation("Getting journal entry {JournalEntryId}", journalEntryId);

        var result = await _mediator.Send(new GetJournalEntryByIdQuery { JournalEntryId = journalEntryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JournalEntryDto>> CreateJournalEntry([FromBody] CreateJournalEntryCommand command)
    {
        _logger.LogInformation("Creating journal entry for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/journalentries/{result.JournalEntryId}", result);
    }

    [HttpPut("{journalEntryId:guid}")]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalEntryDto>> UpdateJournalEntry(Guid journalEntryId, [FromBody] UpdateJournalEntryCommand command)
    {
        if (journalEntryId != command.JournalEntryId)
        {
            return BadRequest("Journal entry ID mismatch");
        }

        _logger.LogInformation("Updating journal entry {JournalEntryId}", journalEntryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{journalEntryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteJournalEntry(Guid journalEntryId)
    {
        _logger.LogInformation("Deleting journal entry {JournalEntryId}", journalEntryId);

        var result = await _mediator.Send(new DeleteJournalEntryCommand { JournalEntryId = journalEntryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{journalEntryId:guid}/favorite")]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalEntryDto>> ToggleFavorite(Guid journalEntryId)
    {
        _logger.LogInformation("Toggling favorite for journal entry {JournalEntryId}", journalEntryId);

        var result = await _mediator.Send(new ToggleFavoriteCommand { JournalEntryId = journalEntryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
