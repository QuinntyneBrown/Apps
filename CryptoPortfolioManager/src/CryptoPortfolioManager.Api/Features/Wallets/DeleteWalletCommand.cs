// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class DeleteWalletCommand : IRequest<bool>
{
    public Guid WalletId { get; set; }
}

public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, bool>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public DeleteWalletCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(w => w.WalletId == request.WalletId, cancellationToken);

        if (wallet == null)
            return false;

        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
