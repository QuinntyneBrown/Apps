using ContactManagementApp.Api.Features.Interactions;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteractionsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InteractionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InteractionDto>>> GetInteractions(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? contactId)
    {
        return Ok(new List<InteractionDto>());
    }

    [HttpGet("{interactionId:guid}")]
    [ProducesResponseType(typeof(InteractionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InteractionDto>> GetInteractionById(Guid interactionId)
    {
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(InteractionDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<InteractionDto>> CreateInteraction([FromBody] CreateInteractionRequest request)
    {
        return Created(string.Empty, new InteractionDto());
    }

    [HttpDelete("{interactionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInteraction(Guid interactionId)
    {
        return NotFound();
    }
}

public record CreateInteractionRequest
{
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string InteractionType { get; init; } = string.Empty;
    public DateTime InteractionDate { get; init; }
    public string? Subject { get; init; }
    public string? Notes { get; init; }
    public string? Outcome { get; init; }
    public int? DurationMinutes { get; init; }
}
