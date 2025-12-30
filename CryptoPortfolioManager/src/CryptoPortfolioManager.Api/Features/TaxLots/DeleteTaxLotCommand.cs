// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class DeleteTaxLotCommand : IRequest<bool>
{
    public Guid TaxLotId { get; set; }
}

public class DeleteTaxLotCommandHandler : IRequestHandler<DeleteTaxLotCommand, bool>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public DeleteTaxLotCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTaxLotCommand request, CancellationToken cancellationToken)
    {
        var taxLot = await _context.TaxLots
            .FirstOrDefaultAsync(t => t.TaxLotId == request.TaxLotId, cancellationToken);

        if (taxLot == null)
            return false;

        _context.TaxLots.Remove(taxLot);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
