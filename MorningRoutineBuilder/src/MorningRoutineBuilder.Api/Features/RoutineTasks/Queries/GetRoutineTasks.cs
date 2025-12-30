// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks.Queries;

/// <summary>
/// Query to get all routine tasks.
/// </summary>
public class GetRoutineTasks : IRequest<List<RoutineTaskDto>>
{
    public Guid? RoutineId { get; set; }
}

/// <summary>
/// Handler for GetRoutineTasks query.
/// </summary>
public class GetRoutineTasksHandler : IRequestHandler<GetRoutineTasks, List<RoutineTaskDto>>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetRoutineTasksHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<RoutineTaskDto>> Handle(GetRoutineTasks request, CancellationToken cancellationToken)
    {
        var query = _context.Tasks.AsQueryable();

        if (request.RoutineId.HasValue)
        {
            query = query.Where(t => t.RoutineId == request.RoutineId.Value);
        }

        var tasks = await query
            .OrderBy(t => t.SortOrder)
            .ThenBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tasks.Select(t => new RoutineTaskDto
        {
            RoutineTaskId = t.RoutineTaskId,
            RoutineId = t.RoutineId,
            Name = t.Name,
            TaskType = t.TaskType,
            Description = t.Description,
            EstimatedDurationMinutes = t.EstimatedDurationMinutes,
            SortOrder = t.SortOrder,
            IsOptional = t.IsOptional,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}
