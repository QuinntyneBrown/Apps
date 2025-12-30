// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class CreateCryptoHoldingCommand : IRequest<CryptoHoldingDto>
{
    public Guid WalletId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
}

public class CreateCryptoHoldingCommandHandler : IRequestHandler<CreateCryptoHoldingCommand, CryptoHoldingDto>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public CreateCryptoHoldingCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<CryptoHoldingDto> Handle(CreateCryptoHoldingCommand request, CancellationToken cancellationToken)
    {
        var holding = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = request.WalletId,
            Symbol = request.Symbol,
            Name = request.Name,
            Quantity = request.Quantity,
            AverageCost = request.AverageCost,
            CurrentPrice = request.CurrentPrice,
            LastPriceUpdate = DateTime.UtcNow
        };

        _context.CryptoHoldings.Add(holding);
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
