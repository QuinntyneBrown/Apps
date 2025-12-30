// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Query to get a milestone by ID.
/// </summary>
public record GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
}

/// <summary>
/// Handler for GetMilestoneByIdQuery.
/// </summary>
public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly IPersonalProjectPipelineContext _context;

    public GetMilestoneByIdQueryHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .Include(m => m.Tasks)
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        return milestone?.ToDto();
    }
}
