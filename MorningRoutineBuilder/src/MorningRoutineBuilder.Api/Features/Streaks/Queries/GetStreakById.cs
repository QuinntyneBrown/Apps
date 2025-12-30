// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Streaks.Queries;

/// <summary>
/// Query to get a streak by ID.
/// </summary>
public class GetStreakById : IRequest<StreakDto?>
{
    public Guid StreakId { get; set; }
}

/// <summary>
/// Handler for GetStreakById query.
/// </summary>
public class GetStreakByIdHandler : IRequestHandler<GetStreakById, StreakDto?>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetStreakByIdHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<StreakDto?> Handle(GetStreakById request, CancellationToken cancellationToken)
    {
        var streak = await _context.Streaks
            .FirstOrDefaultAsync(s => s.StreakId == request.StreakId, cancellationToken);

        if (streak == null)
        {
            return null;
        }

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
