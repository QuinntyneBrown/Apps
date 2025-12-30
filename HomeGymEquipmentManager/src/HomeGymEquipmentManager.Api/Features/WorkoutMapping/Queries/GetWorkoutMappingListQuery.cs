// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping.Queries;

public class GetWorkoutMappingListQuery : IRequest<List<WorkoutMappingDto>>
{
    public Guid? UserId { get; set; }
    public Guid? EquipmentId { get; set; }
    public bool? IsFavorite { get; set; }
}

public class GetWorkoutMappingListQueryHandler : IRequestHandler<GetWorkoutMappingListQuery, List<WorkoutMappingDto>>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetWorkoutMappingListQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutMappingDto>> Handle(GetWorkoutMappingListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.WorkoutMappings.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(w => w.UserId == request.UserId.Value);
        }

        if (request.EquipmentId.HasValue)
        {
            query = query.Where(w => w.EquipmentId == request.EquipmentId.Value);
        }

        if (request.IsFavorite.HasValue)
        {
            query = query.Where(w => w.IsFavorite == request.IsFavorite.Value);
        }

        var workoutMappings = await query
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync(cancellationToken);

        return workoutMappings.Select(WorkoutMappingDto.FromEntity).ToList();
    }
}
