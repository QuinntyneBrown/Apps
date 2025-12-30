// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.SessionAnalytics;

/// <summary>
/// Query to get analytics.
/// </summary>
public class GetAnalyticsQuery : IRequest<List<SessionAnalyticsDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for getting analytics.
/// </summary>
public class GetAnalyticsQueryHandler : IRequestHandler<GetAnalyticsQuery, List<SessionAnalyticsDto>>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAnalyticsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAnalyticsQueryHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<SessionAnalyticsDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Analytics.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        var analytics = await query
            .OrderByDescending(a => a.PeriodEndDate)
            .ToListAsync(cancellationToken);

        return analytics.Select(a => new SessionAnalyticsDto
        {
            SessionAnalyticsId = a.SessionAnalyticsId,
            UserId = a.UserId,
            PeriodStartDate = a.PeriodStartDate,
            PeriodEndDate = a.PeriodEndDate,
            TotalSessions = a.TotalSessions,
            TotalFocusMinutes = a.TotalFocusMinutes,
            AverageFocusScore = a.AverageFocusScore,
            TotalDistractions = a.TotalDistractions,
            CompletionRate = a.CompletionRate,
            MostProductiveSessionType = a.MostProductiveSessionType,
            AverageSessionDuration = a.GetAverageSessionDuration(),
            AverageDistractions = a.GetAverageDistractions(),
            CreatedAt = a.CreatedAt
        }).ToList();
    }
}
