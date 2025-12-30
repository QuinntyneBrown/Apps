// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Api.Features.EfficiencyReports;
using FuelEconomyTracker.Api.Features.EfficiencyReports.Commands;
using FuelEconomyTracker.Api.Features.EfficiencyReports.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelEconomyTracker.Api.Controllers;

/// <summary>
/// Controller for managing efficiency reports.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EfficiencyReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EfficiencyReportsController> _logger;

    public EfficiencyReportsController(IMediator mediator, ILogger<EfficiencyReportsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all efficiency reports.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EfficiencyReportDto>>> GetEfficiencyReports([FromQuery] Guid? vehicleId = null)
    {
        var query = new GetEfficiencyReports { VehicleId = vehicleId };
        var reports = await _mediator.Send(query);
        return Ok(reports);
    }

    /// <summary>
    /// Gets an efficiency report by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EfficiencyReportDto>> GetEfficiencyReport(Guid id)
    {
        var query = new GetEfficiencyReportById { EfficiencyReportId = id };
        var report = await _mediator.Send(query);

        if (report == null)
        {
            return NotFound();
        }

        return Ok(report);
    }

    /// <summary>
    /// Generates a new efficiency report.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EfficiencyReportDto>> GenerateReport([FromBody] GenerateEfficiencyReportRequest request)
    {
        var command = new GenerateEfficiencyReport
        {
            VehicleId = request.VehicleId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Notes = request.Notes
        };

        var report = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEfficiencyReport), new { id = report.EfficiencyReportId }, report);
    }

    /// <summary>
    /// Deletes an efficiency report.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEfficiencyReport(Guid id)
    {
        try
        {
            var command = new DeleteEfficiencyReport { EfficiencyReportId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
