// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks.Commands;

/// <summary>
/// Command to update an existing routine task.
/// </summary>
public class UpdateRoutineTask : IRequest<RoutineTaskDto>
{
    public Guid RoutineTaskId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskType TaskType { get; set; }
    public string? Description { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int SortOrder { get; set; }
    public bool IsOptional { get; set; }
}

/// <summary>
/// Handler for UpdateRoutineTask command.
/// </summary>
public class UpdateRoutineTaskHandler : IRequestHandler<UpdateRoutineTask, RoutineTaskDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public UpdateRoutineTaskHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineTaskDto> Handle(UpdateRoutineTask request, CancellationToken cancellationToken)
    {
        var routineTask = await _context.Tasks
            .FirstOrDefaultAsync(t => t.RoutineTaskId == request.RoutineTaskId, cancellationToken);

        if (routineTask == null)
        {
            throw new KeyNotFoundException($"RoutineTask with ID {request.RoutineTaskId} not found.");
        }

        routineTask.Name = request.Name;
        routineTask.TaskType = request.TaskType;
        routineTask.Description = request.Description;
        routineTask.EstimatedDurationMinutes = request.EstimatedDurationMinutes;
        routineTask.SortOrder = request.SortOrder;
        routineTask.IsOptional = request.IsOptional;

        await _context.SaveChangesAsync(cancellationToken);

        return new RoutineTaskDto
        {
            RoutineTaskId = routineTask.RoutineTaskId,
            RoutineId = routineTask.RoutineId,
            Name = routineTask.Name,
            TaskType = routineTask.TaskType,
            Description = routineTask.Description,
            EstimatedDurationMinutes = routineTask.EstimatedDurationMinutes,
            SortOrder = routineTask.SortOrder,
            IsOptional = routineTask.IsOptional,
            CreatedAt = routineTask.CreatedAt
        };
    }
}
