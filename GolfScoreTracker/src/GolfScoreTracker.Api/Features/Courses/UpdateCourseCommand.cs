// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Courses;

public class UpdateCourseCommand : IRequest<CourseDto>
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int NumberOfHoles { get; set; }
    public int TotalPar { get; set; }
    public decimal? CourseRating { get; set; }
    public int? SlopeRating { get; set; }
    public string? Notes { get; set; }
}

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();

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

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public UpdateCourseCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
            ?? throw new KeyNotFoundException($"Course with ID {request.CourseId} not found.");

        course.Name = request.Name;
        course.Location = request.Location;
        course.NumberOfHoles = request.NumberOfHoles;
        course.TotalPar = request.TotalPar;
        course.CourseRating = request.CourseRating;
        course.SlopeRating = request.SlopeRating;
        course.Notes = request.Notes;

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
