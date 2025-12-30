// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.Distraction;

/// <summary>
/// Command to create a new distraction.
/// </summary>
public class CreateDistractionCommand : IRequest<DistractionDto>
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the distraction type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets when the distraction occurred.
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    public double? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the distraction is internal.
    /// </summary>
    public bool IsInternal { get; set; }
}

/// <summary>
/// Handler for creating a distraction.
/// </summary>
public class CreateDistractionCommandHandler : IRequestHandler<CreateDistractionCommand, DistractionDto>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDistractionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateDistractionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<DistractionDto> Handle(CreateDistractionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.FocusSessionId == request.FocusSessionId, cancellationToken);

        if (session == null)
        {
            throw new KeyNotFoundException($"Focus session with ID {request.FocusSessionId} not found.");
        }

        var distraction = new Core.Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = request.FocusSessionId,
            Type = request.Type,
            Description = request.Description,
            OccurredAt = request.OccurredAt,
            DurationMinutes = request.DurationMinutes,
            IsInternal = request.IsInternal,
            CreatedAt = DateTime.UtcNow
        };

        _context.Distractions.Add(distraction);
        await _context.SaveChangesAsync(cancellationToken);

        return new DistractionDto
        {
            DistractionId = distraction.DistractionId,
            FocusSessionId = distraction.FocusSessionId,
            Type = distraction.Type,
            Description = distraction.Description,
            OccurredAt = distraction.OccurredAt,
            DurationMinutes = distraction.DurationMinutes,
            IsInternal = distraction.IsInternal,
            CreatedAt = distraction.CreatedAt
        };
    }
}
