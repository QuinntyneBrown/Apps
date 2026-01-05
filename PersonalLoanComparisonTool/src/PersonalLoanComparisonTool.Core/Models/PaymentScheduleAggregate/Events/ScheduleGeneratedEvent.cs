// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public record ScheduleGeneratedEvent
{
    public Guid PaymentScheduleId { get; init; }
    public Guid OfferId { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
