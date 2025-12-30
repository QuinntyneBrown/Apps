// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Query to get all weekly reviews.
/// </summary>
public class GetAllWeeklyReviewsQuery : IRequest<List<WeeklyReviewDto>>
{
}

/// <summary>
/// Handler for GetAllWeeklyReviewsQuery.
/// </summary>
public class GetAllWeeklyReviewsQueryHandler : IRequestHandler<GetAllWeeklyReviewsQuery, List<WeeklyReviewDto>>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetAllWeeklyReviewsQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<List<WeeklyReviewDto>> Handle(GetAllWeeklyReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .OrderByDescending(r => r.WeekStartDate)
            .ToListAsync(cancellationToken);

        return reviews.Select(r => r.ToDto()).ToList();
    }
}
