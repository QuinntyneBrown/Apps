// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks.Queries;

/// <summary>
/// Query to get a routine task by ID.
/// </summary>
public class GetRoutineTaskById : IRequest<RoutineTaskDto?>
{
    public Guid RoutineTaskId { get; set; }
}

/// <summary>
/// Handler for GetRoutineTaskById query.
/// </summary>
public class GetRoutineTaskByIdHandler : IRequestHandler<GetRoutineTaskById, RoutineTaskDto?>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetRoutineTaskByIdHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineTaskDto?> Handle(GetRoutineTaskById request, CancellationToken cancellationToken)
    {
        var routineTask = await _context.Tasks
            .FirstOrDefaultAsync(t => t.RoutineTaskId == request.RoutineTaskId, cancellationToken);

        if (routineTask == null)
        {
            return null;
        }

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
