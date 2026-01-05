// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public class Offer
{
    public Guid OfferId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid LoanId { get; set; }
    public string LenderName { get; set; } = string.Empty;
    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Fees { get; set; }
    public string? Notes { get; set; }
    public Loan? Loan { get; set; }
    
    public void CalculateTotalCost()
    {
        TotalCost = (MonthlyPayment * TermMonths) + Fees;
    }
    
    public decimal CalculateAPR()
    {
        return InterestRate;
    }
}
