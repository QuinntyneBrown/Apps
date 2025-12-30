// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Hands;

public class UpdateHandCommand : IRequest<HandDto>
{
    public Guid HandId { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public string? StartingCards { get; set; }
    public decimal? PotSize { get; set; }
    public bool WasWon { get; set; }
    public string? Notes { get; set; }
}

public class UpdateHandCommandValidator : AbstractValidator<UpdateHandCommand>
{
    public UpdateHandCommandValidator()
    {
        RuleFor(x => x.HandId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.SessionId)
            .NotEmpty();

        RuleFor(x => x.StartingCards)
            .MaximumLength(100);

        RuleFor(x => x.PotSize)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PotSize.HasValue);
    }
}

public class UpdateHandCommandHandler : IRequestHandler<UpdateHandCommand, HandDto>
{
    private readonly IPokerGameTrackerContext _context;

    public UpdateHandCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandDto> Handle(UpdateHandCommand request, CancellationToken cancellationToken)
    {
        var hand = await _context.Hands
            .FirstOrDefaultAsync(h => h.HandId == request.HandId, cancellationToken)
            ?? throw new KeyNotFoundException($"Hand with ID {request.HandId} not found.");

        hand.UserId = request.UserId;
        hand.SessionId = request.SessionId;
        hand.StartingCards = request.StartingCards;
        hand.PotSize = request.PotSize;
        hand.WasWon = request.WasWon;
        hand.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new HandDto
        {
            HandId = hand.HandId,
            UserId = hand.UserId,
            SessionId = hand.SessionId,
            StartingCards = hand.StartingCards,
            PotSize = hand.PotSize,
            WasWon = hand.WasWon,
            Notes = hand.Notes,
            CreatedAt = hand.CreatedAt
        };
    }
}
