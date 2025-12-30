// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Handicaps;

public class CreateHandicapCommand : IRequest<HandicapDto>
{
    public Guid UserId { get; set; }
    public decimal HandicapIndex { get; set; }
    public DateTime CalculatedDate { get; set; }
    public int RoundsUsed { get; set; }
    public string? Notes { get; set; }
}

public class CreateHandicapCommandValidator : AbstractValidator<CreateHandicapCommand>
{
    public CreateHandicapCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.HandicapIndex)
            .GreaterThanOrEqualTo(-10)
            .LessThanOrEqualTo(54);

        RuleFor(x => x.CalculatedDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.RoundsUsed)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);
    }
}

public class CreateHandicapCommandHandler : IRequestHandler<CreateHandicapCommand, HandicapDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public CreateHandicapCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandicapDto> Handle(CreateHandicapCommand request, CancellationToken cancellationToken)
    {
        var handicap = new Handicap
        {
            HandicapId = Guid.NewGuid(),
            UserId = request.UserId,
            HandicapIndex = request.HandicapIndex,
            CalculatedDate = request.CalculatedDate,
            RoundsUsed = request.RoundsUsed,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Handicaps.Add(handicap);
        await _context.SaveChangesAsync(cancellationToken);

        return new HandicapDto
        {
            HandicapId = handicap.HandicapId,
            UserId = handicap.UserId,
            HandicapIndex = handicap.HandicapIndex,
            CalculatedDate = handicap.CalculatedDate,
            RoundsUsed = handicap.RoundsUsed,
            Notes = handicap.Notes,
            CreatedAt = handicap.CreatedAt
        };
    }
}
