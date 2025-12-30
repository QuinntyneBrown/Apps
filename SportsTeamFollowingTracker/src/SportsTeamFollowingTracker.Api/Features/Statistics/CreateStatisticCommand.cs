using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record CreateStatisticCommand : IRequest<StatisticDto>
{
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string StatName { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public DateTime RecordedDate { get; init; }
}

public class CreateStatisticCommandHandler : IRequestHandler<CreateStatisticCommand, StatisticDto>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<CreateStatisticCommandHandler> _logger;

    public CreateStatisticCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<CreateStatisticCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StatisticDto> Handle(CreateStatisticCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating statistic for team {TeamId}: {StatName}",
            request.TeamId,
            request.StatName);

        var statistic = new Statistic
        {
            StatisticId = Guid.NewGuid(),
            UserId = request.UserId,
            TeamId = request.TeamId,
            StatName = request.StatName,
            Value = request.Value,
            RecordedDate = request.RecordedDate,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Statistics.Add(statistic);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created statistic {StatisticId} for team {TeamId}",
            statistic.StatisticId,
            request.TeamId);

        return statistic.ToDto();
    }
}
