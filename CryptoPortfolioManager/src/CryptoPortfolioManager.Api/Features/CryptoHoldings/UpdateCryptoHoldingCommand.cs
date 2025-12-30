// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class UpdateCryptoHoldingCommand : IRequest<CryptoHoldingDto?>
{
    public Guid CryptoHoldingId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
}

public class UpdateCryptoHoldingCommandHandler : IRequestHandler<UpdateCryptoHoldingCommand, CryptoHoldingDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public UpdateCryptoHoldingCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<CryptoHoldingDto?> Handle(UpdateCryptoHoldingCommand request, CancellationToken cancellationToken)
    {
        var holding = await _context.CryptoHoldings
            .FirstOrDefaultAsync(h => h.CryptoHoldingId == request.CryptoHoldingId, cancellationToken);

        if (holding == null)
            return null;

        holding.Symbol = request.Symbol;
        holding.Name = request.Name;
        holding.Quantity = request.Quantity;
        holding.AverageCost = request.AverageCost;
        holding.UpdatePrice(request.CurrentPrice);

        await _context.SaveChangesAsync(cancellationToken);

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
