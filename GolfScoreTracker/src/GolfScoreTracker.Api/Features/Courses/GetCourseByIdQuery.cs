// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Courses;

public class GetCourseByIdQuery : IRequest<CourseDto?>
{
    public Guid CourseId { get; set; }
}

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetCourseByIdQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(c => c.CourseId == request.CourseId)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
