// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Query to get all focus sessions.
/// </summary>
public class GetFocusSessionsQuery : IRequest<List<FocusSessionDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional session type filter.
    /// </summary>
    public SessionType? SessionType { get; set; }

    /// <summary>
    /// Gets or sets the optional start date filter.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the optional end date filter.
    /// </summary>
    public DateTime? EndDate { get; set; }
}

/// <summary>
/// Handler for getting focus sessions.
/// </summary>
public class GetFocusSessionsQueryHandler : IRequestHandler<GetFocusSessionsQuery, List<FocusSessionDto>>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFocusSessionsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetFocusSessionsQueryHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<FocusSessionDto>> Handle(GetFocusSessionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Sessions
            .Include(s => s.Distractions)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.SessionType.HasValue)
        {
            query = query.Where(s => s.SessionType == request.SessionType.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(s => s.StartTime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(s => s.StartTime <= request.EndDate.Value);
        }

        var sessions = await query
            .OrderByDescending(s => s.StartTime)
            .ToListAsync(cancellationToken);

        return sessions.Select(s => new FocusSessionDto
        {
            FocusSessionId = s.FocusSessionId,
            UserId = s.UserId,
            SessionType = s.SessionType,
            Name = s.Name,
            PlannedDurationMinutes = s.PlannedDurationMinutes,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Notes = s.Notes,
            FocusScore = s.FocusScore,
            IsCompleted = s.IsCompleted,
            ActualDurationMinutes = s.GetActualDurationMinutes(),
            DistractionCount = s.GetDistractionCount(),
            CreatedAt = s.CreatedAt
        }).ToList();
    }
}
