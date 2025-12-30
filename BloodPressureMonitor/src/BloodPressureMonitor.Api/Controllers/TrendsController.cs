// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodPressureMonitor.Api;

/// <summary>
/// API controller for managing blood pressure trends.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrendsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TrendsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrendsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public TrendsController(IMediator mediator, ILogger<TrendsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a trend by ID.
    /// </summary>
    /// <param name="id">The trend ID.</param>
    /// <returns>The trend.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TrendDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrendDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting trend {TrendId}", id);

        var result = await _mediator.Send(new GetTrendByIdQuery { TrendId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all trends for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of trends.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrendDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TrendDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting trends for user {UserId}", userId);

        var result = await _mediator.Send(new GetTrendsByUserIdQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Calculates and creates a new trend for a user.
    /// </summary>
    /// <param name="command">The calculate command.</param>
    /// <returns>The calculated trend.</returns>
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(TrendDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrendDto>> Calculate([FromBody] CalculateTrendCommand command)
    {
        _logger.LogInformation(
            "Calculating trend for user {UserId}",
            command.UserId);

        try
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.TrendId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to calculate trend");
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a trend.
    /// </summary>
    /// <param name="id">The trend ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting trend {TrendId}", id);

        var result = await _mediator.Send(new DeleteTrendCommand { TrendId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
