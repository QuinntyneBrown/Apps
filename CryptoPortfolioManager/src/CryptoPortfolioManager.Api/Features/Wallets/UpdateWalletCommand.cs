// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class UpdateWalletCommand : IRequest<WalletDto?>
{
    public Guid WalletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string WalletType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}

public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, WalletDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public UpdateWalletCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<WalletDto?> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .Include(w => w.Holdings)
            .FirstOrDefaultAsync(w => w.WalletId == request.WalletId, cancellationToken);

        if (wallet == null)
            return null;

        wallet.Name = request.Name;
        wallet.Address = request.Address;
        wallet.WalletType = request.WalletType;
        wallet.IsActive = request.IsActive;
        wallet.Notes = request.Notes;

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
