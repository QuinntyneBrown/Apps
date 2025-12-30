using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record UpdateSeasonCommand : IRequest<SeasonDto?>
{
    public Guid SeasonId { get; init; }
    public string SeasonName { get; init; } = string.Empty;
    public int Year { get; init; }
    public int Wins { get; init; }
    public int Losses { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand, SeasonDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<UpdateSeasonCommandHandler> _logger;

    public UpdateSeasonCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<UpdateSeasonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SeasonDto?> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating season {SeasonId}", request.SeasonId);

        var season = await _context.Seasons
            .FirstOrDefaultAsync(s => s.SeasonId == request.SeasonId, cancellationToken);

        if (season == null)
        {
            _logger.LogWarning("Season {SeasonId} not found", request.SeasonId);
            return null;
        }

        season.SeasonName = request.SeasonName;
        season.Year = request.Year;
        season.Wins = request.Wins;
        season.Losses = request.Losses;
        season.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated season {SeasonId}", request.SeasonId);

        return season.ToDto();
    }
}
