// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Query to get a challenge by ID.
/// </summary>
public class GetChallengeByIdQuery : IRequest<ChallengeDto?>
{
    public Guid ChallengeId { get; set; }
}

/// <summary>
/// Handler for GetChallengeByIdQuery.
/// </summary>
public class GetChallengeByIdQueryHandler : IRequestHandler<GetChallengeByIdQuery, ChallengeDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetChallengeByIdQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<ChallengeDto?> Handle(GetChallengeByIdQuery request, CancellationToken cancellationToken)
    {
        var challenge = await _context.Challenges
            .FirstOrDefaultAsync(c => c.ChallengeId == request.ChallengeId, cancellationToken);

        return challenge?.ToDto();
    }
}
