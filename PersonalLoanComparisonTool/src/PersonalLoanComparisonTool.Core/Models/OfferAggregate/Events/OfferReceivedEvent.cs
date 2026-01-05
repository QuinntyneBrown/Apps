// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public record OfferReceivedEvent
{
    public Guid OfferId { get; init; }
    public Guid LoanId { get; init; }
    public string LenderName { get; init; } = string.Empty;
    public decimal InterestRate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
