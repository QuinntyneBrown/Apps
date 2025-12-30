using StressMoodTracker.Api.Features.Journals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StressMoodTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<JournalsController> _logger;

    public JournalsController(IMediator mediator, ILogger<JournalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JournalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<JournalDto>>> GetJournals(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? tag)
    {
        _logger.LogInformation("Getting journal entries for user {UserId}", userId);

        var result = await _mediator.Send(new GetJournalsQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            Tag = tag,
        });

        return Ok(result);
    }

    [HttpGet("{journalId:guid}")]
    [ProducesResponseType(typeof(JournalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalDto>> GetJournalById(Guid journalId)
    {
        _logger.LogInformation("Getting journal entry {JournalId}", journalId);

        var result = await _mediator.Send(new GetJournalByIdQuery { JournalId = journalId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(JournalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JournalDto>> CreateJournal([FromBody] CreateJournalCommand command)
    {
        _logger.LogInformation("Creating journal entry for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/journals/{result.JournalId}", result);
    }

    [HttpPut("{journalId:guid}")]
    [ProducesResponseType(typeof(JournalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalDto>> UpdateJournal(Guid journalId, [FromBody] UpdateJournalCommand command)
    {
        if (journalId != command.JournalId)
        {
            return BadRequest("Journal ID mismatch");
        }

        _logger.LogInformation("Updating journal entry {JournalId}", journalId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{journalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteJournal(Guid journalId)
    {
        _logger.LogInformation("Deleting journal entry {JournalId}", journalId);

        var result = await _mediator.Send(new DeleteJournalCommand { JournalId = journalId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
