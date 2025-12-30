// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Sessions;

public class UpdateSessionCommand : IRequest<SessionDto>
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public GameType GameType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal BuyIn { get; set; }
    public decimal? CashOut { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
}

public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionCommandValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.BuyIn)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CashOut)
            .GreaterThanOrEqualTo(0)
            .When(x => x.CashOut.HasValue);

        RuleFor(x => x.Location)
            .MaximumLength(200);
    }
}

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, SessionDto>
{
    private readonly IPokerGameTrackerContext _context;

    public UpdateSessionCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<SessionDto> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken)
            ?? throw new KeyNotFoundException($"Session with ID {request.SessionId} not found.");

        session.UserId = request.UserId;
        session.GameType = request.GameType;
        session.StartTime = request.StartTime;
        session.EndTime = request.EndTime;
        session.BuyIn = request.BuyIn;
        session.CashOut = request.CashOut;
        session.Location = request.Location;
        session.Notes = request.Notes;

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
