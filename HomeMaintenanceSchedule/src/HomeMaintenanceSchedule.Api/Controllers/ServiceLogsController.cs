using HomeMaintenanceSchedule.Api.Features.ServiceLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceSchedule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ServiceLogsController> _logger;

    public ServiceLogsController(IMediator mediator, ILogger<ServiceLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceLogDto>>> GetServiceLogs(
        [FromQuery] Guid? maintenanceTaskId,
        [FromQuery] Guid? contractorId)
    {
        _logger.LogInformation("Getting service logs");

        var result = await _mediator.Send(new GetServiceLogsQuery
        {
            MaintenanceTaskId = maintenanceTaskId,
            ContractorId = contractorId,
        });

        return Ok(result);
    }

    [HttpGet("{serviceLogId:guid}")]
    [ProducesResponseType(typeof(ServiceLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceLogDto>> GetServiceLogById(Guid serviceLogId)
    {
        _logger.LogInformation("Getting service log {ServiceLogId}", serviceLogId);

        var result = await _mediator.Send(new GetServiceLogByIdQuery { ServiceLogId = serviceLogId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ServiceLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceLogDto>> CreateServiceLog([FromBody] CreateServiceLogCommand command)
    {
        _logger.LogInformation("Creating service log for maintenance task {MaintenanceTaskId}", command.MaintenanceTaskId);

        var result = await _mediator.Send(command);

        return Created($"/api/servicelogs/{result.ServiceLogId}", result);
    }

    [HttpPut("{serviceLogId:guid}")]
    [ProducesResponseType(typeof(ServiceLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceLogDto>> UpdateServiceLog(Guid serviceLogId, [FromBody] UpdateServiceLogCommand command)
    {
        if (serviceLogId != command.ServiceLogId)
        {
            return BadRequest("Service log ID mismatch");
        }

        _logger.LogInformation("Updating service log {ServiceLogId}", serviceLogId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{serviceLogId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServiceLog(Guid serviceLogId)
    {
        _logger.LogInformation("Deleting service log {ServiceLogId}", serviceLogId);

        var result = await _mediator.Send(new DeleteServiceLogCommand { ServiceLogId = serviceLogId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
