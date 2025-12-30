using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record CreateTeamCommand : IRequest<TeamDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Sport Sport { get; init; }
    public string? League { get; init; }
    public string? City { get; init; }
    public bool IsFavorite { get; init; }
}

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, TeamDto>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<CreateTeamCommandHandler> _logger;

    public CreateTeamCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<CreateTeamCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating team for user {UserId}: {TeamName}",
            request.UserId,
            request.Name);

        var team = new Team
        {
            TeamId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Sport = request.Sport,
            League = request.League,
            City = request.City,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created team {TeamId} for user {UserId}",
            team.TeamId,
            request.UserId);

        return team.ToDto();
    }
}
