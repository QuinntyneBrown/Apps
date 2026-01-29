using Distractions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Distractions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistractionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DistractionsController> _logger;

    public DistractionsController(IMediator mediator, ILogger<DistractionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DistractionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DistractionDto>>> GetDistractions([FromQuery] Guid? sessionId = null)
    {
        _logger.LogInformation("Getting distractions");
        var result = await _mediator.Send(new GetDistractionsQuery { SessionId = sessionId });
        return Ok(result);
    }

    [HttpGet("{distractionId:guid}")]
    [ProducesResponseType(typeof(DistractionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistractionDto>> GetDistractionById(Guid distractionId)
    {
        _logger.LogInformation("Getting distraction {DistractionId}", distractionId);
        var result = await _mediator.Send(new GetDistractionByIdQuery { DistractionId = distractionId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DistractionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DistractionDto>> CreateDistraction([FromBody] CreateDistractionCommand command)
    {
        _logger.LogInformation("Creating distraction for session {SessionId}", command.SessionId);
        var result = await _mediator.Send(command);
        return Created($"/api/distractions/{result.DistractionId}", result);
    }

    [HttpPut("{distractionId:guid}")]
    [ProducesResponseType(typeof(DistractionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistractionDto>> UpdateDistraction(Guid distractionId, [FromBody] UpdateDistractionCommand command)
    {
        if (distractionId != command.DistractionId) return BadRequest("Distraction ID mismatch");
        _logger.LogInformation("Updating distraction {DistractionId}", distractionId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{distractionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDistraction(Guid distractionId)
    {
        _logger.LogInformation("Deleting distraction {DistractionId}", distractionId);
        var result = await _mediator.Send(new DeleteDistractionCommand { DistractionId = distractionId });
        if (!result) return NotFound();
        return NoContent();
    }
}
