// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core;

public record TaxYearCreatedEvent
{
    public Guid TaxYearId { get; init; }
    public int Year { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
