// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Sessions;

public class CreateSessionCommand : IRequest<SessionDto>
{
    public Guid UserId { get; set; }
    public GameType GameType { get; set; }
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public decimal BuyIn { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
}

public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.BuyIn)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Location)
            .MaximumLength(200);
    }
}

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
{
    private readonly IPokerGameTrackerContext _context;

    public CreateSessionCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = request.UserId,
            GameType = request.GameType,
            StartTime = request.StartTime,
            BuyIn = request.BuyIn,
            Location = request.Location,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new SessionDto
        {
            SessionId = session.SessionId,
            UserId = session.UserId,
            GameType = (int)session.GameType,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            BuyIn = session.BuyIn,
            CashOut = session.CashOut,
            Location = session.Location,
            Notes = session.Notes,
            CreatedAt = session.CreatedAt
        };
    }
}
