// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Routines.Commands;

/// <summary>
/// Command to update an existing routine.
/// </summary>
public class UpdateRoutine : IRequest<RoutineDto>
{
    public Guid RoutineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeSpan TargetStartTime { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Handler for UpdateRoutine command.
/// </summary>
public class UpdateRoutineHandler : IRequestHandler<UpdateRoutine, RoutineDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public UpdateRoutineHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineDto> Handle(UpdateRoutine request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines
            .FirstOrDefaultAsync(r => r.RoutineId == request.RoutineId, cancellationToken);

        if (routine == null)
        {
            throw new KeyNotFoundException($"Routine with ID {request.RoutineId} not found.");
        }

        routine.Name = request.Name;
        routine.Description = request.Description;
        routine.TargetStartTime = request.TargetStartTime;
        routine.EstimatedDurationMinutes = request.EstimatedDurationMinutes;
        routine.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

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
