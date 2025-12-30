// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Api.Features.Dividend;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioTracker.Api.Controllers;

/// <summary>
/// Controller for managing dividend payments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DividendController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DividendController> _logger;

    public DividendController(IMediator mediator, ILogger<DividendController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all dividends.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DividendDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DividendDto>>> GetDividends(CancellationToken cancellationToken)
    {
        var dividends = await _mediator.Send(new GetDividendsQuery(), cancellationToken);
        return Ok(dividends);
    }

    /// <summary>
    /// Gets a dividend by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DividendDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DividendDto>> GetDividendById(Guid id, CancellationToken cancellationToken)
    {
        var dividend = await _mediator.Send(new GetDividendByIdQuery(id), cancellationToken);

        if (dividend == null)
        {
            return NotFound();
        }

        return Ok(dividend);
    }

    /// <summary>
    /// Creates a new dividend.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DividendDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DividendDto>> CreateDividend(
        [FromBody] CreateDividendCommand command,
        CancellationToken cancellationToken)
    {
        var dividend = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDividendById), new { id = dividend.DividendId }, dividend);
    }

    /// <summary>
    /// Updates an existing dividend.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DividendDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DividendDto>> UpdateDividend(
        Guid id,
        [FromBody] UpdateDividendCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.DividendId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var dividend = await _mediator.Send(command, cancellationToken);
            return Ok(dividend);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a dividend.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDividend(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteDividendCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
