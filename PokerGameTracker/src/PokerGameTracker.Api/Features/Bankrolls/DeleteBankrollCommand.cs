// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Bankrolls;

public class DeleteBankrollCommand : IRequest<bool>
{
    public Guid BankrollId { get; set; }
}

public class DeleteBankrollCommandHandler : IRequestHandler<DeleteBankrollCommand, bool>
{
    private readonly IPokerGameTrackerContext _context;

    public DeleteBankrollCommandHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBankrollCommand request, CancellationToken cancellationToken)
    {
        var bankroll = await _context.Bankrolls
            .FirstOrDefaultAsync(b => b.BankrollId == request.BankrollId, cancellationToken);

        if (bankroll == null)
        {
            return false;
        }

        _context.Bankrolls.Remove(bankroll);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
