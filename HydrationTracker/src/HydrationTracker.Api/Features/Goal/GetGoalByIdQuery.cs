// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Goal;

public record GetGoalByIdQuery(Guid GoalId) : IRequest<GoalDto>;

public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, GoalDto>
{
    private readonly IHydrationTrackerContext _context;

    public GetGoalByIdQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<GoalDto> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.GoalId == request.GoalId, cancellationToken)
            ?? throw new InvalidOperationException($"Goal with ID {request.GoalId} not found.");

        return goal.ToDto();
    }
}
