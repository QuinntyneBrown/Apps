// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Query to get a focus session by ID.
/// </summary>
public class GetFocusSessionByIdQuery : IRequest<FocusSessionDto?>
{
    /// <summary>
    /// Gets or sets the focus session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }
}

/// <summary>
/// Handler for getting a focus session by ID.
/// </summary>
public class GetFocusSessionByIdQueryHandler : IRequestHandler<GetFocusSessionByIdQuery, FocusSessionDto?>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFocusSessionByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetFocusSessionByIdQueryHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<FocusSessionDto?> Handle(GetFocusSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .Include(s => s.Distractions)
            .FirstOrDefaultAsync(s => s.FocusSessionId == request.FocusSessionId, cancellationToken);

        if (session == null)
        {
            return null;
        }

        return new FocusSessionDto
        {
            FocusSessionId = session.FocusSessionId,
            UserId = session.UserId,
            SessionType = session.SessionType,
            Name = session.Name,
            PlannedDurationMinutes = session.PlannedDurationMinutes,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Notes = session.Notes,
            FocusScore = session.FocusScore,
            IsCompleted = session.IsCompleted,
            ActualDurationMinutes = session.GetActualDurationMinutes(),
            DistractionCount = session.GetDistractionCount(),
            CreatedAt = session.CreatedAt
        };
    }
}
