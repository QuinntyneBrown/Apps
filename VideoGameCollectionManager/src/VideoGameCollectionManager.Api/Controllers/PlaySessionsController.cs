// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameCollectionManager.Api.Features.PlaySessions;

namespace VideoGameCollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaySessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PlaySessionsController> _logger;

    public PlaySessionsController(IMediator mediator, ILogger<PlaySessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlaySessionDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all play sessions");
        var playSessions = await _mediator.Send(new GetAllPlaySessionsQuery(), cancellationToken);
        return Ok(playSessions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlaySessionDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting play session with id {PlaySessionId}", id);
        var playSession = await _mediator.Send(new GetPlaySessionByIdQuery { PlaySessionId = id }, cancellationToken);

        if (playSession == null)
        {
            _logger.LogWarning("Play session with id {PlaySessionId} not found", id);
            return NotFound();
        }

        return Ok(playSession);
    }

    [HttpPost]
    public async Task<ActionResult<PlaySessionDto>> Create([FromBody] CreatePlaySessionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new play session for game {GameId}", command.GameId);
        var playSession = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = playSession.PlaySessionId }, playSession);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlaySessionDto>> Update(Guid id, [FromBody] UpdatePlaySessionCommand command, CancellationToken cancellationToken)
    {
        if (id != command.PlaySessionId)
        {
            _logger.LogWarning("Play session id mismatch: {UrlId} != {CommandId}", id, command.PlaySessionId);
            return BadRequest("Play session ID mismatch");
        }

        _logger.LogInformation("Updating play session with id {PlaySessionId}", id);
        var playSession = await _mediator.Send(command, cancellationToken);

        if (playSession == null)
        {
            _logger.LogWarning("Play session with id {PlaySessionId} not found", id);
            return NotFound();
        }

        return Ok(playSession);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting play session with id {PlaySessionId}", id);
        var result = await _mediator.Send(new DeletePlaySessionCommand { PlaySessionId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Play session with id {PlaySessionId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
