// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class CreateWalletCommand : IRequest<WalletDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string WalletType { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, WalletDto>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public CreateWalletCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<WalletDto> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = request.Name,
            Address = request.Address,
            WalletType = request.WalletType,
            Notes = request.Notes,
            IsActive = true
        };

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync(cancellationToken);

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
