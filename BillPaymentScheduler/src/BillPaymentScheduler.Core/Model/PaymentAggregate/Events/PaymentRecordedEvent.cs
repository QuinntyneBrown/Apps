// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core;

public record PaymentRecordedEvent
{
    public Guid PaymentId { get; init; }
    public Guid BillId { get; init; }
    public decimal Amount { get; init; }
    public DateTime PaymentDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
