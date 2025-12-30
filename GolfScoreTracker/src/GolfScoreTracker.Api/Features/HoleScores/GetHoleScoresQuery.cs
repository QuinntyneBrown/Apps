// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.HoleScores;

public class GetHoleScoresQuery : IRequest<List<HoleScoreDto>>
{
    public Guid? RoundId { get; set; }
}

public class GetHoleScoresQueryHandler : IRequestHandler<GetHoleScoresQuery, List<HoleScoreDto>>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetHoleScoresQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<HoleScoreDto>> Handle(GetHoleScoresQuery request, CancellationToken cancellationToken)
    {
        var query = _context.HoleScores.AsQueryable();

        if (request.RoundId.HasValue)
        {
            query = query.Where(h => h.RoundId == request.RoundId.Value);
        }

        return await query
            .Select(h => new HoleScoreDto
            {
                HoleScoreId = h.HoleScoreId,
                RoundId = h.RoundId,
                HoleNumber = h.HoleNumber,
                Par = h.Par,
                Score = h.Score,
                Putts = h.Putts,
                FairwayHit = h.FairwayHit,
                GreenInRegulation = h.GreenInRegulation,
                Notes = h.Notes,
                CreatedAt = h.CreatedAt
            })
            .OrderBy(h => h.HoleNumber)
            .ToListAsync(cancellationToken);
    }
}
