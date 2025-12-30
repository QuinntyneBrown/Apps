// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class CreateTaxLotCommand : IRequest<TaxLotDto>
{
    public Guid CryptoHoldingId { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal CostBasis { get; set; }
}

public class CreateTaxLotCommandHandler : IRequestHandler<CreateTaxLotCommand, TaxLotDto>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public CreateTaxLotCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TaxLotDto> Handle(CreateTaxLotCommand request, CancellationToken cancellationToken)
    {
        var taxLot = new TaxLot
        {
            TaxLotId = Guid.NewGuid(),
            CryptoHoldingId = request.CryptoHoldingId,
            AcquisitionDate = request.AcquisitionDate,
            Quantity = request.Quantity,
            CostBasis = request.CostBasis,
            IsDisposed = false
        };

        _context.TaxLots.Add(taxLot);
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
