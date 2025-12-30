// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Api.Features.Payments;

public class PaymentDto
{
    public Guid PaymentId { get; set; }
    public Guid BillId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? ConfirmationNumber { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public string? BillName { get; set; }
}
