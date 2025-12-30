// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Query to get a weekly review by ID.
/// </summary>
public class GetWeeklyReviewByIdQuery : IRequest<WeeklyReviewDto?>
{
    public Guid WeeklyReviewId { get; set; }
}

/// <summary>
/// Handler for GetWeeklyReviewByIdQuery.
/// </summary>
public class GetWeeklyReviewByIdQueryHandler : IRequestHandler<GetWeeklyReviewByIdQuery, WeeklyReviewDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetWeeklyReviewByIdQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyReviewDto?> Handle(GetWeeklyReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.WeeklyReviewId == request.WeeklyReviewId, cancellationToken);

        return review?.ToDto();
    }
}
