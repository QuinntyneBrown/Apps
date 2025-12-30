// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.HoleScores;

public class UpdateHoleScoreCommand : IRequest<HoleScoreDto>
{
    public Guid HoleScoreId { get; set; }
    public Guid RoundId { get; set; }
    public int HoleNumber { get; set; }
    public int Par { get; set; }
    public int Score { get; set; }
    public int? Putts { get; set; }
    public bool FairwayHit { get; set; }
    public bool GreenInRegulation { get; set; }
    public string? Notes { get; set; }
}

public class UpdateHoleScoreCommandValidator : AbstractValidator<UpdateHoleScoreCommand>
{
    public UpdateHoleScoreCommandValidator()
    {
        RuleFor(x => x.HoleScoreId)
            .NotEmpty();

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

public class UpdateHoleScoreCommandHandler : IRequestHandler<UpdateHoleScoreCommand, HoleScoreDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public UpdateHoleScoreCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HoleScoreDto> Handle(UpdateHoleScoreCommand request, CancellationToken cancellationToken)
    {
        var holeScore = await _context.HoleScores
            .FirstOrDefaultAsync(h => h.HoleScoreId == request.HoleScoreId, cancellationToken)
            ?? throw new KeyNotFoundException($"HoleScore with ID {request.HoleScoreId} not found.");

        holeScore.RoundId = request.RoundId;
        holeScore.HoleNumber = request.HoleNumber;
        holeScore.Par = request.Par;
        holeScore.Score = request.Score;
        holeScore.Putts = request.Putts;
        holeScore.FairwayHit = request.FairwayHit;
        holeScore.GreenInRegulation = request.GreenInRegulation;
        holeScore.Notes = request.Notes;

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
