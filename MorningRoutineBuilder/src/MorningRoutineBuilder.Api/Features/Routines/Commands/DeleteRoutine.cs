// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Routines.Commands;

/// <summary>
/// Command to delete a routine.
/// </summary>
public class DeleteRoutine : IRequest
{
    public Guid RoutineId { get; set; }
}

/// <summary>
/// Handler for DeleteRoutine command.
/// </summary>
public class DeleteRoutineHandler : IRequestHandler<DeleteRoutine>
{
    private readonly IMorningRoutineBuilderContext _context;

    public DeleteRoutineHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoutine request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines
            .FirstOrDefaultAsync(r => r.RoutineId == request.RoutineId, cancellationToken);

        if (routine == null)
        {
            throw new KeyNotFoundException($"Routine with ID {request.RoutineId} not found.");
        }

        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
