using ServiceRecords.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ServiceRecords.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRecordsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ServiceRecordsController> _logger;

    public ServiceRecordsController(IMediator mediator, ILogger<ServiceRecordsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceRecordDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRecordDto>>> GetServiceRecords(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all service records");
        var result = await _mediator.Send(new GetServiceRecordsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceRecordDto>> GetServiceRecordById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting service record {ServiceRecordId}", id);
        var result = await _mediator.Send(new GetServiceRecordByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ServiceRecordDto>> CreateServiceRecord(
        [FromBody] CreateServiceRecordCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating service record for appliance {ApplianceId}", command.ApplianceId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetServiceRecordById), new { id = result.ServiceRecordId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceRecordDto>> UpdateServiceRecord(
        Guid id,
        [FromBody] UpdateServiceRecordCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ServiceRecordId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating service record {ServiceRecordId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServiceRecord(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting service record {ServiceRecordId}", id);
        var result = await _mediator.Send(new DeleteServiceRecordCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
