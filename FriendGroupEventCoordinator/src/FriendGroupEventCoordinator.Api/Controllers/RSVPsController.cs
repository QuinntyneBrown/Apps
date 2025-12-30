// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Features.RSVPs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FriendGroupEventCoordinator.Api.Controllers;

/// <summary>
/// Controller for managing RSVPs.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RSVPsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RSVPsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RSVPsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RSVPsController(IMediator mediator, ILogger<RSVPsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets an RSVP by ID.
    /// </summary>
    /// <param name="id">The RSVP ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The RSVP.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RSVPDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RSVPDto>> GetRSVP(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting RSVP with ID: {RSVPId}", id);
        var rsvpDto = await _mediator.Send(new GetRSVPQuery(id), cancellationToken);

        if (rsvpDto == null)
        {
            _logger.LogWarning("RSVP with ID {RSVPId} not found", id);
            return NotFound();
        }

        return Ok(rsvpDto);
    }

    /// <summary>
    /// Gets all RSVPs for a specific event.
    /// </summary>
    /// <param name="eventId">The event ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of RSVPs for the event.</returns>
    [HttpGet("event/{eventId}")]
    [ProducesResponseType(typeof(List<RSVPDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RSVPDto>>> GetRSVPsByEvent(Guid eventId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting RSVPs for event with ID: {EventId}", eventId);
        var rsvps = await _mediator.Send(new GetRSVPsByEventQuery(eventId), cancellationToken);
        return Ok(rsvps);
    }

    /// <summary>
    /// Creates a new RSVP.
    /// </summary>
    /// <param name="createRSVPDto">The RSVP to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created RSVP.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RSVPDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RSVPDto>> CreateRSVP(CreateRSVPDto createRSVPDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new RSVP for event: {EventId}", createRSVPDto.EventId);
        var rsvpDto = await _mediator.Send(new CreateRSVPCommand(createRSVPDto), cancellationToken);
        return CreatedAtAction(nameof(GetRSVP), new { id = rsvpDto.RSVPId }, rsvpDto);
    }

    /// <summary>
    /// Updates an existing RSVP.
    /// </summary>
    /// <param name="id">The RSVP ID.</param>
    /// <param name="updateRSVPDto">The updated RSVP data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated RSVP.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RSVPDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RSVPDto>> UpdateRSVP(Guid id, UpdateRSVPDto updateRSVPDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating RSVP with ID: {RSVPId}", id);
        var rsvpDto = await _mediator.Send(new UpdateRSVPCommand(id, updateRSVPDto), cancellationToken);

        if (rsvpDto == null)
        {
            _logger.LogWarning("RSVP with ID {RSVPId} not found", id);
            return NotFound();
        }

        return Ok(rsvpDto);
    }

    /// <summary>
    /// Deletes an RSVP.
    /// </summary>
    /// <param name="id">The RSVP ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRSVP(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting RSVP with ID: {RSVPId}", id);
        var result = await _mediator.Send(new DeleteRSVPCommand(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("RSVP with ID {RSVPId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
