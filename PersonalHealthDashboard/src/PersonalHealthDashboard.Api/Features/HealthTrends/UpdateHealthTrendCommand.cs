using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record UpdateHealthTrendCommand : IRequest<HealthTrendDto?>
{
    public Guid HealthTrendId { get; init; }
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

public class UpdateHealthTrendCommandHandler : IRequestHandler<UpdateHealthTrendCommand, HealthTrendDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<UpdateHealthTrendCommandHandler> _logger;

    public UpdateHealthTrendCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<UpdateHealthTrendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HealthTrendDto?> Handle(UpdateHealthTrendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating health trend {HealthTrendId}", request.HealthTrendId);

        var healthTrend = await _context.HealthTrends
            .FirstOrDefaultAsync(h => h.HealthTrendId == request.HealthTrendId, cancellationToken);

        if (healthTrend == null)
        {
            _logger.LogWarning("Health trend {HealthTrendId} not found", request.HealthTrendId);
            return null;
        }

        healthTrend.MetricName = request.MetricName;
        healthTrend.StartDate = request.StartDate;
        healthTrend.EndDate = request.EndDate;
        healthTrend.AverageValue = request.AverageValue;
        healthTrend.MinValue = request.MinValue;
        healthTrend.MaxValue = request.MaxValue;
        healthTrend.TrendDirection = request.TrendDirection;
        healthTrend.PercentageChange = request.PercentageChange;
        healthTrend.Insights = request.Insights;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated health trend {HealthTrendId}", request.HealthTrendId);

        return healthTrend.ToDto();
    }
}
