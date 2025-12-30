using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record CreateHealthTrendCommand : IRequest<HealthTrendDto>
{
    public Guid UserId { get; init; }
    public string MetricName { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double AverageValue { get; init; }
    public double MinValue { get; init; }
    public double MaxValue { get; init; }
    public string TrendDirection { get; init; } = string.Empty;
    public double PercentageChange { get; init; }
    public string? Insights { get; init; }
}

public class CreateHealthTrendCommandHandler : IRequestHandler<CreateHealthTrendCommand, HealthTrendDto>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<CreateHealthTrendCommandHandler> _logger;

    public CreateHealthTrendCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<CreateHealthTrendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HealthTrendDto> Handle(CreateHealthTrendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating health trend for user {UserId}, metric: {MetricName}",
            request.UserId,
            request.MetricName);

        var healthTrend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = request.UserId,
            MetricName = request.MetricName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AverageValue = request.AverageValue,
            MinValue = request.MinValue,
            MaxValue = request.MaxValue,
            TrendDirection = request.TrendDirection,
            PercentageChange = request.PercentageChange,
            Insights = request.Insights,
            CreatedAt = DateTime.UtcNow,
        };

        _context.HealthTrends.Add(healthTrend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created health trend {HealthTrendId} for user {UserId}",
            healthTrend.HealthTrendId,
            request.UserId);

        return healthTrend.ToDto();
    }
}
