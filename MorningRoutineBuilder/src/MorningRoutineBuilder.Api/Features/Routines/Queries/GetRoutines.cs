// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Routines.Queries;

/// <summary>
/// Query to get all routines.
/// </summary>
public class GetRoutines : IRequest<List<RoutineDto>>
{
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetRoutines query.
/// </summary>
public class GetRoutinesHandler : IRequestHandler<GetRoutines, List<RoutineDto>>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetRoutinesHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<RoutineDto>> Handle(GetRoutines request, CancellationToken cancellationToken)
    {
        var query = _context.Routines.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        var routines = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return routines.Select(r => new RoutineDto
        {
            RoutineId = r.RoutineId,
            UserId = r.UserId,
            Name = r.Name,
            Description = r.Description,
            TargetStartTime = r.TargetStartTime,
            EstimatedDurationMinutes = r.EstimatedDurationMinutes,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt
        }).ToList();
    }
}
