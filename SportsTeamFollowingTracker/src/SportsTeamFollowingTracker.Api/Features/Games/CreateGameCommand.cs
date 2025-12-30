using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record CreateGameCommand : IRequest<GameDto>
{
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public DateTime GameDate { get; init; }
    public string Opponent { get; init; } = string.Empty;
    public int? TeamScore { get; init; }
    public int? OpponentScore { get; init; }
    public string? Notes { get; init; }
}

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameDto>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<CreateGameCommandHandler> _logger;

    public CreateGameCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<CreateGameCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating game for team {TeamId} vs {Opponent}",
            request.TeamId,
            request.Opponent);

        bool? isWin = null;
        if (request.TeamScore.HasValue && request.OpponentScore.HasValue)
        {
            isWin = request.TeamScore.Value > request.OpponentScore.Value;
        }

        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = request.UserId,
            TeamId = request.TeamId,
            GameDate = request.GameDate,
            Opponent = request.Opponent,
            TeamScore = request.TeamScore,
            OpponentScore = request.OpponentScore,
            IsWin = isWin,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created game {GameId} for team {TeamId}",
            game.GameId,
            request.TeamId);

        return game.ToDto();
    }
}
