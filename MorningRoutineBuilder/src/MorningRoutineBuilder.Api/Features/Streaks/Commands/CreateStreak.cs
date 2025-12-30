// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Streaks.Commands;

/// <summary>
/// Command to create a new streak.
/// </summary>
public class CreateStreak : IRequest<StreakDto>
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for CreateStreak command.
/// </summary>
public class CreateStreakHandler : IRequestHandler<CreateStreak, StreakDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public CreateStreakHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<StreakDto> Handle(CreateStreak request, CancellationToken cancellationToken)
    {
        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            RoutineId = request.RoutineId,
            UserId = request.UserId,
            CurrentStreak = 0,
            LongestStreak = 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Streaks.Add(streak);
        await _context.SaveChangesAsync(cancellationToken);

        return new StreakDto
        {
            StreakId = streak.StreakId,
            RoutineId = streak.RoutineId,
            UserId = streak.UserId,
            CurrentStreak = streak.CurrentStreak,
            LongestStreak = streak.LongestStreak,
            LastCompletionDate = streak.LastCompletionDate,
            StreakStartDate = streak.StreakStartDate,
            IsActive = streak.IsActive,
            CreatedAt = streak.CreatedAt
        };
    }
}
