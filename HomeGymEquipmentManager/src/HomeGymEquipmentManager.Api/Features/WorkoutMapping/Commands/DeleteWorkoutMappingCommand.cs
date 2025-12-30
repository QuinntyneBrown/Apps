// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping.Commands;

public class DeleteWorkoutMappingCommand : IRequest<Unit>
{
    public Guid WorkoutMappingId { get; set; }
}

public class DeleteWorkoutMappingCommandHandler : IRequestHandler<DeleteWorkoutMappingCommand, Unit>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public DeleteWorkoutMappingCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWorkoutMappingCommand request, CancellationToken cancellationToken)
    {
        var workoutMapping = await _context.WorkoutMappings
            .FirstOrDefaultAsync(w => w.WorkoutMappingId == request.WorkoutMappingId, cancellationToken);

        if (workoutMapping == null)
        {
            throw new KeyNotFoundException($"WorkoutMapping with ID {request.WorkoutMappingId} not found.");
        }

        _context.WorkoutMappings.Remove(workoutMapping);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
