using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record CreateSeasonCommand : IRequest<SeasonDto>
{
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string SeasonName { get; init; } = string.Empty;
    public int Year { get; init; }
    public int Wins { get; init; }
    public int Losses { get; init; }
    public string? Notes { get; init; }
}

public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, SeasonDto>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<CreateSeasonCommandHandler> _logger;

    public CreateSeasonCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<CreateSeasonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SeasonDto> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating season for team {TeamId}: {SeasonName} {Year}",
            request.TeamId,
            request.SeasonName,
            request.Year);

        var season = new Season
        {
            SeasonId = Guid.NewGuid(),
            UserId = request.UserId,
            TeamId = request.TeamId,
            SeasonName = request.SeasonName,
            Year = request.Year,
            Wins = request.Wins,
            Losses = request.Losses,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Seasons.Add(season);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created season {SeasonId} for team {TeamId}",
            season.SeasonId,
            request.TeamId);

        return season.ToDto();
    }
}
