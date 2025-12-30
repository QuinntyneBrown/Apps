// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Hands;

public class CreateHandCommand : IRequest<HandDto>
{
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public string? StartingCards { get; set; }
    public decimal? PotSize { get; set; }
    public bool WasWon { get; set; }
    public string? Notes { get; set; }
}

public class CreateHandCommandValidator : AbstractValidator<CreateHandCommand>
{
    public CreateHandCommandValidator()
    {
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

public class CreateHandCommandHandler : IRequestHandler<CreateHandCommand, HandDto>
{
    private readonly IPokerGameTrackerContext _context;

    public CreateHandCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandDto> Handle(CreateHandCommand request, CancellationToken cancellationToken)
    {
        var hand = new Hand
        {
            HandId = Guid.NewGuid(),
            UserId = request.UserId,
            SessionId = request.SessionId,
            StartingCards = request.StartingCards,
            PotSize = request.PotSize,
            WasWon = request.WasWon,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Hands.Add(hand);
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
