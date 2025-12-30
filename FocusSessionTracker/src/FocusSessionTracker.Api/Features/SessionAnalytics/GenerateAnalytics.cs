// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.SessionAnalytics;

/// <summary>
/// Command to generate analytics for a user.
/// </summary>
public class GenerateAnalyticsCommand : IRequest<SessionAnalyticsDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the period start date.
    /// </summary>
    public DateTime PeriodStartDate { get; set; }

    /// <summary>
    /// Gets or sets the period end date.
    /// </summary>
    public DateTime PeriodEndDate { get; set; }
}

/// <summary>
/// Handler for generating analytics.
/// </summary>
public class GenerateAnalyticsCommandHandler : IRequestHandler<GenerateAnalyticsCommand, SessionAnalyticsDto>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateAnalyticsCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GenerateAnalyticsCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<SessionAnalyticsDto> Handle(GenerateAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var sessions = await _context.Sessions
            .Include(s => s.Distractions)
            .Where(s => s.UserId == request.UserId
                && s.StartTime >= request.PeriodStartDate
                && s.StartTime <= request.PeriodEndDate)
            .ToListAsync(cancellationToken);

        var totalSessions = sessions.Count;
        var completedSessions = sessions.Count(s => s.IsCompleted);
        var totalFocusMinutes = sessions
            .Where(s => s.EndTime.HasValue)
            .Sum(s => s.GetActualDurationMinutes() ?? 0);
        var averageFocusScore = sessions
            .Where(s => s.FocusScore.HasValue)
            .Average(s => (double?)s.FocusScore);
        var totalDistractions = sessions.Sum(s => s.Distractions.Count);
        var completionRate = totalSessions > 0 ? (double)completedSessions / totalSessions * 100 : 0;

        // Find most productive session type (highest average focus score)
        var sessionTypeStats = sessions
            .Where(s => s.FocusScore.HasValue)
            .GroupBy(s => s.SessionType)
            .Select(g => new
            {
                SessionType = g.Key,
                AverageScore = g.Average(s => s.FocusScore!.Value)
            })
            .OrderByDescending(x => x.AverageScore)
            .FirstOrDefault();

        var analytics = new Core.SessionAnalytics
        {
            SessionAnalyticsId = Guid.NewGuid(),
            UserId = request.UserId,
            PeriodStartDate = request.PeriodStartDate,
            PeriodEndDate = request.PeriodEndDate,
            TotalSessions = totalSessions,
            TotalFocusMinutes = totalFocusMinutes,
            AverageFocusScore = averageFocusScore,
            TotalDistractions = totalDistractions,
            CompletionRate = completionRate,
            MostProductiveSessionType = sessionTypeStats?.SessionType,
            CreatedAt = DateTime.UtcNow
        };

        _context.Analytics.Add(analytics);
        await _context.SaveChangesAsync(cancellationToken);

        return new SessionAnalyticsDto
        {
            SessionAnalyticsId = analytics.SessionAnalyticsId,
            UserId = analytics.UserId,
            PeriodStartDate = analytics.PeriodStartDate,
            PeriodEndDate = analytics.PeriodEndDate,
            TotalSessions = analytics.TotalSessions,
            TotalFocusMinutes = analytics.TotalFocusMinutes,
            AverageFocusScore = analytics.AverageFocusScore,
            TotalDistractions = analytics.TotalDistractions,
            CompletionRate = analytics.CompletionRate,
            MostProductiveSessionType = analytics.MostProductiveSessionType,
            AverageSessionDuration = analytics.GetAverageSessionDuration(),
            AverageDistractions = analytics.GetAverageDistractions(),
            CreatedAt = analytics.CreatedAt
        };
    }
}
