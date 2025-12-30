using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record DeleteStatisticCommand : IRequest<bool>
{
    public Guid StatisticId { get; init; }
}

public class DeleteStatisticCommandHandler : IRequestHandler<DeleteStatisticCommand, bool>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<DeleteStatisticCommandHandler> _logger;

    public DeleteStatisticCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<DeleteStatisticCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteStatisticCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting statistic {StatisticId}", request.StatisticId);

        var statistic = await _context.Statistics
            .FirstOrDefaultAsync(s => s.StatisticId == request.StatisticId, cancellationToken);

        if (statistic == null)
        {
            _logger.LogWarning("Statistic {StatisticId} not found", request.StatisticId);
            return false;
        }

        _context.Statistics.Remove(statistic);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted statistic {StatisticId}", request.StatisticId);

        return true;
    }
}
