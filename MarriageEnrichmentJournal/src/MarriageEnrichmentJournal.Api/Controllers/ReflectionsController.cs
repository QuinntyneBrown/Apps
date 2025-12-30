using MarriageEnrichmentJournal.Api.Features.Reflections;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarriageEnrichmentJournal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReflectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReflectionsController> _logger;

    public ReflectionsController(IMediator mediator, ILogger<ReflectionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReflectionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReflectionDto>>> GetReflections(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? journalEntryId,
        [FromQuery] string? topic,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting reflections for user {UserId}", userId);

        var result = await _mediator.Send(new GetReflectionsQuery
        {
            UserId = userId,
            JournalEntryId = journalEntryId,
            Topic = topic,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{reflectionId:guid}")]
    [ProducesResponseType(typeof(ReflectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReflectionDto>> GetReflectionById(Guid reflectionId)
    {
        _logger.LogInformation("Getting reflection {ReflectionId}", reflectionId);

        var result = await _mediator.Send(new GetReflectionByIdQuery { ReflectionId = reflectionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReflectionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReflectionDto>> CreateReflection([FromBody] CreateReflectionCommand command)
    {
        _logger.LogInformation("Creating reflection for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/reflections/{result.ReflectionId}", result);
    }

    [HttpPut("{reflectionId:guid}")]
    [ProducesResponseType(typeof(ReflectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReflectionDto>> UpdateReflection(Guid reflectionId, [FromBody] UpdateReflectionCommand command)
    {
        if (reflectionId != command.ReflectionId)
        {
            return BadRequest("Reflection ID mismatch");
        }

        _logger.LogInformation("Updating reflection {ReflectionId}", reflectionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{reflectionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReflection(Guid reflectionId)
    {
        _logger.LogInformation("Deleting reflection {ReflectionId}", reflectionId);

        var result = await _mediator.Send(new DeleteReflectionCommand { ReflectionId = reflectionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
