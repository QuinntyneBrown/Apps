// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Command to update a focus session.
/// </summary>
public class UpdateFocusSessionCommand : IRequest<FocusSessionDto>
{
    /// <summary>
    /// Gets or sets the focus session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the session name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the focus score.
    /// </summary>
    public int? FocusScore { get; set; }
}

/// <summary>
/// Handler for updating a focus session.
/// </summary>
public class UpdateFocusSessionCommandHandler : IRequestHandler<UpdateFocusSessionCommand, FocusSessionDto>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFocusSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateFocusSessionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<FocusSessionDto> Handle(UpdateFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .Include(s => s.Distractions)
            .FirstOrDefaultAsync(s => s.FocusSessionId == request.FocusSessionId, cancellationToken);

        if (session == null)
        {
            throw new KeyNotFoundException($"Focus session with ID {request.FocusSessionId} not found.");
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            session.Name = request.Name;
        }

        if (request.Notes != null)
        {
            session.Notes = request.Notes;
        }

        if (request.FocusScore.HasValue)
        {
            session.FocusScore = request.FocusScore.Value;
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
