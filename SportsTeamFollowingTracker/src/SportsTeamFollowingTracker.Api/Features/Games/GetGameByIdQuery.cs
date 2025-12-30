using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record GetGameByIdQuery : IRequest<GameDto?>
{
    public Guid GameId { get; init; }
}

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetGameByIdQueryHandler> _logger;

    public GetGameByIdQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetGameByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GameDto?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting game {GameId}", request.GameId);

        var game = await _context.Games
            .Include(g => g.Team)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        if (game == null)
        {
            _logger.LogWarning("Game {GameId} not found", request.GameId);
            return null;
        }

        return game.ToDto();
    }
}
