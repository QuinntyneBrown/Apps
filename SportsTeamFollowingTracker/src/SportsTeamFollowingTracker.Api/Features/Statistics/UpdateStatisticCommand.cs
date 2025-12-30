using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record UpdateStatisticCommand : IRequest<StatisticDto?>
{
    public Guid StatisticId { get; init; }
    public string StatName { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public DateTime RecordedDate { get; init; }
}

public class UpdateStatisticCommandHandler : IRequestHandler<UpdateStatisticCommand, StatisticDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<UpdateStatisticCommandHandler> _logger;

    public UpdateStatisticCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<UpdateStatisticCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StatisticDto?> Handle(UpdateStatisticCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating statistic {StatisticId}", request.StatisticId);

        var statistic = await _context.Statistics
            .FirstOrDefaultAsync(s => s.StatisticId == request.StatisticId, cancellationToken);

        if (statistic == null)
        {
            _logger.LogWarning("Statistic {StatisticId} not found", request.StatisticId);
            return null;
        }

        statistic.StatName = request.StatName;
        statistic.Value = request.Value;
        statistic.RecordedDate = request.RecordedDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated statistic {StatisticId}", request.StatisticId);

        return statistic.ToDto();
    }
}
