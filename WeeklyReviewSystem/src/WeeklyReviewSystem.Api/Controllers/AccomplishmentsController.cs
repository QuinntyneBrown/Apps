// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeeklyReviewSystem.Api.Features.Accomplishments;

namespace WeeklyReviewSystem.Api.Controllers;

/// <summary>
/// Controller for managing accomplishments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccomplishmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccomplishmentsController> _logger;

    public AccomplishmentsController(IMediator mediator, ILogger<AccomplishmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all accomplishments.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<AccomplishmentDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all accomplishments");
        var accomplishments = await _mediator.Send(new GetAllAccomplishmentsQuery(), cancellationToken);
        return Ok(accomplishments);
    }

    /// <summary>
    /// Gets an accomplishment by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<AccomplishmentDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting accomplishment {AccomplishmentId}", id);
        var accomplishment = await _mediator.Send(new GetAccomplishmentByIdQuery { AccomplishmentId = id }, cancellationToken);

        if (accomplishment == null)
        {
            _logger.LogWarning("Accomplishment {AccomplishmentId} not found", id);
            return NotFound();
        }

        return Ok(accomplishment);
    }

    /// <summary>
    /// Creates a new accomplishment.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AccomplishmentDto>> Create(CreateAccomplishmentCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new accomplishment for review {WeeklyReviewId}", command.WeeklyReviewId);
        var accomplishment = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = accomplishment.AccomplishmentId }, accomplishment);
    }

    /// <summary>
    /// Updates an existing accomplishment.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<AccomplishmentDto>> Update(Guid id, UpdateAccomplishmentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.AccomplishmentId)
        {
            _logger.LogWarning("ID mismatch: URL {UrlId} != Command {CommandId}", id, command.AccomplishmentId);
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating accomplishment {AccomplishmentId}", id);
        var accomplishment = await _mediator.Send(command, cancellationToken);

        if (accomplishment == null)
        {
            _logger.LogWarning("Accomplishment {AccomplishmentId} not found", id);
            return NotFound();
        }

        return Ok(accomplishment);
    }

    /// <summary>
    /// Deletes an accomplishment.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting accomplishment {AccomplishmentId}", id);
        var result = await _mediator.Send(new DeleteAccomplishmentCommand { AccomplishmentId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Accomplishment {AccomplishmentId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
