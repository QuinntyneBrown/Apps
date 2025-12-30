// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Command to update an existing project task.
/// </summary>
public record UpdateProjectTaskCommand : IRequest<ProjectTaskDto>
{
    public Guid ProjectTaskId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public double? EstimatedHours { get; init; }
}

/// <summary>
/// Handler for UpdateProjectTaskCommand.
/// </summary>
public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, ProjectTaskDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public UpdateProjectTaskCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProjectTaskDto> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.ProjectTaskId == request.ProjectTaskId, cancellationToken)
            ?? throw new InvalidOperationException($"Task with ID {request.ProjectTaskId} not found.");

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.EstimatedHours = request.EstimatedHours;

        if (request.IsCompleted && !task.IsCompleted)
        {
            task.Complete();
        }
        else if (!request.IsCompleted && task.IsCompleted)
        {
            task.IsCompleted = false;
            task.CompletionDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return task.ToDto();
    }
}
