// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class GetCryptoHoldingByIdQuery : IRequest<CryptoHoldingDto?>
{
    public Guid CryptoHoldingId { get; set; }
}

public class GetCryptoHoldingByIdQueryHandler : IRequestHandler<GetCryptoHoldingByIdQuery, CryptoHoldingDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetCryptoHoldingByIdQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<CryptoHoldingDto?> Handle(GetCryptoHoldingByIdQuery request, CancellationToken cancellationToken)
    {
        var holding = await _context.CryptoHoldings
            .FirstOrDefaultAsync(h => h.CryptoHoldingId == request.CryptoHoldingId, cancellationToken);

        if (holding == null)
            return null;

        return new CryptoHoldingDto
        {
            CryptoHoldingId = holding.CryptoHoldingId,
            WalletId = holding.WalletId,
            Symbol = holding.Symbol,
            Name = holding.Name,
            Quantity = holding.Quantity,
            AverageCost = holding.AverageCost,
            CurrentPrice = holding.CurrentPrice,
            LastPriceUpdate = holding.LastPriceUpdate,
            MarketValue = holding.CalculateMarketValue(),
            UnrealizedGainLoss = holding.CalculateUnrealizedGainLoss()
        };
    }
}
