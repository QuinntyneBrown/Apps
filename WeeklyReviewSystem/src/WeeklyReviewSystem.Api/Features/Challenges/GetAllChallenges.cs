// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Query to get all challenges.
/// </summary>
public class GetAllChallengesQuery : IRequest<List<ChallengeDto>>
{
}

/// <summary>
/// Handler for GetAllChallengesQuery.
/// </summary>
public class GetAllChallengesQueryHandler : IRequestHandler<GetAllChallengesQuery, List<ChallengeDto>>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetAllChallengesQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<List<ChallengeDto>> Handle(GetAllChallengesQuery request, CancellationToken cancellationToken)
    {
        var challenges = await _context.Challenges
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return challenges.Select(c => c.ToDto()).ToList();
    }
}
