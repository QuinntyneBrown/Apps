// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class DeleteCryptoHoldingCommand : IRequest<bool>
{
    public Guid CryptoHoldingId { get; set; }
}

public class DeleteCryptoHoldingCommandHandler : IRequestHandler<DeleteCryptoHoldingCommand, bool>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public DeleteCryptoHoldingCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCryptoHoldingCommand request, CancellationToken cancellationToken)
    {
        var holding = await _context.CryptoHoldings
            .FirstOrDefaultAsync(h => h.CryptoHoldingId == request.CryptoHoldingId, cancellationToken);

        if (holding == null)
            return false;

        _context.CryptoHoldings.Remove(holding);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
