// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class GetCryptoHoldingsQuery : IRequest<List<CryptoHoldingDto>>
{
    public Guid? WalletId { get; set; }
}

public class GetCryptoHoldingsQueryHandler : IRequestHandler<GetCryptoHoldingsQuery, List<CryptoHoldingDto>>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetCryptoHoldingsQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<List<CryptoHoldingDto>> Handle(GetCryptoHoldingsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.CryptoHoldings.AsQueryable();

        if (request.WalletId.HasValue)
        {
            query = query.Where(h => h.WalletId == request.WalletId.Value);
        }

        return await query
            .Select(h => new CryptoHoldingDto
            {
                CryptoHoldingId = h.CryptoHoldingId,
                WalletId = h.WalletId,
                Symbol = h.Symbol,
                Name = h.Name,
                Quantity = h.Quantity,
                AverageCost = h.AverageCost,
                CurrentPrice = h.CurrentPrice,
                LastPriceUpdate = h.LastPriceUpdate,
                MarketValue = h.CalculateMarketValue(),
                UnrealizedGainLoss = h.CalculateUnrealizedGainLoss()
            })
            .ToListAsync(cancellationToken);
    }
}
