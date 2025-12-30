// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Bankrolls;

public class CreateBankrollCommand : IRequest<BankrollDto>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}

public class CreateBankrollCommandValidator : AbstractValidator<CreateBankrollCommand>
{
    public CreateBankrollCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

public class CreateBankrollCommandHandler : IRequestHandler<CreateBankrollCommand, BankrollDto>
{
    private readonly IPokerGameTrackerContext _context;

    public CreateBankrollCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<BankrollDto> Handle(CreateBankrollCommand request, CancellationToken cancellationToken)
    {
        var bankroll = new Bankroll
        {
            BankrollId = Guid.NewGuid(),
            UserId = request.UserId,
            Amount = request.Amount,
            RecordedDate = request.RecordedDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Bankrolls.Add(bankroll);
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
