using VehicleMaintenanceLogger.Api.Features.ServiceRecords;
using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenanceLogger.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ServiceRecordDto>>> GetServiceRecords(
        [FromQuery] Guid? vehicleId,
        [FromQuery] ServiceType? serviceType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] decimal? minCost,
        [FromQuery] decimal? maxCost)
    {
        _logger.LogInformation("Getting service records for vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new GetServiceRecordsQuery
        {
            VehicleId = vehicleId,
            ServiceType = serviceType,
            StartDate = startDate,
            EndDate = endDate,
            MinCost = minCost,
            MaxCost = maxCost,
        });

        return Ok(result);
    }

    [HttpGet("{serviceRecordId:guid}")]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceRecordDto>> GetServiceRecordById(Guid serviceRecordId)
    {
        _logger.LogInformation("Getting service record {ServiceRecordId}", serviceRecordId);

        var result = await _mediator.Send(new GetServiceRecordByIdQuery { ServiceRecordId = serviceRecordId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceRecordDto>> CreateServiceRecord([FromBody] CreateServiceRecordCommand command)
    {
        _logger.LogInformation("Creating service record for vehicle {VehicleId}", command.VehicleId);

        var result = await _mediator.Send(command);

        return Created($"/api/servicerecords/{result.ServiceRecordId}", result);
    }

    [HttpPut("{serviceRecordId:guid}")]
    [ProducesResponseType(typeof(ServiceRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceRecordDto>> UpdateServiceRecord(Guid serviceRecordId, [FromBody] UpdateServiceRecordCommand command)
    {
        if (serviceRecordId != command.ServiceRecordId)
        {
            return BadRequest("Service record ID mismatch");
        }

        _logger.LogInformation("Updating service record {ServiceRecordId}", serviceRecordId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{serviceRecordId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServiceRecord(Guid serviceRecordId)
    {
        _logger.LogInformation("Deleting service record {ServiceRecordId}", serviceRecordId);

        var result = await _mediator.Send(new DeleteServiceRecordCommand { ServiceRecordId = serviceRecordId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
