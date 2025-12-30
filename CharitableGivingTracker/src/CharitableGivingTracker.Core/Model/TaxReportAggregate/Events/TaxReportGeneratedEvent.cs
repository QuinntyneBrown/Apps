// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core;

public record TaxReportGeneratedEvent
{
    public Guid TaxReportId { get; init; }
    public int TaxYear { get; init; }
    public decimal TotalDeductibleAmount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
