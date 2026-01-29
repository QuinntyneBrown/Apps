using FocusSessions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FocusSessions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FocusSessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FocusSessionsController> _logger;

    public FocusSessionsController(IMediator mediator, ILogger<FocusSessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FocusSessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FocusSessionDto>>> GetFocusSessions()
    {
        _logger.LogInformation("Getting focus sessions");
        var result = await _mediator.Send(new GetFocusSessionsQuery());
        return Ok(result);
    }

    [HttpGet("{sessionId:guid}")]
    [ProducesResponseType(typeof(FocusSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FocusSessionDto>> GetFocusSessionById(Guid sessionId)
    {
        _logger.LogInformation("Getting focus session {SessionId}", sessionId);
        var result = await _mediator.Send(new GetFocusSessionByIdQuery { SessionId = sessionId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FocusSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FocusSessionDto>> CreateFocusSession([FromBody] CreateFocusSessionCommand command)
    {
        _logger.LogInformation("Creating focus session");
        var result = await _mediator.Send(command);
        return Created($"/api/focussessions/{result.SessionId}", result);
    }

    [HttpPost("{sessionId:guid}/complete")]
    [ProducesResponseType(typeof(FocusSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FocusSessionDto>> CompleteFocusSession(Guid sessionId)
    {
        _logger.LogInformation("Completing focus session {SessionId}", sessionId);
        var result = await _mediator.Send(new CompleteFocusSessionCommand { SessionId = sessionId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{sessionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFocusSession(Guid sessionId)
    {
        _logger.LogInformation("Deleting focus session {SessionId}", sessionId);
        var result = await _mediator.Send(new DeleteFocusSessionCommand { SessionId = sessionId });
        if (!result) return NotFound();
        return NoContent();
    }
}
