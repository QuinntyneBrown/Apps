// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping.Commands;

public class UpdateWorkoutMappingCommand : IRequest<WorkoutMappingDto>
{
    public Guid WorkoutMappingId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string? MuscleGroup { get; set; }
    public string? Instructions { get; set; }
    public bool IsFavorite { get; set; }
}

public class UpdateWorkoutMappingCommandHandler : IRequestHandler<UpdateWorkoutMappingCommand, WorkoutMappingDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public UpdateWorkoutMappingCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<WorkoutMappingDto> Handle(UpdateWorkoutMappingCommand request, CancellationToken cancellationToken)
    {
        var workoutMapping = await _context.WorkoutMappings
            .FirstOrDefaultAsync(w => w.WorkoutMappingId == request.WorkoutMappingId, cancellationToken);

        if (workoutMapping == null)
        {
            throw new KeyNotFoundException($"WorkoutMapping with ID {request.WorkoutMappingId} not found.");
        }

        workoutMapping.ExerciseName = request.ExerciseName;
        workoutMapping.MuscleGroup = request.MuscleGroup;
        workoutMapping.Instructions = request.Instructions;
        workoutMapping.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

        return WorkoutMappingDto.FromEntity(workoutMapping);
    }
}
