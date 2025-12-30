// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Bankrolls;

public class UpdateBankrollCommand : IRequest<BankrollDto>
{
    public Guid BankrollId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RecordedDate { get; set; }
    public string? Notes { get; set; }
}

public class UpdateBankrollCommandValidator : AbstractValidator<UpdateBankrollCommand>
{
    public UpdateBankrollCommandValidator()
    {
        RuleFor(x => x.BankrollId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

public class UpdateBankrollCommandHandler : IRequestHandler<UpdateBankrollCommand, BankrollDto>
{
    private readonly IPokerGameTrackerContext _context;

    public UpdateBankrollCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<BankrollDto> Handle(UpdateBankrollCommand request, CancellationToken cancellationToken)
    {
        var bankroll = await _context.Bankrolls
            .FirstOrDefaultAsync(b => b.BankrollId == request.BankrollId, cancellationToken)
            ?? throw new KeyNotFoundException($"Bankroll with ID {request.BankrollId} not found.");

        bankroll.UserId = request.UserId;
        bankroll.Amount = request.Amount;
        bankroll.RecordedDate = request.RecordedDate;
        bankroll.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new BankrollDto
        {
            BankrollId = bankroll.BankrollId,
            UserId = bankroll.UserId,
            Amount = bankroll.Amount,
            RecordedDate = bankroll.RecordedDate,
            Notes = bankroll.Notes,
            CreatedAt = bankroll.CreatedAt
        };
    }
}
