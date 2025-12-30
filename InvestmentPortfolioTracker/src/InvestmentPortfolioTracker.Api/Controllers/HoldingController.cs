// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Api.Features.Holding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioTracker.Api.Controllers;

/// <summary>
/// Controller for managing portfolio holdings.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HoldingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HoldingController> _logger;

    public HoldingController(IMediator mediator, ILogger<HoldingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all holdings.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<HoldingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<HoldingDto>>> GetHoldings(CancellationToken cancellationToken)
    {
        var holdings = await _mediator.Send(new GetHoldingsQuery(), cancellationToken);
        return Ok(holdings);
    }

    /// <summary>
    /// Gets a holding by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(HoldingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HoldingDto>> GetHoldingById(Guid id, CancellationToken cancellationToken)
    {
        var holding = await _mediator.Send(new GetHoldingByIdQuery(id), cancellationToken);

        if (holding == null)
        {
            return NotFound();
        }

        return Ok(holding);
    }

    /// <summary>
    /// Creates a new holding.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(HoldingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HoldingDto>> CreateHolding(
        [FromBody] CreateHoldingCommand command,
        CancellationToken cancellationToken)
    {
        var holding = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetHoldingById), new { id = holding.HoldingId }, holding);
    }

    /// <summary>
    /// Updates an existing holding.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(HoldingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HoldingDto>> UpdateHolding(
        Guid id,
        [FromBody] UpdateHoldingCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.HoldingId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var holding = await _mediator.Send(command, cancellationToken);
            return Ok(holding);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a holding.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHolding(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteHoldingCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
