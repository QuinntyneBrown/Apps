// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceWarrantyManualOrganizer.Api.Controllers;

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

    [HttpGet("appliance/{applianceId}")]
    public async Task<ActionResult<List<ServiceRecordDto>>> GetServiceRecordsByAppliance(Guid applianceId)
    {
        try
        {
            var query = new GetServiceRecordsByAppliance.Query { ApplianceId = applianceId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting service records for appliance {ApplianceId}", applianceId);
            return StatusCode(500, "An error occurred while getting service records");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceRecordDto>> GetServiceRecordById(Guid id)
    {
        try
        {
            var query = new GetServiceRecordById.Query { ServiceRecordId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting service record {ServiceRecordId}", id);
            return StatusCode(500, "An error occurred while getting the service record");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ServiceRecordDto>> CreateServiceRecord([FromBody] CreateServiceRecord.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetServiceRecordById), new { id = result.ServiceRecordId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating service record");
            return StatusCode(500, "An error occurred while creating the service record");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceRecordDto>> UpdateServiceRecord(Guid id, [FromBody] UpdateServiceRecord.Command command)
    {
        try
        {
            command.ServiceRecordId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating service record {ServiceRecordId}", id);
            return StatusCode(500, "An error occurred while updating the service record");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteServiceRecord(Guid id)
    {
        try
        {
            var command = new DeleteServiceRecord.Command { ServiceRecordId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting service record {ServiceRecordId}", id);
            return StatusCode(500, "An error occurred while deleting the service record");
        }
    }
}
