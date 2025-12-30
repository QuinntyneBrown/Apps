// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get all milestones for an injury.
/// </summary>
public record GetMilestonesQuery : IRequest<IEnumerable<MilestoneDto>>
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }
}

/// <summary>
/// Handler for GetMilestonesQuery.
/// </summary>
public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, IEnumerable<MilestoneDto>>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetMilestonesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMilestonesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetMilestonesQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetMilestonesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all milestones for injury {InjuryId}",
            request.InjuryId);

        var milestones = await _context.Milestones
            .AsNoTracking()
            .Where(x => x.InjuryId == request.InjuryId)
            .OrderBy(x => x.TargetDate)
            .ToListAsync(cancellationToken);

        return milestones.Select(x => x.ToDto());
    }
}
