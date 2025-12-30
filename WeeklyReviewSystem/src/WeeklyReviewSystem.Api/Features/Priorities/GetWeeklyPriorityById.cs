// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Query to get a weekly priority by ID.
/// </summary>
public class GetWeeklyPriorityByIdQuery : IRequest<WeeklyPriorityDto?>
{
    public Guid WeeklyPriorityId { get; set; }
}

/// <summary>
/// Handler for GetWeeklyPriorityByIdQuery.
/// </summary>
public class GetWeeklyPriorityByIdQueryHandler : IRequestHandler<GetWeeklyPriorityByIdQuery, WeeklyPriorityDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetWeeklyPriorityByIdQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyPriorityDto?> Handle(GetWeeklyPriorityByIdQuery request, CancellationToken cancellationToken)
    {
        var priority = await _context.Priorities
            .FirstOrDefaultAsync(p => p.WeeklyPriorityId == request.WeeklyPriorityId, cancellationToken);

        return priority?.ToDto();
    }
}
