// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks.Commands;

/// <summary>
/// Command to delete a routine task.
/// </summary>
public class DeleteRoutineTask : IRequest
{
    public Guid RoutineTaskId { get; set; }
}

/// <summary>
/// Handler for DeleteRoutineTask command.
/// </summary>
public class DeleteRoutineTaskHandler : IRequestHandler<DeleteRoutineTask>
{
    private readonly IMorningRoutineBuilderContext _context;

    public DeleteRoutineTaskHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoutineTask request, CancellationToken cancellationToken)
    {
        var routineTask = await _context.Tasks
            .FirstOrDefaultAsync(t => t.RoutineTaskId == request.RoutineTaskId, cancellationToken);

        if (routineTask == null)
        {
            throw new KeyNotFoundException($"RoutineTask with ID {request.RoutineTaskId} not found.");
        }

        _context.Tasks.Remove(routineTask);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
