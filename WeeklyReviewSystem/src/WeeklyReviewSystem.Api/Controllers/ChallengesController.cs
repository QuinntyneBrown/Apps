// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeeklyReviewSystem.Api.Features.Challenges;

namespace WeeklyReviewSystem.Api.Controllers;

/// <summary>
/// Controller for managing challenges.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ChallengesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ChallengesController> _logger;

    public ChallengesController(IMediator mediator, ILogger<ChallengesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all challenges.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ChallengeDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all challenges");
        var challenges = await _mediator.Send(new GetAllChallengesQuery(), cancellationToken);
        return Ok(challenges);
    }

    /// <summary>
    /// Gets a challenge by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ChallengeDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting challenge {ChallengeId}", id);
        var challenge = await _mediator.Send(new GetChallengeByIdQuery { ChallengeId = id }, cancellationToken);

        if (challenge == null)
        {
            _logger.LogWarning("Challenge {ChallengeId} not found", id);
            return NotFound();
        }

        return Ok(challenge);
    }

    /// <summary>
    /// Creates a new challenge.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ChallengeDto>> Create(CreateChallengeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new challenge for review {WeeklyReviewId}", command.WeeklyReviewId);
        var challenge = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = challenge.ChallengeId }, challenge);
    }

    /// <summary>
    /// Updates an existing challenge.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ChallengeDto>> Update(Guid id, UpdateChallengeCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ChallengeId)
        {
            _logger.LogWarning("ID mismatch: URL {UrlId} != Command {CommandId}", id, command.ChallengeId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating challenge {ChallengeId}", id);
        var challenge = await _mediator.Send(command, cancellationToken);

        if (challenge == null)
        {
            _logger.LogWarning("Challenge {ChallengeId} not found", id);
            return NotFound();
        }

        return Ok(challenge);
    }

    /// <summary>
    /// Deletes a challenge.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting challenge {ChallengeId}", id);
        var result = await _mediator.Send(new DeleteChallengeCommand { ChallengeId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Challenge {ChallengeId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
