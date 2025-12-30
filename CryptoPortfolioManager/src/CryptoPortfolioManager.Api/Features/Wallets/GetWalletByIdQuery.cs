// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class GetWalletByIdQuery : IRequest<WalletDto?>
{
    public Guid WalletId { get; set; }
}

public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, WalletDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetWalletByIdQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<WalletDto?> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .Include(w => w.Holdings)
            .FirstOrDefaultAsync(w => w.WalletId == request.WalletId, cancellationToken);

        if (wallet == null)
            return null;

        return new WalletDto
        {
            WalletId = wallet.WalletId,
            Name = wallet.Name,
            Address = wallet.Address,
            WalletType = wallet.WalletType,
            IsActive = wallet.IsActive,
            Notes = wallet.Notes,
            TotalValue = wallet.CalculateTotalValue()
        };
    }
}
