using ContactManagementApp.Api.Features.FollowUps;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowUpsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FollowUpDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FollowUpDto>>> GetFollowUps(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? contactId,
        [FromQuery] bool? isCompleted)
    {
        return Ok(new List<FollowUpDto>());
    }

    [HttpGet("{followUpId:guid}")]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> GetFollowUpById(Guid followUpId)
    {
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<FollowUpDto>> CreateFollowUp([FromBody] CreateFollowUpRequest request)
    {
        return Created(string.Empty, new FollowUpDto());
    }

    [HttpPut("{followUpId:guid}/complete")]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> CompleteFollowUp(Guid followUpId)
    {
        return NotFound();
    }

    [HttpDelete("{followUpId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFollowUp(Guid followUpId)
    {
        return NotFound();
    }
}

public record CreateFollowUpRequest
{
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public string Priority { get; init; } = "Medium";
    public string? Notes { get; init; }
}
