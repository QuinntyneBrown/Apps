// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Command to delete a milestone.
/// </summary>
public class DeleteMilestoneCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the milestone ID to delete.
    /// </summary>
    public Guid MilestoneId { get; set; }
}

/// <summary>
/// Handler for DeleteMilestoneCommand.
/// </summary>
public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, bool>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteMilestoneCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            return false;
        }

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
