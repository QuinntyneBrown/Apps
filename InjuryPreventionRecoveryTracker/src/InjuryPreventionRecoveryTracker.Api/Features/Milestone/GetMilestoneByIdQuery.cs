// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get a milestone by ID.
/// </summary>
public record GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    /// <summary>
    /// Gets or sets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; init; }
}

/// <summary>
/// Handler for GetMilestoneByIdQuery.
/// </summary>
public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetMilestoneByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMilestoneByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetMilestoneByIdQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetMilestoneByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting milestone {MilestoneId}",
            request.MilestoneId);

        var milestone = await _context.Milestones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogInformation(
                "Milestone {MilestoneId} not found",
                request.MilestoneId);
            return null;
        }

        return milestone.ToDto();
    }
}
