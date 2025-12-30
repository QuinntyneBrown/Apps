using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record DeleteGameCommand : IRequest<bool>
{
    public Guid GameId { get; init; }
}

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<DeleteGameCommandHandler> _logger;

    public DeleteGameCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<DeleteGameCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting game {GameId}", request.GameId);

        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.GameId == request.GameId, cancellationToken);

        if (game == null)
        {
            _logger.LogWarning("Game {GameId} not found", request.GameId);
            return false;
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted game {GameId}", request.GameId);

        return true;
    }
}
