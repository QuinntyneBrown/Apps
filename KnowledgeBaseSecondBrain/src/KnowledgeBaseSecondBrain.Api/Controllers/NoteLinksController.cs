using KnowledgeBaseSecondBrain.Api.Features.NoteLinks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseSecondBrain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteLinksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NoteLinksController> _logger;

    public NoteLinksController(IMediator mediator, ILogger<NoteLinksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NoteLinkDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NoteLinkDto>>> GetNoteLinks(
        [FromQuery] Guid? sourceNoteId,
        [FromQuery] Guid? targetNoteId)
    {
        _logger.LogInformation("Getting note links");

        var result = await _mediator.Send(new GetNoteLinksQuery
        {
            SourceNoteId = sourceNoteId,
            TargetNoteId = targetNoteId,
        });

        return Ok(result);
    }

    [HttpGet("{noteLinkId:guid}")]
    [ProducesResponseType(typeof(NoteLinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteLinkDto>> GetNoteLinkById(Guid noteLinkId)
    {
        _logger.LogInformation("Getting note link {NoteLinkId}", noteLinkId);

        var result = await _mediator.Send(new GetNoteLinkByIdQuery { NoteLinkId = noteLinkId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NoteLinkDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NoteLinkDto>> CreateNoteLink([FromBody] CreateNoteLinkCommand command)
    {
        _logger.LogInformation("Creating note link");

        var result = await _mediator.Send(command);

        return Created($"/api/notelinks/{result.NoteLinkId}", result);
    }

    [HttpPut("{noteLinkId:guid}")]
    [ProducesResponseType(typeof(NoteLinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteLinkDto>> UpdateNoteLink(Guid noteLinkId, [FromBody] UpdateNoteLinkCommand command)
    {
        if (noteLinkId != command.NoteLinkId)
        {
            return BadRequest("Note link ID mismatch");
        }

        _logger.LogInformation("Updating note link {NoteLinkId}", noteLinkId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{noteLinkId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNoteLink(Guid noteLinkId)
    {
        _logger.LogInformation("Deleting note link {NoteLinkId}", noteLinkId);

        var result = await _mediator.Send(new DeleteNoteLinkCommand { NoteLinkId = noteLinkId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
