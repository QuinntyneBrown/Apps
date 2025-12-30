using PersonalHealthDashboard.Api.Features.WearableData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalHealthDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WearableDataController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WearableDataController> _logger;

    public WearableDataController(IMediator mediator, ILogger<WearableDataController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WearableDataDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WearableDataDto>>> GetWearableData(
        [FromQuery] Guid? userId,
        [FromQuery] string? deviceName,
        [FromQuery] string? dataType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting wearable data for user {UserId}", userId);

        var result = await _mediator.Send(new GetWearableDataQuery
        {
            UserId = userId,
            DeviceName = deviceName,
            DataType = dataType,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{wearableDataId:guid}")]
    [ProducesResponseType(typeof(WearableDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WearableDataDto>> GetWearableDataById(Guid wearableDataId)
    {
        _logger.LogInformation("Getting wearable data {WearableDataId}", wearableDataId);

        var result = await _mediator.Send(new GetWearableDataByIdQuery { WearableDataId = wearableDataId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WearableDataDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WearableDataDto>> CreateWearableData([FromBody] CreateWearableDataCommand command)
    {
        _logger.LogInformation("Creating wearable data for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/wearabledata/{result.WearableDataId}", result);
    }

    [HttpPut("{wearableDataId:guid}")]
    [ProducesResponseType(typeof(WearableDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WearableDataDto>> UpdateWearableData(Guid wearableDataId, [FromBody] UpdateWearableDataCommand command)
    {
        if (wearableDataId != command.WearableDataId)
        {
            return BadRequest("Wearable data ID mismatch");
        }

        _logger.LogInformation("Updating wearable data {WearableDataId}", wearableDataId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{wearableDataId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWearableData(Guid wearableDataId)
    {
        _logger.LogInformation("Deleting wearable data {WearableDataId}", wearableDataId);

        var result = await _mediator.Send(new DeleteWearableDataCommand { WearableDataId = wearableDataId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
