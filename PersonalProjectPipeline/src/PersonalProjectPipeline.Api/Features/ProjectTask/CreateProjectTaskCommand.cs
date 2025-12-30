// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Command to create a new project task.
/// </summary>
public record CreateProjectTaskCommand : IRequest<ProjectTaskDto>
{
    public Guid ProjectId { get; init; }
    public Guid? MilestoneId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public double? EstimatedHours { get; init; }
}

/// <summary>
/// Handler for CreateProjectTaskCommand.
/// </summary>
public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand, ProjectTaskDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public CreateProjectTaskCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectTaskDto> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Core.ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            MilestoneId = request.MilestoneId,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            EstimatedHours = request.EstimatedHours,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return task.ToDto();
    }
}
