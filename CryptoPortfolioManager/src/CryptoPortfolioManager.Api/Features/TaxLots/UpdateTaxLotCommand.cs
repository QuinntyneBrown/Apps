// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class UpdateTaxLotCommand : IRequest<TaxLotDto?>
{
    public Guid TaxLotId { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal CostBasis { get; set; }
    public bool IsDisposed { get; set; }
    public DateTime? DisposalDate { get; set; }
    public decimal? DisposalPrice { get; set; }
}

public class UpdateTaxLotCommandHandler : IRequestHandler<UpdateTaxLotCommand, TaxLotDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public UpdateTaxLotCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TaxLotDto?> Handle(UpdateTaxLotCommand request, CancellationToken cancellationToken)
    {
        var taxLot = await _context.TaxLots
            .FirstOrDefaultAsync(t => t.TaxLotId == request.TaxLotId, cancellationToken);

        if (taxLot == null)
            return null;

        taxLot.AcquisitionDate = request.AcquisitionDate;
        taxLot.Quantity = request.Quantity;
        taxLot.CostBasis = request.CostBasis;
        taxLot.IsDisposed = request.IsDisposed;
        taxLot.DisposalDate = request.DisposalDate;
        taxLot.DisposalPrice = request.DisposalPrice;

        await _context.SaveChangesAsync(cancellationToken);

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
