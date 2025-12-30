// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeeklyReviewSystem.Api.Features.WeeklyReviews;

namespace WeeklyReviewSystem.Api.Controllers;

/// <summary>
/// Controller for managing weekly reviews.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WeeklyReviewsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WeeklyReviewsController> _logger;

    public WeeklyReviewsController(IMediator mediator, ILogger<WeeklyReviewsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all weekly reviews.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<WeeklyReviewDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all weekly reviews");
        var reviews = await _mediator.Send(new GetAllWeeklyReviewsQuery(), cancellationToken);
        return Ok(reviews);
    }

    /// <summary>
    /// Gets a weekly review by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WeeklyReviewDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting weekly review {WeeklyReviewId}", id);
        var review = await _mediator.Send(new GetWeeklyReviewByIdQuery { WeeklyReviewId = id }, cancellationToken);

        if (review == null)
        {
            _logger.LogWarning("Weekly review {WeeklyReviewId} not found", id);
            return NotFound();
        }

        return Ok(review);
    }

    /// <summary>
    /// Creates a new weekly review.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WeeklyReviewDto>> Create(CreateWeeklyReviewCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new weekly review for user {UserId}", command.UserId);
        var review = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = review.WeeklyReviewId }, review);
    }

    /// <summary>
    /// Updates an existing weekly review.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<WeeklyReviewDto>> Update(Guid id, UpdateWeeklyReviewCommand command, CancellationToken cancellationToken)
    {
        if (id != command.WeeklyReviewId)
        {
            _logger.LogWarning("ID mismatch: URL {UrlId} != Command {CommandId}", id, command.WeeklyReviewId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating weekly review {WeeklyReviewId}", id);
        var review = await _mediator.Send(command, cancellationToken);

        if (review == null)
        {
            _logger.LogWarning("Weekly review {WeeklyReviewId} not found", id);
            return NotFound();
        }

        return Ok(review);
    }

    /// <summary>
    /// Deletes a weekly review.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting weekly review {WeeklyReviewId}", id);
        var result = await _mediator.Send(new DeleteWeeklyReviewCommand { WeeklyReviewId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Weekly review {WeeklyReviewId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
