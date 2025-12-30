using MarriageEnrichmentJournal.Api.Features.Gratitudes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarriageEnrichmentJournal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GratitudesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GratitudesController> _logger;

    public GratitudesController(IMediator mediator, ILogger<GratitudesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GratitudeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GratitudeDto>>> GetGratitudes(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? journalEntryId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting gratitudes for user {UserId}", userId);

        var result = await _mediator.Send(new GetGratitudesQuery
        {
            UserId = userId,
            JournalEntryId = journalEntryId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{gratitudeId:guid}")]
    [ProducesResponseType(typeof(GratitudeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GratitudeDto>> GetGratitudeById(Guid gratitudeId)
    {
        _logger.LogInformation("Getting gratitude {GratitudeId}", gratitudeId);

        var result = await _mediator.Send(new GetGratitudeByIdQuery { GratitudeId = gratitudeId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GratitudeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GratitudeDto>> CreateGratitude([FromBody] CreateGratitudeCommand command)
    {
        _logger.LogInformation("Creating gratitude for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/gratitudes/{result.GratitudeId}", result);
    }

    [HttpPut("{gratitudeId:guid}")]
    [ProducesResponseType(typeof(GratitudeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GratitudeDto>> UpdateGratitude(Guid gratitudeId, [FromBody] UpdateGratitudeCommand command)
    {
        if (gratitudeId != command.GratitudeId)
        {
            return BadRequest("Gratitude ID mismatch");
        }

        _logger.LogInformation("Updating gratitude {GratitudeId}", gratitudeId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{gratitudeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGratitude(Guid gratitudeId)
    {
        _logger.LogInformation("Deleting gratitude {GratitudeId}", gratitudeId);

        var result = await _mediator.Send(new DeleteGratitudeCommand { GratitudeId = gratitudeId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
