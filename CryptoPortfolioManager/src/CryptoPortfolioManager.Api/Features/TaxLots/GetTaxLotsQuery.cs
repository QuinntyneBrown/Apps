// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class GetTaxLotsQuery : IRequest<List<TaxLotDto>>
{
    public Guid? CryptoHoldingId { get; set; }
}

public class GetTaxLotsQueryHandler : IRequestHandler<GetTaxLotsQuery, List<TaxLotDto>>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetTaxLotsQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<List<TaxLotDto>> Handle(GetTaxLotsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TaxLots.AsQueryable();

        if (request.CryptoHoldingId.HasValue)
        {
            query = query.Where(t => t.CryptoHoldingId == request.CryptoHoldingId.Value);
        }

        return await query
            .OrderByDescending(t => t.AcquisitionDate)
            .Select(t => new TaxLotDto
            {
                TaxLotId = t.TaxLotId,
                CryptoHoldingId = t.CryptoHoldingId,
                AcquisitionDate = t.AcquisitionDate,
                Quantity = t.Quantity,
                CostBasis = t.CostBasis,
                IsDisposed = t.IsDisposed,
                DisposalDate = t.DisposalDate,
                DisposalPrice = t.DisposalPrice,
                RealizedGainLoss = t.CalculateRealizedGainLoss()
            })
            .ToListAsync(cancellationToken);
    }
}
