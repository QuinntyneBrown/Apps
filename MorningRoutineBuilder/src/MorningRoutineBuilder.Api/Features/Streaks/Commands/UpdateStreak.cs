// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Streaks.Commands;

/// <summary>
/// Command to update an existing streak.
/// </summary>
public class UpdateStreak : IRequest<StreakDto>
{
    public Guid StreakId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastCompletionDate { get; set; }
    public DateTime? StreakStartDate { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Handler for UpdateStreak command.
/// </summary>
public class UpdateStreakHandler : IRequestHandler<UpdateStreak, StreakDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public UpdateStreakHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<StreakDto> Handle(UpdateStreak request, CancellationToken cancellationToken)
    {
        var streak = await _context.Streaks
            .FirstOrDefaultAsync(s => s.StreakId == request.StreakId, cancellationToken);

        if (streak == null)
        {
            throw new KeyNotFoundException($"Streak with ID {request.StreakId} not found.");
        }

        streak.CurrentStreak = request.CurrentStreak;
        streak.LongestStreak = request.LongestStreak;
        streak.LastCompletionDate = request.LastCompletionDate;
        streak.StreakStartDate = request.StreakStartDate;
        streak.IsActive = request.IsActive;

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
