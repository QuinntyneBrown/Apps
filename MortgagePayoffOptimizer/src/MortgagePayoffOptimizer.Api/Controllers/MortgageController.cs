// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MortgagePayoffOptimizer.Api.Features.Mortgage;

namespace MortgagePayoffOptimizer.Api.Controllers;

/// <summary>
/// Controller for managing mortgages.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MortgageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MortgageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all mortgages.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<MortgageDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetMortgagesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Gets a mortgage by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<MortgageDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMortgageByIdQuery { MortgageId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new mortgage.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<MortgageDto>> Create([FromBody] CreateMortgageCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.MortgageId }, result);
    }

    /// <summary>
    /// Updates an existing mortgage.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<MortgageDto>> Update(Guid id, [FromBody] UpdateMortgageCommand command)
    {
        if (id != command.MortgageId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a mortgage.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteMortgageCommand { MortgageId = id });
        return NoContent();
    }
}
