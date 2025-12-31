// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core;

public class Payment
{
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid BillId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? ConfirmationNumber { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public Bill? Bill { get; set; }
}
