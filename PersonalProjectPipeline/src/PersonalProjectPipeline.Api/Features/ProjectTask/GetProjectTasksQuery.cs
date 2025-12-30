// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Query to get all project tasks.
/// </summary>
public record GetProjectTasksQuery : IRequest<List<ProjectTaskDto>>
{
    public Guid? ProjectId { get; init; }
    public Guid? MilestoneId { get; init; }
}

/// <summary>
/// Handler for GetProjectTasksQuery.
/// </summary>
public class GetProjectTasksQueryHandler : IRequestHandler<GetProjectTasksQuery, List<ProjectTaskDto>>
{
    private readonly IPersonalProjectPipelineContext _context;

    public GetProjectTasksQueryHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<ProjectTaskDto>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tasks.AsQueryable();

        if (request.ProjectId.HasValue)
        {
            query = query.Where(t => t.ProjectId == request.ProjectId.Value);
        }

        if (request.MilestoneId.HasValue)
        {
            query = query.Where(t => t.MilestoneId == request.MilestoneId.Value);
        }

        var tasks = await query
            .OrderBy(t => t.DueDate)
            .ThenBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tasks.Select(t => t.ToDto()).ToList();
    }
}
