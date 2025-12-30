// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core;

public record DeductionAddedEvent
{
    public Guid DeductionId { get; init; }
    public decimal Amount { get; init; }
    public DeductionCategory Category { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
