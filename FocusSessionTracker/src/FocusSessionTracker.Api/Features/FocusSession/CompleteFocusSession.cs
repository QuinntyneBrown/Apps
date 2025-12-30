// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Command to complete a focus session.
/// </summary>
public class CompleteFocusSessionCommand : IRequest<FocusSessionDto>
{
    /// <summary>
    /// Gets or sets the focus session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Gets or sets the focus score.
    /// </summary>
    public int? FocusScore { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for completing a focus session.
/// </summary>
public class CompleteFocusSessionCommandHandler : IRequestHandler<CompleteFocusSessionCommand, FocusSessionDto>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompleteFocusSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CompleteFocusSessionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<FocusSessionDto> Handle(CompleteFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .Include(s => s.Distractions)
            .FirstOrDefaultAsync(s => s.FocusSessionId == request.FocusSessionId, cancellationToken);

        if (session == null)
        {
            throw new KeyNotFoundException($"Focus session with ID {request.FocusSessionId} not found.");
        }

        session.EndSession(request.EndTime);

        if (request.FocusScore.HasValue)
        {
            session.FocusScore = request.FocusScore.Value;
        }

        if (request.Notes != null)
        {
            session.Notes = request.Notes;
        }

        await _context.SaveChangesAsync(cancellationToken);

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
