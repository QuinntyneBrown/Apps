// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Rounds;

public class GetRoundsQuery : IRequest<List<RoundDto>>
{
    public Guid? UserId { get; set; }
    public Guid? CourseId { get; set; }
}

public class GetRoundsQueryHandler : IRequestHandler<GetRoundsQuery, List<RoundDto>>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetRoundsQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<RoundDto>> Handle(GetRoundsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Rounds.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.CourseId.HasValue)
        {
            query = query.Where(r => r.CourseId == request.CourseId.Value);
        }

        return await query
            .Include(r => r.Course)
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
            .OrderByDescending(r => r.PlayedDate)
            .ToListAsync(cancellationToken);
    }
}
