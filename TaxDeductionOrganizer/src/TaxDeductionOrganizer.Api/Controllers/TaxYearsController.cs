// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxDeductionOrganizer.Api.Features.TaxYears;

namespace TaxDeductionOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxYearsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaxYearsController> _logger;

    public TaxYearsController(IMediator mediator, ILogger<TaxYearsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all tax years.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TaxYearDto>>> GetAll()
    {
        _logger.LogInformation("Getting all tax years");
        var result = await _mediator.Send(new GetAllTaxYears.Query());
        return Ok(result);
    }

    /// <summary>
    /// Gets a tax year by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaxYearDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting tax year with ID: {TaxYearId}", id);
        try
        {
            var result = await _mediator.Send(new GetTaxYearById.Query { TaxYearId = id });
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Tax year with ID {TaxYearId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new tax year.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaxYearDto>> Create([FromBody] CreateTaxYear.Command command)
    {
        _logger.LogInformation("Creating new tax year for year {Year}", command.Year);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.TaxYearId }, result);
    }

    /// <summary>
    /// Updates an existing tax year.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaxYearDto>> Update(Guid id, [FromBody] UpdateTaxYear.Command command)
    {
        if (id != command.TaxYearId)
        {
            _logger.LogWarning("Tax year ID mismatch: route={RouteId}, body={BodyId}", id, command.TaxYearId);
            return BadRequest(new { message = "Tax year ID in route does not match ID in request body" });
        }

        _logger.LogInformation("Updating tax year with ID: {TaxYearId}", id);
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Tax year with ID {TaxYearId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a tax year.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting tax year with ID: {TaxYearId}", id);
        try
        {
            await _mediator.Send(new DeleteTaxYear.Command { TaxYearId = id });
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Tax year with ID {TaxYearId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }
}
