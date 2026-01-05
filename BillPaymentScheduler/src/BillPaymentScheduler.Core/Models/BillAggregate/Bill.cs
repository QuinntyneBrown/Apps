// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core;

public class Bill
{
    public Guid BillId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid PayeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public BillingFrequency BillingFrequency { get; set; }
    public BillStatus Status { get; set; }
    public bool IsAutoPay { get; set; }
    public string? Notes { get; set; }
    public Payee? Payee { get; set; }
    
    public void MarkAsPaid()
    {
        Status = BillStatus.Paid;
    }
    
    public DateTime CalculateNextDueDate()
    {
        return BillingFrequency switch
        {
            BillingFrequency.Weekly => DueDate.AddDays(7),
            BillingFrequency.Monthly => DueDate.AddMonths(1),
            BillingFrequency.Quarterly => DueDate.AddMonths(3),
            BillingFrequency.Annual => DueDate.AddYears(1),
            _ => DueDate
        };
    }
}
