// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping.Queries;

public class GetWorkoutMappingByIdQuery : IRequest<WorkoutMappingDto?>
{
    public Guid WorkoutMappingId { get; set; }
}

public class GetWorkoutMappingByIdQueryHandler : IRequestHandler<GetWorkoutMappingByIdQuery, WorkoutMappingDto?>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetWorkoutMappingByIdQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<WorkoutMappingDto?> Handle(GetWorkoutMappingByIdQuery request, CancellationToken cancellationToken)
    {
        var workoutMapping = await _context.WorkoutMappings
            .FirstOrDefaultAsync(w => w.WorkoutMappingId == request.WorkoutMappingId, cancellationToken);

        return workoutMapping == null ? null : WorkoutMappingDto.FromEntity(workoutMapping);
    }
}
