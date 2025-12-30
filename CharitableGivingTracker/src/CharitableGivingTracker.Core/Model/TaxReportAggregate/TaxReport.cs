// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core;

public class TaxReport
{
    public Guid TaxReportId { get; set; }
    public int TaxYear { get; set; }
    public decimal TotalCashDonations { get; set; }
    public decimal TotalNonCashDonations { get; set; }
    public decimal TotalDeductibleAmount { get; set; }
    public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    
    public void CalculateTotals()
    {
        TotalDeductibleAmount = TotalCashDonations + TotalNonCashDonations;
    }
}
