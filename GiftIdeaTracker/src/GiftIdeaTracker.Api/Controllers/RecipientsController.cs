using GiftIdeaTracker.Api.Features.Recipients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GiftIdeaTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipientsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecipientsController> _logger;

    public RecipientsController(IMediator mediator, ILogger<RecipientsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecipientDto>>> GetRecipients(
        [FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting recipients for user {UserId}", userId);

        var result = await _mediator.Send(new GetRecipientsQuery
        {
            UserId = userId,
        });

        return Ok(result);
    }

    [HttpGet("{recipientId:guid}")]
    [ProducesResponseType(typeof(RecipientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipientDto>> GetRecipientById(Guid recipientId)
    {
        _logger.LogInformation("Getting recipient {RecipientId}", recipientId);

        var result = await _mediator.Send(new GetRecipientByIdQuery { RecipientId = recipientId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RecipientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipientDto>> CreateRecipient([FromBody] CreateRecipientCommand command)
    {
        _logger.LogInformation("Creating recipient for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/recipients/{result.RecipientId}", result);
    }

    [HttpPut("{recipientId:guid}")]
    [ProducesResponseType(typeof(RecipientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipientDto>> UpdateRecipient(Guid recipientId, [FromBody] UpdateRecipientCommand command)
    {
        if (recipientId != command.RecipientId)
        {
            return BadRequest("Recipient ID mismatch");
        }

        _logger.LogInformation("Updating recipient {RecipientId}", recipientId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{recipientId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipient(Guid recipientId)
    {
        _logger.LogInformation("Deleting recipient {RecipientId}", recipientId);

        var result = await _mediator.Send(new DeleteRecipientCommand { RecipientId = recipientId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
