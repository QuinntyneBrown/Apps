using DateManagement.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DateManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImportantDatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ImportantDatesController> _logger;

    public ImportantDatesController(IMediator mediator, ILogger<ImportantDatesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ImportantDateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ImportantDateDto>>> GetImportantDates(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all important dates");
        var result = await _mediator.Send(new GetImportantDatesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImportantDateDto>> GetImportantDateById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting important date {ImportantDateId}", id);
        var result = await _mediator.Send(new GetImportantDateByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ImportantDateDto>> CreateImportantDate(
        [FromBody] CreateImportantDateCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating important date for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetImportantDateById), new { id = result.ImportantDateId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ImportantDateDto>> UpdateImportantDate(
        Guid id,
        [FromBody] UpdateImportantDateCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ImportantDateId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating important date {ImportantDateId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImportantDate(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting important date {ImportantDateId}", id);
        var result = await _mediator.Send(new DeleteImportantDateCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
