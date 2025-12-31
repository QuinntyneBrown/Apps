// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public class TaxLot
{
    public Guid TaxLotId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid CryptoHoldingId { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal CostBasis { get; set; }
    public bool IsDisposed { get; set; }
    public DateTime? DisposalDate { get; set; }
    public decimal? DisposalPrice { get; set; }
    public CryptoHolding? CryptoHolding { get; set; }
    
    public decimal? CalculateRealizedGainLoss()
    {
        if (!IsDisposed || !DisposalPrice.HasValue)
            return null;
        return (DisposalPrice.Value * Quantity) - CostBasis;
    }
}
