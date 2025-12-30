// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class GetWalletsQuery : IRequest<List<WalletDto>>
{
}

public class GetWalletsQueryHandler : IRequestHandler<GetWalletsQuery, List<WalletDto>>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetWalletsQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<List<WalletDto>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Wallets
            .Include(w => w.Holdings)
            .Select(w => new WalletDto
            {
                WalletId = w.WalletId,
                Name = w.Name,
                Address = w.Address,
                WalletType = w.WalletType,
                IsActive = w.IsActive,
                Notes = w.Notes,
                TotalValue = w.CalculateTotalValue()
            })
            .ToListAsync(cancellationToken);
    }
}
