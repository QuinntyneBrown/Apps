// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Goal;

public record CreateGoalCommand(
    Guid UserId,
    decimal DailyGoalMl,
    DateTime StartDate,
    bool IsActive,
    string? Notes) : IRequest<GoalDto>;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IHydrationTrackerContext _context;

    public CreateGoalCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = request.UserId,
            DailyGoalMl = request.DailyGoalMl,
            StartDate = request.StartDate,
            IsActive = request.IsActive,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.ToDto();
    }
}
