// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.HoleScores;

public class CreateHoleScoreCommand : IRequest<HoleScoreDto>
{
    public Guid RoundId { get; set; }
    public int HoleNumber { get; set; }
    public int Par { get; set; }
    public int Score { get; set; }
    public int? Putts { get; set; }
    public bool FairwayHit { get; set; }
    public bool GreenInRegulation { get; set; }
    public string? Notes { get; set; }
}

public class CreateHoleScoreCommandValidator : AbstractValidator<CreateHoleScoreCommand>
{
    public CreateHoleScoreCommandValidator()
    {
        RuleFor(x => x.RoundId)
            .NotEmpty();

        RuleFor(x => x.HoleNumber)
            .GreaterThan(0)
            .LessThanOrEqualTo(18);

        RuleFor(x => x.Par)
            .GreaterThan(0)
            .LessThanOrEqualTo(6);

        RuleFor(x => x.Score)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);

        RuleFor(x => x.Putts)
            .GreaterThan(0)
            .When(x => x.Putts.HasValue);

        RuleFor(x => x.Notes)
            .MaximumLength(500);
    }
}

public class CreateHoleScoreCommandHandler : IRequestHandler<CreateHoleScoreCommand, HoleScoreDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public CreateHoleScoreCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HoleScoreDto> Handle(CreateHoleScoreCommand request, CancellationToken cancellationToken)
    {
        var holeScore = new HoleScore
        {
            HoleScoreId = Guid.NewGuid(),
            RoundId = request.RoundId,
            HoleNumber = request.HoleNumber,
            Par = request.Par,
            Score = request.Score,
            Putts = request.Putts,
            FairwayHit = request.FairwayHit,
            GreenInRegulation = request.GreenInRegulation,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.HoleScores.Add(holeScore);
        await _context.SaveChangesAsync(cancellationToken);

        return new HoleScoreDto
        {
            HoleScoreId = holeScore.HoleScoreId,
            RoundId = holeScore.RoundId,
            HoleNumber = holeScore.HoleNumber,
            Par = holeScore.Par,
            Score = holeScore.Score,
            Putts = holeScore.Putts,
            FairwayHit = holeScore.FairwayHit,
            GreenInRegulation = holeScore.GreenInRegulation,
            Notes = holeScore.Notes,
            CreatedAt = holeScore.CreatedAt
        };
    }
}
