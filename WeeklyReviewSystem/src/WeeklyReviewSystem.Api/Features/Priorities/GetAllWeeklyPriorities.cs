// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Query to get all weekly priorities.
/// </summary>
public class GetAllWeeklyPrioritiesQuery : IRequest<List<WeeklyPriorityDto>>
{
}

/// <summary>
/// Handler for GetAllWeeklyPrioritiesQuery.
/// </summary>
public class GetAllWeeklyPrioritiesQueryHandler : IRequestHandler<GetAllWeeklyPrioritiesQuery, List<WeeklyPriorityDto>>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetAllWeeklyPrioritiesQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<List<WeeklyPriorityDto>> Handle(GetAllWeeklyPrioritiesQuery request, CancellationToken cancellationToken)
    {
        var priorities = await _context.Priorities
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return priorities.Select(p => p.ToDto()).ToList();
    }
}
