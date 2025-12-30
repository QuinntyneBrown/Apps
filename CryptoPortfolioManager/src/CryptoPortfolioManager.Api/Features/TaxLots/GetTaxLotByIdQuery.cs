// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class GetTaxLotByIdQuery : IRequest<TaxLotDto?>
{
    public Guid TaxLotId { get; set; }
}

public class GetTaxLotByIdQueryHandler : IRequestHandler<GetTaxLotByIdQuery, TaxLotDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetTaxLotByIdQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TaxLotDto?> Handle(GetTaxLotByIdQuery request, CancellationToken cancellationToken)
    {
        var taxLot = await _context.TaxLots
            .FirstOrDefaultAsync(t => t.TaxLotId == request.TaxLotId, cancellationToken);

        if (taxLot == null)
            return null;

        return new TaxLotDto
        {
            TaxLotId = taxLot.TaxLotId,
            CryptoHoldingId = taxLot.CryptoHoldingId,
            AcquisitionDate = taxLot.AcquisitionDate,
            Quantity = taxLot.Quantity,
            CostBasis = taxLot.CostBasis,
            IsDisposed = taxLot.IsDisposed,
            DisposalDate = taxLot.DisposalDate,
            DisposalPrice = taxLot.DisposalPrice,
            RealizedGainLoss = taxLot.CalculateRealizedGainLoss()
        };
    }
}
