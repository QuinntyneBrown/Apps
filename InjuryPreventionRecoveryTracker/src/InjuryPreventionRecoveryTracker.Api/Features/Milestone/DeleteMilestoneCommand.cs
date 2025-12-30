// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to delete a milestone.
/// </summary>
public record DeleteMilestoneCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; init; }
}

/// <summary>
/// Handler for DeleteMilestoneCommand.
/// </summary>
public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, bool>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<DeleteMilestoneCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteMilestoneCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<DeleteMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting milestone {MilestoneId}",
            request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning(
                "Milestone {MilestoneId} not found",
                request.MilestoneId);
            return false;
        }

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted milestone {MilestoneId}",
            request.MilestoneId);

        return true;
    }
}
