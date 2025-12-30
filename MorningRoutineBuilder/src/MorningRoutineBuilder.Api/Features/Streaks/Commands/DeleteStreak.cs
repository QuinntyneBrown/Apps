// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.Streaks.Commands;

/// <summary>
/// Command to delete a streak.
/// </summary>
public class DeleteStreak : IRequest
{
    public Guid StreakId { get; set; }
}

/// <summary>
/// Handler for DeleteStreak command.
/// </summary>
public class DeleteStreakHandler : IRequestHandler<DeleteStreak>
{
    private readonly IMorningRoutineBuilderContext _context;

    public DeleteStreakHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteStreak request, CancellationToken cancellationToken)
    {
        var streak = await _context.Streaks
            .FirstOrDefaultAsync(s => s.StreakId == request.StreakId, cancellationToken);

        if (streak == null)
        {
            throw new KeyNotFoundException($"Streak with ID {request.StreakId} not found.");
        }

        _context.Streaks.Remove(streak);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
