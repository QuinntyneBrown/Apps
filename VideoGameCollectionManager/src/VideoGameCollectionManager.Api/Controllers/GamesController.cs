// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameCollectionManager.Api.Features.Games;

namespace VideoGameCollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GamesController> _logger;

    public GamesController(IMediator mediator, ILogger<GamesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all games");
        var games = await _mediator.Send(new GetAllGamesQuery(), cancellationToken);
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting game with id {GameId}", id);
        var game = await _mediator.Send(new GetGameByIdQuery { GameId = id }, cancellationToken);

        if (game == null)
        {
            _logger.LogWarning("Game with id {GameId} not found", id);
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> Create([FromBody] CreateGameCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new game: {Title}", command.Title);
        var game = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = game.GameId }, game);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GameDto>> Update(Guid id, [FromBody] UpdateGameCommand command, CancellationToken cancellationToken)
    {
        if (id != command.GameId)
        {
            _logger.LogWarning("Game id mismatch: {UrlId} != {CommandId}", id, command.GameId);
            return BadRequest("Game ID mismatch");
        }

        _logger.LogInformation("Updating game with id {GameId}", id);
        var game = await _mediator.Send(command, cancellationToken);

        if (game == null)
        {
            _logger.LogWarning("Game with id {GameId} not found", id);
            return NotFound();
        }

        return Ok(game);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting game with id {GameId}", id);
        var result = await _mediator.Send(new DeleteGameCommand { GameId = id }, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Game with id {GameId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
