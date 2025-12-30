// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Courses;

public class CreateCourseCommand : IRequest<CourseDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int NumberOfHoles { get; set; } = 18;
    public int TotalPar { get; set; }
    public decimal? CourseRating { get; set; }
    public int? SlopeRating { get; set; }
    public string? Notes { get; set; }
}

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Location)
            .MaximumLength(300);

        RuleFor(x => x.NumberOfHoles)
            .GreaterThan(0)
            .LessThanOrEqualTo(18);

        RuleFor(x => x.TotalPar)
            .GreaterThan(0);

        RuleFor(x => x.CourseRating)
            .GreaterThan(0)
            .When(x => x.CourseRating.HasValue);

        RuleFor(x => x.SlopeRating)
            .GreaterThan(0)
            .LessThanOrEqualTo(155)
            .When(x => x.SlopeRating.HasValue);
    }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public CreateCourseCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = request.Name,
            Location = request.Location,
            NumberOfHoles = request.NumberOfHoles,
            TotalPar = request.TotalPar,
            CourseRating = request.CourseRating,
            SlopeRating = request.SlopeRating,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return new CourseDto
        {
            CourseId = course.CourseId,
            Name = course.Name,
            Location = course.Location,
            NumberOfHoles = course.NumberOfHoles,
            TotalPar = course.TotalPar,
            CourseRating = course.CourseRating,
            SlopeRating = course.SlopeRating,
            Notes = course.Notes,
            CreatedAt = course.CreatedAt
        };
    }
}
