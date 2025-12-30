// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Routines.Queries;

/// <summary>
/// Query to get a routine by ID.
/// </summary>
public class GetRoutineById : IRequest<RoutineDto?>
{
    public Guid RoutineId { get; set; }
}

/// <summary>
/// Handler for GetRoutineById query.
/// </summary>
public class GetRoutineByIdHandler : IRequestHandler<GetRoutineById, RoutineDto?>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetRoutineByIdHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineDto?> Handle(GetRoutineById request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines
            .FirstOrDefaultAsync(r => r.RoutineId == request.RoutineId, cancellationToken);

        if (routine == null)
        {
            return null;
        }

        return new RoutineDto
        {
            RoutineId = routine.RoutineId,
            UserId = routine.UserId,
            Name = routine.Name,
            Description = routine.Description,
            TargetStartTime = routine.TargetStartTime,
            EstimatedDurationMinutes = routine.EstimatedDurationMinutes,
            IsActive = routine.IsActive,
            CreatedAt = routine.CreatedAt
        };
    }
}
