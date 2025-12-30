// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core;

public record DonationRecordedEvent
{
    public Guid DonationId { get; init; }
    public Guid OrganizationId { get; init; }
    public decimal Amount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
