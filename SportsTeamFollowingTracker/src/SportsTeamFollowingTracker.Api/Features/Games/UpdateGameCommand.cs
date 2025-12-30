using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record UpdateGameCommand : IRequest<GameDto?>
{
    public Guid GameId { get; init; }
    public DateTime GameDate { get; init; }
    public string Opponent { get; init; } = string.Empty;
    public int? TeamScore { get; init; }
    public int? OpponentScore { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, GameDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<UpdateGameCommandHandler> _logger;

    public UpdateGameCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<UpdateGameCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GameDto?> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating game {GameId}", request.GameId);

        var game = await _context.Games
            .Include(g => g.Team)
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        if (game == null)
        {
            _logger.LogWarning("Game {GameId} not found", request.GameId);
            return null;
        }

        game.GameDate = request.GameDate;
        game.Opponent = request.Opponent;
        game.TeamScore = request.TeamScore;
        game.OpponentScore = request.OpponentScore;
        game.Notes = request.Notes;

        // Calculate IsWin
        if (request.TeamScore.HasValue && request.OpponentScore.HasValue)
        {
            game.IsWin = request.TeamScore.Value > request.OpponentScore.Value;
        }
        else
        {
            game.IsWin = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated game {GameId}", request.GameId);

        return game.ToDto();
    }
}
