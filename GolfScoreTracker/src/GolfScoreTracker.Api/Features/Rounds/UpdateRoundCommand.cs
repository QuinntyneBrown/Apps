// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Rounds;

public class UpdateRoundCommand : IRequest<RoundDto>
{
    public Guid RoundId { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime PlayedDate { get; set; }
    public int TotalScore { get; set; }
    public int TotalPar { get; set; }
    public string? Weather { get; set; }
    public string? Notes { get; set; }
}

public class UpdateRoundCommandValidator : AbstractValidator<UpdateRoundCommand>
{
    public UpdateRoundCommandValidator()
    {
        RuleFor(x => x.RoundId)
            .NotEmpty();

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

public class UpdateRoundCommandHandler : IRequestHandler<UpdateRoundCommand, RoundDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public UpdateRoundCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<RoundDto> Handle(UpdateRoundCommand request, CancellationToken cancellationToken)
    {
        var round = await _context.Rounds
            .FirstOrDefaultAsync(r => r.RoundId == request.RoundId, cancellationToken)
            ?? throw new KeyNotFoundException($"Round with ID {request.RoundId} not found.");

        round.UserId = request.UserId;
        round.CourseId = request.CourseId;
        round.PlayedDate = request.PlayedDate;
        round.TotalScore = request.TotalScore;
        round.TotalPar = request.TotalPar;
        round.Weather = request.Weather;
        round.Notes = request.Notes;

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
