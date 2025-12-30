using WineCellarInventory.Api.Features.TastingNotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WineCellarInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TastingNotesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TastingNotesController> _logger;

    public TastingNotesController(IMediator mediator, ILogger<TastingNotesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TastingNoteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TastingNoteDto>>> GetTastingNotes(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? wineId,
        [FromQuery] int? minRating)
    {
        _logger.LogInformation("Getting tasting notes for user {UserId}", userId);

        var result = await _mediator.Send(new GetTastingNotesQuery
        {
            UserId = userId,
            WineId = wineId,
            MinRating = minRating,
        });

        return Ok(result);
    }

    [HttpGet("{tastingNoteId:guid}")]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TastingNoteDto>> GetTastingNoteById(Guid tastingNoteId)
    {
        _logger.LogInformation("Getting tasting note {TastingNoteId}", tastingNoteId);

        var result = await _mediator.Send(new GetTastingNoteByIdQuery { TastingNoteId = tastingNoteId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TastingNoteDto>> CreateTastingNote([FromBody] CreateTastingNoteCommand command)
    {
        _logger.LogInformation("Creating tasting note for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/tastingnotes/{result.TastingNoteId}", result);
    }

    [HttpPut("{tastingNoteId:guid}")]
    [ProducesResponseType(typeof(TastingNoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TastingNoteDto>> UpdateTastingNote(Guid tastingNoteId, [FromBody] UpdateTastingNoteCommand command)
    {
        if (tastingNoteId != command.TastingNoteId)
        {
            return BadRequest("Tasting note ID mismatch");
        }

        _logger.LogInformation("Updating tasting note {TastingNoteId}", tastingNoteId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{tastingNoteId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTastingNote(Guid tastingNoteId)
    {
        _logger.LogInformation("Deleting tasting note {TastingNoteId}", tastingNoteId);

        var result = await _mediator.Send(new DeleteTastingNoteCommand { TastingNoteId = tastingNoteId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
