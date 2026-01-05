// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public class Loan
{
    public Guid LoanId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public LoanType LoanType { get; set; }
    public decimal RequestedAmount { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int CreditScore { get; set; }
    public string? Notes { get; set; }
    public List<Offer> Offers { get; set; } = new List<Offer>();
    
    public Offer? GetBestOffer()
    {
        return Offers.OrderBy(o => o.TotalCost).FirstOrDefault();
    }
}
