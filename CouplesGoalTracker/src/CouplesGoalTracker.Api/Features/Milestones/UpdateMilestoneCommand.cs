// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Command to update an existing milestone.
/// </summary>
public class UpdateMilestoneCommand : IRequest<MilestoneDto?>
{
    /// <summary>
    /// Gets or sets the milestone ID to update.
    /// </summary>
    public Guid MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the title of the milestone.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the milestone.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this milestone is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the sort order of this milestone.
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// Handler for UpdateMilestoneCommand.
/// </summary>
public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateMilestoneCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto?> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            return null;
        }

        milestone.Title = request.Title;
        milestone.Description = request.Description;
        milestone.TargetDate = request.TargetDate;
        milestone.SortOrder = request.SortOrder;
        milestone.UpdatedAt = DateTime.UtcNow;

        if (request.IsCompleted && !milestone.IsCompleted)
        {
            milestone.MarkAsCompleted();
        }
        else if (!request.IsCompleted && milestone.IsCompleted)
        {
            milestone.IsCompleted = false;
            milestone.CompletedDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return MilestoneDto.FromMilestone(milestone);
    }
}
