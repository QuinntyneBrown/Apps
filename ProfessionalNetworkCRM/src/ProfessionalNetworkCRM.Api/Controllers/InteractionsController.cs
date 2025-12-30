using ProfessionalNetworkCRM.Api.Features.Interactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteractionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InteractionsController> _logger;

    public InteractionsController(IMediator mediator, ILogger<InteractionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InteractionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InteractionDto>>> GetInteractions(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? contactId,
        [FromQuery] string? interactionType)
    {
        _logger.LogInformation("Getting interactions for user {UserId}, contact {ContactId}", userId, contactId);

        var result = await _mediator.Send(new GetInteractionsQuery
        {
            UserId = userId,
            ContactId = contactId,
            InteractionType = interactionType,
        });

        return Ok(result);
    }

    [HttpGet("{interactionId:guid}")]
    [ProducesResponseType(typeof(InteractionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InteractionDto>> GetInteractionById(Guid interactionId)
    {
        _logger.LogInformation("Getting interaction {InteractionId}", interactionId);

        var result = await _mediator.Send(new GetInteractionByIdQuery { InteractionId = interactionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InteractionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InteractionDto>> CreateInteraction([FromBody] CreateInteractionCommand command)
    {
        _logger.LogInformation("Creating interaction for contact {ContactId}", command.ContactId);

        var result = await _mediator.Send(command);

        return Created($"/api/interactions/{result.InteractionId}", result);
    }

    [HttpPut("{interactionId:guid}")]
    [ProducesResponseType(typeof(InteractionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InteractionDto>> UpdateInteraction(Guid interactionId, [FromBody] UpdateInteractionCommand command)
    {
        if (interactionId != command.InteractionId)
        {
            return BadRequest("Interaction ID mismatch");
        }

        _logger.LogInformation("Updating interaction {InteractionId}", interactionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{interactionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInteraction(Guid interactionId)
    {
        _logger.LogInformation("Deleting interaction {InteractionId}", interactionId);

        var result = await _mediator.Send(new DeleteInteractionCommand { InteractionId = interactionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
