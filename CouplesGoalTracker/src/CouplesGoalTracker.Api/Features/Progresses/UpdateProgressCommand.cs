// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Command to update an existing progress entry.
/// </summary>
public class UpdateProgressCommand : IRequest<ProgressDto?>
{
    /// <summary>
    /// Gets or sets the progress ID to update.
    /// </summary>
    public Guid ProgressId { get; set; }

    /// <summary>
    /// Gets or sets the date of the progress update.
    /// </summary>
    public DateTime ProgressDate { get; set; }

    /// <summary>
    /// Gets or sets the notes or description of the progress.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the completion percentage at the time of this update (0-100).
    /// </summary>
    public double CompletionPercentage { get; set; }

    /// <summary>
    /// Gets or sets the effort hours spent on this progress update.
    /// </summary>
    public decimal? EffortHours { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this was a significant milestone.
    /// </summary>
    public bool IsSignificant { get; set; }
}

/// <summary>
/// Handler for UpdateProgressCommand.
/// </summary>
public class UpdateProgressCommandHandler : IRequestHandler<UpdateProgressCommand, ProgressDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProgressCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateProgressCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProgressDto?> Handle(UpdateProgressCommand request, CancellationToken cancellationToken)
    {
        var progress = await _context.Progresses
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        if (progress == null)
        {
            return null;
        }

        progress.ProgressDate = request.ProgressDate;
        progress.Notes = request.Notes;
        progress.CompletionPercentage = request.CompletionPercentage;
        progress.EffortHours = request.EffortHours;
        progress.IsSignificant = request.IsSignificant;
        progress.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return ProgressDto.FromProgress(progress);
    }
}
