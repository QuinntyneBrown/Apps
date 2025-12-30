// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Streaks.Queries;

/// <summary>
/// Query to get all streaks.
/// </summary>
public class GetStreaks : IRequest<List<StreakDto>>
{
    public Guid? RoutineId { get; set; }
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetStreaks query.
/// </summary>
public class GetStreaksHandler : IRequestHandler<GetStreaks, List<StreakDto>>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetStreaksHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<StreakDto>> Handle(GetStreaks request, CancellationToken cancellationToken)
    {
        var query = _context.Streaks.AsQueryable();

        if (request.RoutineId.HasValue)
        {
            query = query.Where(s => s.RoutineId == request.RoutineId.Value);
        }

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        var streaks = await query
            .OrderByDescending(s => s.CurrentStreak)
            .ToListAsync(cancellationToken);

        return streaks.Select(s => new StreakDto
        {
            StreakId = s.StreakId,
            RoutineId = s.RoutineId,
            UserId = s.UserId,
            CurrentStreak = s.CurrentStreak,
            LongestStreak = s.LongestStreak,
            LastCompletionDate = s.LastCompletionDate,
            StreakStartDate = s.StreakStartDate,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt
        }).ToList();
    }
}
