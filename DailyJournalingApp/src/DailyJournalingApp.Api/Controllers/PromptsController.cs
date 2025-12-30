using DailyJournalingApp.Api.Features.Prompts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyJournalingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PromptsController> _logger;

    public PromptsController(IMediator mediator, ILogger<PromptsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PromptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PromptDto>>> GetPrompts(
        [FromQuery] string? category,
        [FromQuery] bool? systemPromptsOnly,
        [FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting prompts for category {Category}", category);

        var result = await _mediator.Send(new GetPromptsQuery
        {
            Category = category,
            SystemPromptsOnly = systemPromptsOnly,
            UserId = userId,
        });

        return Ok(result);
    }

    [HttpGet("{promptId:guid}")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PromptDto>> GetPromptById(Guid promptId)
    {
        _logger.LogInformation("Getting prompt {PromptId}", promptId);

        var result = await _mediator.Send(new GetPromptByIdQuery { PromptId = promptId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PromptDto>> CreatePrompt([FromBody] CreatePromptCommand command)
    {
        _logger.LogInformation("Creating prompt for category {Category}", command.Category);

        var result = await _mediator.Send(command);

        return Created($"/api/prompts/{result.PromptId}", result);
    }

    [HttpDelete("{promptId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePrompt(Guid promptId)
    {
        _logger.LogInformation("Deleting prompt {PromptId}", promptId);

        var result = await _mediator.Send(new DeletePromptCommand { PromptId = promptId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
