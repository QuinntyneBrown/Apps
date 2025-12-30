using WoodworkingProjectManager.Api.Features.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WoodworkingProjectManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ToolsController> _logger;

    public ToolsController(IMediator mediator, ILogger<ToolsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ToolDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools([FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting tools for user {UserId}", userId);

        var result = await _mediator.Send(new GetToolsQuery { UserId = userId });

        return Ok(result);
    }

    [HttpGet("{toolId:guid}")]
    [ProducesResponseType(typeof(ToolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ToolDto>> GetToolById(Guid toolId)
    {
        _logger.LogInformation("Getting tool {ToolId}", toolId);

        var result = await _mediator.Send(new GetToolByIdQuery { ToolId = toolId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ToolDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ToolDto>> CreateTool([FromBody] CreateToolCommand command)
    {
        _logger.LogInformation("Creating tool for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/tools/{result.ToolId}", result);
    }

    [HttpPut("{toolId:guid}")]
    [ProducesResponseType(typeof(ToolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ToolDto>> UpdateTool(Guid toolId, [FromBody] UpdateToolCommand command)
    {
        if (toolId != command.ToolId)
        {
            return BadRequest("Tool ID mismatch");
        }

        _logger.LogInformation("Updating tool {ToolId}", toolId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{toolId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTool(Guid toolId)
    {
        _logger.LogInformation("Deleting tool {ToolId}", toolId);

        var result = await _mediator.Send(new DeleteToolCommand { ToolId = toolId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
