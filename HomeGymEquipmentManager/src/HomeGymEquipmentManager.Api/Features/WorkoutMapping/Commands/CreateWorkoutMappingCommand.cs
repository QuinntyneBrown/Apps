// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping.Commands;

public class CreateWorkoutMappingCommand : IRequest<WorkoutMappingDto>
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string? MuscleGroup { get; set; }
    public string? Instructions { get; set; }
    public bool IsFavorite { get; set; }
}

public class CreateWorkoutMappingCommandHandler : IRequestHandler<CreateWorkoutMappingCommand, WorkoutMappingDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public CreateWorkoutMappingCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<WorkoutMappingDto> Handle(CreateWorkoutMappingCommand request, CancellationToken cancellationToken)
    {
        var workoutMapping = new Core.WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = request.UserId,
            EquipmentId = request.EquipmentId,
            ExerciseName = request.ExerciseName,
            MuscleGroup = request.MuscleGroup,
            Instructions = request.Instructions,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkoutMappings.Add(workoutMapping);
        await _context.SaveChangesAsync(cancellationToken);

        return WorkoutMappingDto.FromEntity(workoutMapping);
    }
}
