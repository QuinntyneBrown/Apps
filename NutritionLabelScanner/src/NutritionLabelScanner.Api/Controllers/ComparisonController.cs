// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Api.Features.Comparison;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NutritionLabelScanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComparisonController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComparisonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ComparisonDto>>> GetComparisons(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetComparisonsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComparisonDto>> GetComparisonById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetComparisonByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<ComparisonDto>> CreateComparison(
        [FromBody] CreateComparisonCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetComparisonById), new { id = result.ComparisonId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ComparisonDto>> UpdateComparison(
        Guid id,
        [FromBody] UpdateComparisonCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ComparisonId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComparison(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteComparisonCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
