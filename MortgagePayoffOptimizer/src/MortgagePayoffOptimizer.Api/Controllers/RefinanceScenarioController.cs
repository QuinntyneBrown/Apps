// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

namespace MortgagePayoffOptimizer.Api.Controllers;

/// <summary>
/// Controller for managing refinance scenarios.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RefinanceScenarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public RefinanceScenarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all refinance scenarios.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<RefinanceScenarioDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetRefinanceScenariosQuery());
        return Ok(result);
    }

    /// <summary>
    /// Gets a refinance scenario by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RefinanceScenarioDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRefinanceScenarioByIdQuery { RefinanceScenarioId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new refinance scenario.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RefinanceScenarioDto>> Create([FromBody] CreateRefinanceScenarioCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.RefinanceScenarioId }, result);
    }

    /// <summary>
    /// Updates an existing refinance scenario.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<RefinanceScenarioDto>> Update(Guid id, [FromBody] UpdateRefinanceScenarioCommand command)
    {
        if (id != command.RefinanceScenarioId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a refinance scenario.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteRefinanceScenarioCommand { RefinanceScenarioId = id });
        return NoContent();
    }
}
