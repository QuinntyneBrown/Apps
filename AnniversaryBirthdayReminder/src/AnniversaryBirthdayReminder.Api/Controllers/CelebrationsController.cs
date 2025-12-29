// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// API controller for managing celebrations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CelebrationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CelebrationsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CelebrationsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public CelebrationsController(IMediator mediator, ILogger<CelebrationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all celebrations for an important date.
    /// </summary>
    /// <param name="dateId">The important date ID.</param>
    /// <returns>The list of celebrations.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CelebrationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CelebrationDto>>> GetByDateId([FromQuery] Guid dateId)
    {
        _logger.LogInformation("Getting celebrations for important date {ImportantDateId}", dateId);

        var result = await _mediator.Send(new GetCelebrationsByDateIdQuery { ImportantDateId = dateId });

        return Ok(result);
    }

    /// <summary>
    /// Marks a celebration as completed.
    /// </summary>
    /// <param name="command">The mark completed command.</param>
    /// <returns>The created celebration.</returns>
    [HttpPost("completed")]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CelebrationDto>> MarkCompleted([FromBody] MarkCelebrationCompletedCommand command)
    {
        _logger.LogInformation(
            "Marking celebration as completed for important date {ImportantDateId}",
            command.ImportantDateId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Marks a celebration as skipped.
    /// </summary>
    /// <param name="command">The mark skipped command.</param>
    /// <returns>The created celebration.</returns>
    [HttpPost("skipped")]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CelebrationDto>> MarkSkipped([FromBody] MarkCelebrationSkippedCommand command)
    {
        _logger.LogInformation(
            "Marking celebration as skipped for important date {ImportantDateId}",
            command.ImportantDateId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Adds photos to a celebration.
    /// </summary>
    /// <param name="id">The celebration ID.</param>
    /// <param name="command">The add photos command.</param>
    /// <returns>The updated celebration.</returns>
    [HttpPost("{id:guid}/photos")]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CelebrationDto>> AddPhotos(Guid id, [FromBody] AddCelebrationPhotosCommand command)
    {
        if (id != command.CelebrationId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation(
            "Adding photos to celebration {CelebrationId}",
            id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
