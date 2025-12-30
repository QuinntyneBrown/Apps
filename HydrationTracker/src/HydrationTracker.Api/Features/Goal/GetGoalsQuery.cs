// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Goal;

public record GetGoalsQuery() : IRequest<List<GoalDto>>;

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, List<GoalDto>>
{
    private readonly IHydrationTrackerContext _context;

    public GetGoalsQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Goals
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
