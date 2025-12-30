// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Api.Features.TaxLots;

public class TaxLotDto
{
    public Guid TaxLotId { get; set; }
    public Guid CryptoHoldingId { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal CostBasis { get; set; }
    public bool IsDisposed { get; set; }
    public DateTime? DisposalDate { get; set; }
    public decimal? DisposalPrice { get; set; }
    public decimal? RealizedGainLoss { get; set; }
}
