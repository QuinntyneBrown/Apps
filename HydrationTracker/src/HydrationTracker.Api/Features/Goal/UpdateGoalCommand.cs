// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Goal;

public record UpdateGoalCommand(
    Guid GoalId,
    Guid UserId,
    decimal DailyGoalMl,
    DateTime StartDate,
    bool IsActive,
    string? Notes) : IRequest<GoalDto>;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto>
{
    private readonly IHydrationTrackerContext _context;

    public UpdateGoalCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<GoalDto> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(x => x.GoalId == request.GoalId, cancellationToken)
            ?? throw new InvalidOperationException($"Goal with ID {request.GoalId} not found.");

        goal.UserId = request.UserId;
        goal.DailyGoalMl = request.DailyGoalMl;
        goal.StartDate = request.StartDate;
        goal.IsActive = request.IsActive;
        goal.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return goal.ToDto();
    }
}
