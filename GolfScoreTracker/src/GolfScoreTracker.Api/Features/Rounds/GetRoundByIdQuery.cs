// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Rounds;

public class GetRoundByIdQuery : IRequest<RoundDto?>
{
    public Guid RoundId { get; set; }
}

public class GetRoundByIdQueryHandler : IRequestHandler<GetRoundByIdQuery, RoundDto?>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetRoundByIdQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<RoundDto?> Handle(GetRoundByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Rounds
            .Include(r => r.Course)
            .Where(r => r.RoundId == request.RoundId)
            .Select(r => new RoundDto
            {
                RoundId = r.RoundId,
                UserId = r.UserId,
                CourseId = r.CourseId,
                PlayedDate = r.PlayedDate,
                TotalScore = r.TotalScore,
                TotalPar = r.TotalPar,
                Weather = r.Weather,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt,
                CourseName = r.Course != null ? r.Course.Name : null
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
