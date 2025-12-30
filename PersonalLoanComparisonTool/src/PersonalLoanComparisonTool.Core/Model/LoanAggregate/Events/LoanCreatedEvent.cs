// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public record LoanCreatedEvent
{
    public Guid LoanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal RequestedAmount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
