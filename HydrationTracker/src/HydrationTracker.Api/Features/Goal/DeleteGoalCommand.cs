// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Goal;

public record DeleteGoalCommand(Guid GoalId) : IRequest<Unit>;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, Unit>
{
    private readonly IHydrationTrackerContext _context;

    public DeleteGoalCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(x => x.GoalId == request.GoalId, cancellationToken)
            ?? throw new InvalidOperationException($"Goal with ID {request.GoalId} not found.");

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
