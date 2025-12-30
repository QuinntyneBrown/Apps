using GiftIdeaTracker.Api.Features.GiftIdeas;
using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GiftIdeaTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftIdeasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GiftIdeasController> _logger;

    public GiftIdeasController(IMediator mediator, ILogger<GiftIdeasController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GiftIdeaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GiftIdeaDto>>> GetGiftIdeas(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? recipientId,
        [FromQuery] Occasion? occasion,
        [FromQuery] bool? isPurchased)
    {
        _logger.LogInformation("Getting gift ideas for user {UserId}", userId);

        var result = await _mediator.Send(new GetGiftIdeasQuery
        {
            UserId = userId,
            RecipientId = recipientId,
            Occasion = occasion,
            IsPurchased = isPurchased,
        });

        return Ok(result);
    }

    [HttpGet("{giftIdeaId:guid}")]
    [ProducesResponseType(typeof(GiftIdeaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftIdeaDto>> GetGiftIdeaById(Guid giftIdeaId)
    {
        _logger.LogInformation("Getting gift idea {GiftIdeaId}", giftIdeaId);

        var result = await _mediator.Send(new GetGiftIdeaByIdQuery { GiftIdeaId = giftIdeaId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GiftIdeaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GiftIdeaDto>> CreateGiftIdea([FromBody] CreateGiftIdeaCommand command)
    {
        _logger.LogInformation("Creating gift idea for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/giftideas/{result.GiftIdeaId}", result);
    }

    [HttpPut("{giftIdeaId:guid}")]
    [ProducesResponseType(typeof(GiftIdeaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftIdeaDto>> UpdateGiftIdea(Guid giftIdeaId, [FromBody] UpdateGiftIdeaCommand command)
    {
        if (giftIdeaId != command.GiftIdeaId)
        {
            return BadRequest("Gift idea ID mismatch");
        }

        _logger.LogInformation("Updating gift idea {GiftIdeaId}", giftIdeaId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{giftIdeaId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGiftIdea(Guid giftIdeaId)
    {
        _logger.LogInformation("Deleting gift idea {GiftIdeaId}", giftIdeaId);

        var result = await _mediator.Send(new DeleteGiftIdeaCommand { GiftIdeaId = giftIdeaId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
