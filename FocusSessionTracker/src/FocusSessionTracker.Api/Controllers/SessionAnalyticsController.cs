// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.SessionAnalytics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FocusSessionTracker.Api.Controllers;

/// <summary>
/// Controller for managing session analytics.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SessionAnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SessionAnalyticsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionAnalyticsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public SessionAnalyticsController(IMediator mediator, ILogger<SessionAnalyticsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets analytics with optional filters.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of analytics.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SessionAnalyticsDto>>> GetAnalytics(
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        var query = new GetAnalyticsQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Generates analytics for a user and period.
    /// </summary>
    /// <param name="command">The generate command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The generated analytics.</returns>
    [HttpPost("generate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionAnalyticsDto>> GenerateAnalytics(
        [FromBody] GenerateAnalyticsCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAnalytics), new { userId = result.UserId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating analytics");
            return BadRequest(ex.Message);
        }
    }
}
