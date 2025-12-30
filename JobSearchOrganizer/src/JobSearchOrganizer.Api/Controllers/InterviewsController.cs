using JobSearchOrganizer.Api.Features.Interviews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchOrganizer.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<InterviewDto>>> GetInterviews(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? applicationId,
        [FromQuery] string? interviewType,
        [FromQuery] bool? isCompleted)
    {
        _logger.LogInformation("Getting interviews for user {UserId}", userId);

        var result = await _mediator.Send(new GetInterviewsQuery
        {
            UserId = userId,
            ApplicationId = applicationId,
            InterviewType = interviewType,
            IsCompleted = isCompleted,
        });

        return Ok(result);
    }

    [HttpGet("{interviewId:guid}")]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InterviewDto>> GetInterviewById(Guid interviewId)
    {
        _logger.LogInformation("Getting interview {InterviewId}", interviewId);

        var result = await _mediator.Send(new GetInterviewByIdQuery { InterviewId = interviewId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InterviewDto>> CreateInterview([FromBody] CreateInterviewCommand command)
    {
        _logger.LogInformation("Creating interview for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/interviews/{result.InterviewId}", result);
    }

    [HttpPut("{interviewId:guid}")]
    [ProducesResponseType(typeof(InterviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InterviewDto>> UpdateInterview(Guid interviewId, [FromBody] UpdateInterviewCommand command)
    {
        if (interviewId != command.InterviewId)
        {
            return BadRequest("Interview ID mismatch");
        }

        _logger.LogInformation("Updating interview {InterviewId}", interviewId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{interviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInterview(Guid interviewId)
    {
        _logger.LogInformation("Deleting interview {InterviewId}", interviewId);

        var result = await _mediator.Send(new DeleteInterviewCommand { InterviewId = interviewId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
