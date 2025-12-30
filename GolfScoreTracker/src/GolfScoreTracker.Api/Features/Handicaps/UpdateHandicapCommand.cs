// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Handicaps;

public class UpdateHandicapCommand : IRequest<HandicapDto>
{
    public Guid HandicapId { get; set; }
    public Guid UserId { get; set; }
    public decimal HandicapIndex { get; set; }
    public DateTime CalculatedDate { get; set; }
    public int RoundsUsed { get; set; }
    public string? Notes { get; set; }
}

public class UpdateHandicapCommandValidator : AbstractValidator<UpdateHandicapCommand>
{
    public UpdateHandicapCommandValidator()
    {
        RuleFor(x => x.HandicapId)
            .NotEmpty();

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

public class UpdateHandicapCommandHandler : IRequestHandler<UpdateHandicapCommand, HandicapDto>
{
    private readonly IGolfScoreTrackerContext _context;

    public UpdateHandicapCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandicapDto> Handle(UpdateHandicapCommand request, CancellationToken cancellationToken)
    {
        var handicap = await _context.Handicaps
            .FirstOrDefaultAsync(h => h.HandicapId == request.HandicapId, cancellationToken)
            ?? throw new KeyNotFoundException($"Handicap with ID {request.HandicapId} not found.");

        handicap.UserId = request.UserId;
        handicap.HandicapIndex = request.HandicapIndex;
        handicap.CalculatedDate = request.CalculatedDate;
        handicap.RoundsUsed = request.RoundsUsed;
        handicap.Notes = request.Notes;

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
