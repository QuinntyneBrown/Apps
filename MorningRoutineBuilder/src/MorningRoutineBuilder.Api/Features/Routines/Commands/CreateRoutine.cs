// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Routines.Commands;

/// <summary>
/// Command to create a new routine.
/// </summary>
public class CreateRoutine : IRequest<RoutineDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeSpan TargetStartTime { get; set; }
    public int EstimatedDurationMinutes { get; set; }
}

/// <summary>
/// Handler for CreateRoutine command.
/// </summary>
public class CreateRoutineHandler : IRequestHandler<CreateRoutine, RoutineDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public CreateRoutineHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<RoutineDto> Handle(CreateRoutine request, CancellationToken cancellationToken)
    {
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            TargetStartTime = request.TargetStartTime,
            EstimatedDurationMinutes = request.EstimatedDurationMinutes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Routines.Add(routine);
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
