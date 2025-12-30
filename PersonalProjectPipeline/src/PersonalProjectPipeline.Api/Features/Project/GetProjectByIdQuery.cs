// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Project;

/// <summary>
/// Query to get a project by ID.
/// </summary>
public record GetProjectByIdQuery : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for GetProjectByIdQuery.
/// </summary>
public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IPersonalProjectPipelineContext _context;

    public GetProjectByIdQueryHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Milestones)
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        return project?.ToDto();
    }
}
