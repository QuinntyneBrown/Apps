// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Query to get all milestones.
/// </summary>
public record GetMilestonesQuery : IRequest<List<MilestoneDto>>
{
    public Guid? ProjectId { get; init; }
}

/// <summary>
/// Handler for GetMilestonesQuery.
/// </summary>
public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, List<MilestoneDto>>
{
    private readonly IPersonalProjectPipelineContext _context;

    public GetMilestonesQueryHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Milestones.AsQueryable();

        if (request.ProjectId.HasValue)
        {
            query = query.Where(m => m.ProjectId == request.ProjectId.Value);
        }

        var milestones = await query
            .OrderBy(m => m.TargetDate)
            .ThenBy(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        return milestones.Select(m => m.ToDto()).ToList();
    }
}
