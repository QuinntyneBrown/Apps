// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Command to create a new progress entry.
/// </summary>
public class CreateProgressCommand : IRequest<ProgressDto>
{
    /// <summary>
    /// Gets or sets the goal ID this progress entry belongs to.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who is creating this progress entry.
    /// </summary>
    public Guid UserId { get; set; }

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
/// Handler for CreateProgressCommand.
/// </summary>
public class CreateProgressCommandHandler : IRequestHandler<CreateProgressCommand, ProgressDto>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProgressCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateProgressCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProgressDto> Handle(CreateProgressCommand request, CancellationToken cancellationToken)
    {
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = request.GoalId,
            UserId = request.UserId,
            ProgressDate = request.ProgressDate,
            Notes = request.Notes,
            CompletionPercentage = request.CompletionPercentage,
            EffortHours = request.EffortHours,
            IsSignificant = request.IsSignificant,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync(cancellationToken);

        return ProgressDto.FromProgress(progress);
    }
}
