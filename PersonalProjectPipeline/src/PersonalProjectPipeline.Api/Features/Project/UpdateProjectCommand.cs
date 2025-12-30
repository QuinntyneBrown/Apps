// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Project;

/// <summary>
/// Command to update an existing project.
/// </summary>
public record UpdateProjectCommand : IRequest<ProjectDto>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public ProjectStatus Status { get; init; }
    public ProjectPriority Priority { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public string? Tags { get; init; }
}

/// <summary>
/// Handler for UpdateProjectCommand.
/// </summary>
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public UpdateProjectCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken)
            ?? throw new InvalidOperationException($"Project with ID {request.ProjectId} not found.");

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status;
        project.Priority = request.Priority;
        project.StartDate = request.StartDate;
        project.TargetDate = request.TargetDate;
        project.Tags = request.Tags;

        await _context.SaveChangesAsync(cancellationToken);

        return project.ToDto();
    }
}
