// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core;

public record IntakeLoggedEvent
{
    public Guid IntakeId { get; init; }
    public Guid UserId { get; init; }
    public BeverageType BeverageType { get; init; }
    public decimal AmountMl { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
