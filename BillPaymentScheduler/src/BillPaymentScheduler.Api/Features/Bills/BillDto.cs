// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;

namespace BillPaymentScheduler.Api.Features.Bills;

public class BillDto
{
    public Guid BillId { get; set; }
    public Guid PayeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public BillingFrequency BillingFrequency { get; set; }
    public BillStatus Status { get; set; }
    public bool IsAutoPay { get; set; }
    public string? Notes { get; set; }
    public string? PayeeName { get; set; }
}
