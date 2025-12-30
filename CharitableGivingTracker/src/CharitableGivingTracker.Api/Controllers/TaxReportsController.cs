// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Api.Features.TaxReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharitableGivingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaxReportsController> _logger;

    public TaxReportsController(IMediator mediator, ILogger<TaxReportsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all tax reports.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TaxReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TaxReportDto>>> GetAll()
    {
        _logger.LogInformation("Getting all tax reports");
        var result = await _mediator.Send(new GetAllTaxReports.Query());
        return Ok(result);
    }

    /// <summary>
    /// Gets a tax report by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaxReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxReportDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting tax report {TaxReportId}", id);
        var result = await _mediator.Send(new GetTaxReportById.Query(id));

        if (result == null)
        {
            _logger.LogWarning("Tax report {TaxReportId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets a tax report by year.
    /// </summary>
    [HttpGet("year/{year}")]
    [ProducesResponseType(typeof(TaxReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxReportDto>> GetByYear(int year)
    {
        _logger.LogInformation("Getting tax report for year {TaxYear}", year);
        var result = await _mediator.Send(new GetTaxReportByYear.Query(year));

        if (result == null)
        {
            _logger.LogWarning("Tax report for year {TaxYear} not found", year);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new tax report.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TaxReportDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaxReportDto>> Create([FromBody] CreateTaxReport.Command command)
    {
        _logger.LogInformation("Creating new tax report for year {TaxYear}", command.TaxYear);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.TaxReportId }, result);
    }

    /// <summary>
    /// Updates an existing tax report.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TaxReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxReportDto>> Update(Guid id, [FromBody] UpdateTaxReport.Command command)
    {
        if (id != command.TaxReportId)
        {
            _logger.LogWarning("Tax report ID mismatch: {RouteId} vs {CommandId}", id, command.TaxReportId);
            return BadRequest("Tax report ID mismatch");
        }

        _logger.LogInformation("Updating tax report {TaxReportId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Tax report {TaxReportId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a tax report.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting tax report {TaxReportId}", id);
        var result = await _mediator.Send(new DeleteTaxReport.Command(id));

        if (!result)
        {
            _logger.LogWarning("Tax report {TaxReportId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
