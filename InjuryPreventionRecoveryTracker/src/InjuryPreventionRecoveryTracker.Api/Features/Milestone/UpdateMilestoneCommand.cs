// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to update an existing milestone.
/// </summary>
public record UpdateMilestoneCommand : IRequest<MilestoneDto?>
{
    /// <summary>
    /// Gets or sets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the target date.
    /// </summary>
    public DateTime? TargetDate { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the milestone is achieved.
    /// </summary>
    public bool IsAchieved { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateMilestoneCommand.
/// </summary>
public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<UpdateMilestoneCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateMilestoneCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<UpdateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto?> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating milestone {MilestoneId}",
            request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning(
                "Milestone {MilestoneId} not found",
                request.MilestoneId);
            return null;
        }

        milestone.Name = request.Name;
        milestone.Description = request.Description;
        milestone.TargetDate = request.TargetDate;
        milestone.Notes = request.Notes;

        // If marking as achieved and not previously achieved, set achieved date
        if (request.IsAchieved && !milestone.IsAchieved)
        {
            milestone.MarkAsAchieved();
        }
        else if (!request.IsAchieved && milestone.IsAchieved)
        {
            // If unmarking as achieved
            milestone.IsAchieved = false;
            milestone.AchievedDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated milestone {MilestoneId}",
            request.MilestoneId);

        return milestone.ToDto();
    }
}
