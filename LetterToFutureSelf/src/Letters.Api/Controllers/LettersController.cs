using Letters.Api.Features.Letters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Letters.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LettersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LettersController> _logger;

    public LettersController(IMediator mediator, ILogger<LettersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LetterDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LetterDto>>> GetLetters([FromQuery] Guid? userId = null)
    {
        _logger.LogInformation("Getting letters");
        var result = await _mediator.Send(new GetLettersQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{letterId:guid}")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> GetLetterById(Guid letterId)
    {
        _logger.LogInformation("Getting letter {LetterId}", letterId);
        var result = await _mediator.Send(new GetLetterByIdQuery { LetterId = letterId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LetterDto>> CreateLetter([FromBody] CreateLetterCommand command)
    {
        _logger.LogInformation("Creating letter: {Subject}", command.Subject);
        var result = await _mediator.Send(command);
        return Created($"/api/letters/{result.LetterId}", result);
    }

    [HttpPut("{letterId:guid}")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> UpdateLetter(Guid letterId, [FromBody] UpdateLetterCommand command)
    {
        if (letterId != command.LetterId) return BadRequest("Letter ID mismatch");
        _logger.LogInformation("Updating letter {LetterId}", letterId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{letterId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLetter(Guid letterId)
    {
        _logger.LogInformation("Deleting letter {LetterId}", letterId);
        var result = await _mediator.Send(new DeleteLetterCommand { LetterId = letterId });
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("{letterId:guid}/deliver")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> MarkAsDelivered(Guid letterId)
    {
        _logger.LogInformation("Marking letter {LetterId} as delivered", letterId);
        var result = await _mediator.Send(new MarkLetterDeliveredCommand { LetterId = letterId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("{letterId:guid}/read")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> MarkAsRead(Guid letterId)
    {
        _logger.LogInformation("Marking letter {LetterId} as read", letterId);
        var result = await _mediator.Send(new MarkLetterReadCommand { LetterId = letterId });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
