// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Project;

/// <summary>
/// Command to create a new project.
/// </summary>
public record CreateProjectCommand : IRequest<ProjectDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public ProjectStatus Status { get; init; }
    public ProjectPriority Priority { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public string? Tags { get; init; }
}

/// <summary>
/// Handler for CreateProjectCommand.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public CreateProjectCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Core.Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            StartDate = request.StartDate,
            TargetDate = request.TargetDate,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.ToDto();
    }
}
