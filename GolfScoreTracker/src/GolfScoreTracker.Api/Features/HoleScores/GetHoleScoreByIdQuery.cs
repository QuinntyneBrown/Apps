// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.HoleScores;

public class GetHoleScoreByIdQuery : IRequest<HoleScoreDto?>
{
    public Guid HoleScoreId { get; set; }
}

public class GetHoleScoreByIdQueryHandler : IRequestHandler<GetHoleScoreByIdQuery, HoleScoreDto?>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetHoleScoreByIdQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HoleScoreDto?> Handle(GetHoleScoreByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.HoleScores
            .Where(h => h.HoleScoreId == request.HoleScoreId)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
