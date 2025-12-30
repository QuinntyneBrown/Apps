using HomeInventoryManager.Api.Features.ValueEstimates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValueEstimatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ValueEstimatesController> _logger;

    public ValueEstimatesController(IMediator mediator, ILogger<ValueEstimatesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ValueEstimateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ValueEstimateDto>>> GetValueEstimates(
        [FromQuery] Guid? itemId)
    {
        _logger.LogInformation("Getting value estimates for item {ItemId}", itemId);

        var result = await _mediator.Send(new GetValueEstimatesQuery
        {
            ItemId = itemId,
        });

        return Ok(result);
    }

    [HttpGet("{valueEstimateId:guid}")]
    [ProducesResponseType(typeof(ValueEstimateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueEstimateDto>> GetValueEstimateById(Guid valueEstimateId)
    {
        _logger.LogInformation("Getting value estimate {ValueEstimateId}", valueEstimateId);

        var result = await _mediator.Send(new GetValueEstimateByIdQuery { ValueEstimateId = valueEstimateId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ValueEstimateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ValueEstimateDto>> CreateValueEstimate([FromBody] CreateValueEstimateCommand command)
    {
        _logger.LogInformation("Creating value estimate for item {ItemId}", command.ItemId);

        var result = await _mediator.Send(command);

        return Created($"/api/valueestimates/{result.ValueEstimateId}", result);
    }

    [HttpPut("{valueEstimateId:guid}")]
    [ProducesResponseType(typeof(ValueEstimateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueEstimateDto>> UpdateValueEstimate(Guid valueEstimateId, [FromBody] UpdateValueEstimateCommand command)
    {
        if (valueEstimateId != command.ValueEstimateId)
        {
            return BadRequest("Value estimate ID mismatch");
        }

        _logger.LogInformation("Updating value estimate {ValueEstimateId}", valueEstimateId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{valueEstimateId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteValueEstimate(Guid valueEstimateId)
    {
        _logger.LogInformation("Deleting value estimate {ValueEstimateId}", valueEstimateId);

        var result = await _mediator.Send(new DeleteValueEstimateCommand { ValueEstimateId = valueEstimateId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
