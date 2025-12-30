// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Command to calculate a blood pressure trend for a user.
/// </summary>
public record CalculateTrendCommand : IRequest<TrendDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }
}

/// <summary>
/// Handler for CalculateTrendCommand.
/// </summary>
public class CalculateTrendCommandHandler : IRequestHandler<CalculateTrendCommand, TrendDto>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<CalculateTrendCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculateTrendCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CalculateTrendCommandHandler(
        IBloodPressureMonitorContext context,
        ILogger<CalculateTrendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<TrendDto> Handle(CalculateTrendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Calculating trend for user {UserId} from {StartDate} to {EndDate}",
            request.UserId,
            request.StartDate,
            request.EndDate);

        var readings = await _context.Readings
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId &&
                       x.MeasuredAt >= request.StartDate &&
                       x.MeasuredAt <= request.EndDate)
            .OrderBy(x => x.MeasuredAt)
            .ToListAsync(cancellationToken);

        if (readings.Count == 0)
        {
            throw new InvalidOperationException("No readings found for the specified period.");
        }

        var avgSystolic = readings.Average(x => x.Systolic);
        var avgDiastolic = readings.Average(x => x.Diastolic);

        // Determine trend direction by comparing first half to second half
        var midpoint = readings.Count / 2;
        var firstHalfAvgSystolic = readings.Take(midpoint).Average(x => x.Systolic);
        var secondHalfAvgSystolic = readings.Skip(midpoint).Average(x => x.Systolic);

        string trendDirection;
        if (secondHalfAvgSystolic < firstHalfAvgSystolic - 5)
        {
            trendDirection = "Improving";
        }
        else if (secondHalfAvgSystolic > firstHalfAvgSystolic + 5)
        {
            trendDirection = "Worsening";
        }
        else
        {
            trendDirection = "Stable";
        }

        var trend = new Trend
        {
            TrendId = Guid.NewGuid(),
            UserId = request.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AverageSystolic = (decimal)avgSystolic,
            AverageDiastolic = (decimal)avgDiastolic,
            HighestSystolic = readings.Max(x => x.Systolic),
            HighestDiastolic = readings.Max(x => x.Diastolic),
            LowestSystolic = readings.Min(x => x.Systolic),
            LowestDiastolic = readings.Min(x => x.Diastolic),
            ReadingCount = readings.Count,
            TrendDirection = trendDirection,
            Insights = GenerateInsights(avgSystolic, avgDiastolic, trendDirection),
            CreatedAt = DateTime.UtcNow,
        };

        _context.Trends.Add(trend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created trend {TrendId} for user {UserId}, Direction: {TrendDirection}",
            trend.TrendId,
            request.UserId,
            trend.TrendDirection);

        return trend.ToDto();
    }

    private static string GenerateInsights(double avgSystolic, double avgDiastolic, string trendDirection)
    {
        var insights = new List<string>();

        if (avgSystolic >= 140 || avgDiastolic >= 90)
        {
            insights.Add("Your average blood pressure is in the hypertensive range. Consider consulting your doctor.");
        }
        else if (avgSystolic >= 130 || avgDiastolic >= 80)
        {
            insights.Add("Your average blood pressure is elevated. Lifestyle modifications may be beneficial.");
        }
        else
        {
            insights.Add("Your average blood pressure is within normal range. Keep up the good work!");
        }

        if (trendDirection == "Improving")
        {
            insights.Add("Your blood pressure trend is improving. Continue your current management plan.");
        }
        else if (trendDirection == "Worsening")
        {
            insights.Add("Your blood pressure trend is worsening. Consider reviewing your lifestyle and medications with your doctor.");
        }

        return string.Join(" ", insights);
    }
}
