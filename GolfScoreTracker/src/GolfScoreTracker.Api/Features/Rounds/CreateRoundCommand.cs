// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Rounds;

public class CreateRoundCommand : IRequest<RoundDto>
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime PlayedDate { get; set; }
    public int TotalScore { get; set; }
    public int TotalPar { get; set; }
    public string? Weather { get; set; }
    public string? Notes { get; set; }
}

public class CreateRoundCommandValidator : AbstractValidator<CreateRoundCommand>
{
    public CreateRoundCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.CourseId)
            .NotEmpty();

        RuleFor(x => x.PlayedDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.TotalScore)
            .GreaterThan(0);

        RuleFor(x => x.TotalPar)
            .GreaterThan(0);

        RuleFor(x => x.Weather)
            .MaximumLength(100);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);
    }
}

public class CreateRoundCommandHandler : IRequestHandler<CreateRoundCommand, RoundDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public CreateRoundCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<RoundDto> Handle(CreateRoundCommand request, CancellationToken cancellationToken)
    {
        var round = new Round
        {
            RoundId = Guid.NewGuid(),
            UserId = request.UserId,
            CourseId = request.CourseId,
            PlayedDate = request.PlayedDate,
            TotalScore = request.TotalScore,
            TotalPar = request.TotalPar,
            Weather = request.Weather,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rounds.Add(round);
        await _context.SaveChangesAsync(cancellationToken);

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

        return new RoundDto
        {
            RoundId = round.RoundId,
            UserId = round.UserId,
            CourseId = round.CourseId,
            PlayedDate = round.PlayedDate,
            TotalScore = round.TotalScore,
            TotalPar = round.TotalPar,
            Weather = round.Weather,
            Notes = round.Notes,
            CreatedAt = round.CreatedAt,
            CourseName = course?.Name
        };
    }
}
