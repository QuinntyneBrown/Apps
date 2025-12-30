using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record DeleteSeasonCommand : IRequest<bool>
{
    public Guid SeasonId { get; init; }
}

public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, bool>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<DeleteSeasonCommandHandler> _logger;

    public DeleteSeasonCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<DeleteSeasonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting season {SeasonId}", request.SeasonId);

        var season = await _context.Seasons
            .FirstOrDefaultAsync(s => s.SeasonId == request.SeasonId, cancellationToken);

        if (season == null)
        {
            _logger.LogWarning("Season {SeasonId} not found", request.SeasonId);
            return false;
        }

        _context.Seasons.Remove(season);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted season {SeasonId}", request.SeasonId);

        return true;
    }
}
