using Interviews.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interviews.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InterviewsController> _logger;

    public InterviewsController(IMediator mediator, ILogger<InterviewsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InterviewDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InterviewDto>>> GetInterviews(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all interviews");
        var result = await _mediator.Send(new GetInterviewsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InterviewDto>> GetInterviewById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting interview {InterviewId}", id);
        var result = await _mediator.Send(new GetInterviewByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<InterviewDto>> CreateInterview(
        [FromBody] CreateInterviewCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating interview for application {ApplicationId}", command.ApplicationId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetInterviewById), new { id = result.InterviewId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InterviewDto>> UpdateInterview(
        Guid id,
        [FromBody] UpdateInterviewCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.InterviewId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating interview {InterviewId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInterview(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting interview {InterviewId}", id);
        var result = await _mediator.Send(new DeleteInterviewCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
