// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Query to get all accomplishments.
/// </summary>
public class GetAllAccomplishmentsQuery : IRequest<List<AccomplishmentDto>>
{
}

/// <summary>
/// Handler for GetAllAccomplishmentsQuery.
/// </summary>
public class GetAllAccomplishmentsQueryHandler : IRequestHandler<GetAllAccomplishmentsQuery, List<AccomplishmentDto>>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetAllAccomplishmentsQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<List<AccomplishmentDto>> Handle(GetAllAccomplishmentsQuery request, CancellationToken cancellationToken)
    {
        var accomplishments = await _context.Accomplishments
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return accomplishments.Select(a => a.ToDto()).ToList();
    }
}
