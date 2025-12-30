// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Command to create a new focus session.
/// </summary>
public class CreateFocusSessionCommand : IRequest<FocusSessionDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the session type.
    /// </summary>
    public SessionType SessionType { get; set; }

    /// <summary>
    /// Gets or sets the session name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the planned duration in minutes.
    /// </summary>
    public int PlannedDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for creating a focus session.
/// </summary>
public class CreateFocusSessionCommandHandler : IRequestHandler<CreateFocusSessionCommand, FocusSessionDto>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFocusSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateFocusSessionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<FocusSessionDto> Handle(CreateFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = request.UserId,
            SessionType = request.SessionType,
            Name = request.Name,
            PlannedDurationMinutes = request.PlannedDurationMinutes,
            StartTime = request.StartTime,
            Notes = request.Notes,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Sessions.Add(session);
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
