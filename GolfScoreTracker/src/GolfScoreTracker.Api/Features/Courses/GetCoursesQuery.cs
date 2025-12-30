// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Courses;

public class GetCoursesQuery : IRequest<List<CourseDto>>
{
}

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetCoursesQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Name = c.Name,
                Location = c.Location,
                NumberOfHoles = c.NumberOfHoles,
                TotalPar = c.TotalPar,
                CourseRating = c.CourseRating,
                SlopeRating = c.SlopeRating,
                Notes = c.Notes,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
