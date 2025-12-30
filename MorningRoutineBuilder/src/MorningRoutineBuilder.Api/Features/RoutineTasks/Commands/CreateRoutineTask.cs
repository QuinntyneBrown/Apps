// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks.Commands;

/// <summary>
/// Command to create a new routine task.
/// </summary>
public class CreateRoutineTask : IRequest<RoutineTaskDto>
{
    public Guid RoutineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskType TaskType { get; set; }
    public string? Description { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int SortOrder { get; set; }
    public bool IsOptional { get; set; }
}

/// <summary>
/// Handler for CreateRoutineTask command.
/// </summary>
public class CreateRoutineTaskHandler : IRequestHandler<CreateRoutineTask, RoutineTaskDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public CreateRoutineTaskHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineTaskDto> Handle(CreateRoutineTask request, CancellationToken cancellationToken)
    {
        var routineTask = new RoutineTask
        {
            RoutineTaskId = Guid.NewGuid(),
            RoutineId = request.RoutineId,
            Name = request.Name,
            TaskType = request.TaskType,
            Description = request.Description,
            EstimatedDurationMinutes = request.EstimatedDurationMinutes,
            SortOrder = request.SortOrder,
            IsOptional = request.IsOptional,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(routineTask);
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
