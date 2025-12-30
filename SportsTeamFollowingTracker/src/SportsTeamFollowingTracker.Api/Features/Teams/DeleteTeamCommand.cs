using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record DeleteTeamCommand : IRequest<bool>
{
    public Guid TeamId { get; init; }
}

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<DeleteTeamCommandHandler> _logger;

    public DeleteTeamCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<DeleteTeamCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting team {TeamId}", request.TeamId);

        var team = await _context.Teams
            .FirstOrDefaultAsync(t => t.TeamId == request.TeamId, cancellationToken);

        if (team == null)
        {
            _logger.LogWarning("Team {TeamId} not found", request.TeamId);
            return false;
        }

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted team {TeamId}", request.TeamId);

        return true;
    }
}
