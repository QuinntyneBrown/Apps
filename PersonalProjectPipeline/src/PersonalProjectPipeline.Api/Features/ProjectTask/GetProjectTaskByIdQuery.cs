// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Query to get a project task by ID.
/// </summary>
public record GetProjectTaskByIdQuery : IRequest<ProjectTaskDto?>
{
    public Guid ProjectTaskId { get; init; }
}

/// <summary>
/// Handler for GetProjectTaskByIdQuery.
/// </summary>
public class GetProjectTaskByIdQueryHandler : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDto?>
{
    private readonly IPersonalProjectPipelineContext _context;

    public GetProjectTaskByIdQueryHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectTaskDto?> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.ProjectTaskId == request.ProjectTaskId, cancellationToken);

        return task?.ToDto();
    }
}
