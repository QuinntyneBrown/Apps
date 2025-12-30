using NeighborhoodSocialNetwork.Api.Features.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NeighborhoodSocialNetwork.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMediator mediator, ILogger<MessagesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MessageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages(
        [FromQuery] Guid? senderNeighborId,
        [FromQuery] Guid? recipientNeighborId,
        [FromQuery] bool? isRead)
    {
        _logger.LogInformation("Getting messages");

        var result = await _mediator.Send(new GetMessagesQuery
        {
            SenderNeighborId = senderNeighborId,
            RecipientNeighborId = recipientNeighborId,
            IsRead = isRead,
        });

        return Ok(result);
    }

    [HttpGet("{messageId:guid}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MessageDto>> GetMessageById(Guid messageId)
    {
        _logger.LogInformation("Getting message {MessageId}", messageId);

        var result = await _mediator.Send(new GetMessageByIdQuery { MessageId = messageId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageCommand command)
    {
        _logger.LogInformation("Creating message");

        var result = await _mediator.Send(command);

        return Created($"/api/messages/{result.MessageId}", result);
    }

    [HttpPut("{messageId:guid}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MessageDto>> UpdateMessage(Guid messageId, [FromBody] UpdateMessageCommand command)
    {
        if (messageId != command.MessageId)
        {
            return BadRequest("Message ID mismatch");
        }

        _logger.LogInformation("Updating message {MessageId}", messageId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{messageId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMessage(Guid messageId)
    {
        _logger.LogInformation("Deleting message {MessageId}", messageId);

        var result = await _mediator.Send(new DeleteMessageCommand { MessageId = messageId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
