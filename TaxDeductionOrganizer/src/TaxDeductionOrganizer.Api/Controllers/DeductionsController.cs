// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxDeductionOrganizer.Api.Features.Deductions;

namespace TaxDeductionOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeductionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeductionsController> _logger;

    public DeductionsController(IMediator mediator, ILogger<DeductionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all deductions, optionally filtered by tax year.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<DeductionDto>>> GetAll([FromQuery] Guid? taxYearId = null)
    {
        _logger.LogInformation("Getting all deductions for tax year: {TaxYearId}", taxYearId);
        var result = await _mediator.Send(new GetAllDeductions.Query { TaxYearId = taxYearId });
        return Ok(result);
    }

    /// <summary>
    /// Gets a deduction by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DeductionDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting deduction with ID: {DeductionId}", id);
        try
        {
            var result = await _mediator.Send(new GetDeductionById.Query { DeductionId = id });
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Deduction with ID {DeductionId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new deduction.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DeductionDto>> Create([FromBody] CreateDeduction.Command command)
    {
        _logger.LogInformation("Creating new deduction for tax year {TaxYearId}", command.TaxYearId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.DeductionId }, result);
    }

    /// <summary>
    /// Updates an existing deduction.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<DeductionDto>> Update(Guid id, [FromBody] UpdateDeduction.Command command)
    {
        if (id != command.DeductionId)
        {
            _logger.LogWarning("Deduction ID mismatch: route={RouteId}, body={BodyId}", id, command.DeductionId);
            return BadRequest(new { message = "Deduction ID in route does not match ID in request body" });
        }

        _logger.LogInformation("Updating deduction with ID: {DeductionId}", id);
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Deduction with ID {DeductionId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a deduction.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting deduction with ID: {DeductionId}", id);
        try
        {
            await _mediator.Send(new DeleteDeduction.Command { DeductionId = id });
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Deduction with ID {DeductionId} not found", id);
            return NotFound(new { message = ex.Message });
        }
    }
}
